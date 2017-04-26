using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Square.OkHttp3;
using System.Net.Http;
using System.Net;

namespace Nuubit.SDK
{
    class ConcatenatingStream : Stream
    {
        readonly CancellationTokenSource cts = new CancellationTokenSource();
        readonly Action onDispose;

        long position;
        bool closeStreams;
        int isEnding = 0;
        Task blockUntil;

        IEnumerator<Stream> iterator;

        Stream current;
        Stream Current
        {
            get
            {
                if (current != null) return current;
                if (iterator == null) throw new ObjectDisposedException(GetType().Name);

                if (iterator.MoveNext())
                {
                    current = iterator.Current;
                }

                return current;
            }
        }

        public ConcatenatingStream(IEnumerable<Func<Stream>> source, bool closeStreams, Task blockUntil = null, Action onDispose = null)
        {
            if (source == null) throw new ArgumentNullException("source");

            iterator = source.Select(x => x()).GetEnumerator();

            this.closeStreams = closeStreams;
            this.blockUntil = blockUntil;
            this.onDispose = onDispose;
        }

        public override bool CanRead { get { return true; } }
        public override bool CanWrite { get { return false; } }
        public override void Write(byte[] buffer, int offset, int count) { throw new NotSupportedException(); }
        public override void WriteByte(byte value) { throw new NotSupportedException(); }
        public override bool CanSeek { get { return false; } }
        public override bool CanTimeout { get { return false; } }
        public override void SetLength(long value) { throw new NotSupportedException(); }
        public override long Seek(long offset, SeekOrigin origin) { throw new NotSupportedException(); }

        public override void Flush() { }
        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position
        {
            get { return position; }
            set { if (value != this.position) throw new NotSupportedException(); }
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            int result = 0;

            if (blockUntil != null)
            {
                await blockUntil.ContinueWith(_ => { }, cancellationToken);
            }

            while (count > 0)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }

                if (cts.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }

                Stream stream = Current;
                if (stream == null) break;

                var thisCount = default(int);
                thisCount = await stream.ReadAsync(buffer, offset, count, cancellationToken);

                result += thisCount;
                count -= thisCount;
                offset += thisCount;
                if (thisCount == 0) EndOfStream();
            }

            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }

            if (cts.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }

            position += result;
            return result;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return readInternal(buffer, offset, count);
        }

        int readInternal(byte[] buffer, int offset, int count, CancellationToken ct = default(CancellationToken))
        {
            int result = 0;

            if (blockUntil != null)
            {
                blockUntil.Wait(cts.Token);
            }

            while (count > 0)
            {
                if (ct.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }

                if (cts.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }

                Stream stream = Current;
                if (stream == null) break;

                var thisCount = default(int);
                thisCount = stream.Read(buffer, offset, count);

                result += thisCount;
                count -= thisCount;
                offset += thisCount;
                if (thisCount == 0) EndOfStream();
            }

            position += result;
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref isEnding, 1, 0) == 1)
            {
                return;
            }

            if (disposing)
            {
                cts.Cancel();

                while (Current != null)
                {
                    EndOfStream();
                }

                iterator.Dispose();
                iterator = null;
                current = null;

                if (onDispose != null) onDispose();
            }

            base.Dispose(disposing);
        }

        void EndOfStream()
        {
            if (closeStreams && current != null)
            {
                current.Close();
                current.Dispose();
            }

            current = null;
        }
    }

	public delegate void ProgressDelegate(long bytes, long totalBytes, long totalBytesExpected);

	public class ProgressStreamContent : StreamContent
	{
		public ProgressStreamContent(Stream stream, CancellationToken token)
			: this(new ProgressStream(stream, token))
		{
		}

		public ProgressStreamContent(Stream stream, int bufferSize)
			: this(new ProgressStream(stream, CancellationToken.None), bufferSize)
		{
		}

		ProgressStreamContent(ProgressStream stream)
			: base(stream)
		{
			init(stream);
		}

		ProgressStreamContent(ProgressStream stream, int bufferSize)
			: base(stream, bufferSize)
		{
			init(stream);
		}

		void init(ProgressStream stream)
		{
			stream.ReadCallback = readBytes;

			Progress = delegate { };
		}

		void reset()
		{
			_totalBytes = 0L;
		}

		long _totalBytes;
		long _totalBytesExpected = -1;

		void readBytes(long bytes)
		{
			if (_totalBytesExpected == -1)
				_totalBytesExpected = Headers.ContentLength ?? -1;

			long computedLength;
			if (_totalBytesExpected == -1 && TryComputeLength(out computedLength))
				_totalBytesExpected = computedLength == 0 ? -1 : computedLength;

			// If less than zero still then change to -1
			_totalBytesExpected = Math.Max(-1, _totalBytesExpected);
			_totalBytes += bytes;

			Progress(bytes, _totalBytes, _totalBytesExpected);
		}

		ProgressDelegate _progress;
		public ProgressDelegate Progress
		{
			get { return _progress; }
			set
			{
				if (value == null) _progress = delegate { };
				else _progress = value;
			}
		}

		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			reset();
			return base.SerializeToStreamAsync(stream, context);
		}

		protected override bool TryComputeLength(out long length)
		{
			var result = base.TryComputeLength(out length);
			_totalBytesExpected = length;
			return result;
		}

		class ProgressStream : Stream
		{
			CancellationToken token;

			public ProgressStream(Stream stream, CancellationToken token)
			{
				ParentStream = stream;
				this.token = token;

				ReadCallback = delegate { };
				WriteCallback = delegate { };
			}

			public Action<long> ReadCallback { get; set; }

			public Action<long> WriteCallback { get; set; }

			public Stream ParentStream { get; private set; }

			public override bool CanRead { get { return ParentStream.CanRead; } }

			public override bool CanSeek { get { return ParentStream.CanSeek; } }

			public override bool CanWrite { get { return ParentStream.CanWrite; } }

			public override bool CanTimeout { get { return ParentStream.CanTimeout; } }

			public override long Length { get { return ParentStream.Length; } }

			public override void Flush()
			{
				ParentStream.Flush();
			}

			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				return ParentStream.FlushAsync(cancellationToken);
			}

			public override long Position
			{
				get { return ParentStream.Position; }
				set { ParentStream.Position = value; }
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				token.ThrowIfCancellationRequested();

				var readCount = ParentStream.Read(buffer, offset, count);
				ReadCallback(readCount);
				return readCount;
			}

			public override long Seek(long offset, SeekOrigin origin)
			{
				token.ThrowIfCancellationRequested();
				return ParentStream.Seek(offset, origin);
			}

			public override void SetLength(long value)
			{
				token.ThrowIfCancellationRequested();
				ParentStream.SetLength(value);
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
				token.ThrowIfCancellationRequested();
				ParentStream.Write(buffer, offset, count);
				WriteCallback(count);
			}

			public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				token.ThrowIfCancellationRequested();
				var linked = CancellationTokenSource.CreateLinkedTokenSource(token, cancellationToken);

				var readCount = await ParentStream.ReadAsync(buffer, offset, count, linked.Token);

				ReadCallback(readCount);
				return readCount;
			}

			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				token.ThrowIfCancellationRequested();

				var linked = CancellationTokenSource.CreateLinkedTokenSource(token, cancellationToken);
				var task = ParentStream.WriteAsync(buffer, offset, count, linked.Token);

				WriteCallback(count);
				return task;
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					ParentStream.Dispose();
				}
			}
		}
	}
}
