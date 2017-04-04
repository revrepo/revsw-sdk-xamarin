using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading;

namespace RevApm
{
    public class RevApmMessageHandler : HttpClientHandler
    {
        const string wrongVersion = "You're referencing the Portable version in your App - you need to reference the platform (iOS/Android) version";

        public bool DisableCaching { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="ModernHttpClient.Portable.NativeMessageHandler"/> class.
        /// </summary>
        public RevApmMessageHandler() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="ModernHttpClient.Portable.NativeMessageHandler"/> class.
        /// </summary>
        /// <param name="throwOnCaptiveNetwork">If set to <c>true</c> throw on
        /// captive network (ie: a captive network is usually a wifi network
        /// where an authentication html form is shown instead of the real
        /// content).</param>
        /// <param name="customSSLVerification">Enable custom SSL certificate 
        /// verification via ServicePointManager. Disabled by default for 
        /// performance reasons (i.e. the OS default certificate verification 
        /// will take place)</param>
        /// <param name="cookieHandler">Enable native cookie handling.
        /// </param>
        public RevApmMessageHandler(bool throwOnCaptiveNetwork, bool customSSLVerification, NativeCookieHandler cookieHandler = null) : base()
        {
        }

        public void RegisterForProgress(HttpRequestMessage request, ProgressDelegate callback)
        {
            throw new Exception(wrongVersion);
        }
    }

    public class ProgressStreamContent : StreamContent
    {
        const string wrongVersion = "You're referencing the Portable version in your App - you need to reference the platform (iOS/Android) version";

        public ProgressStreamContent(Stream stream, CancellationToken token)
           : this(new ProgressStream(stream, token))
        {
        }


        ProgressStreamContent(Stream stream) : base(stream)
        {
            throw new Exception(wrongVersion);
        }

        ProgressStreamContent(Stream stream, int bufferSize) : base(stream, bufferSize)
        {
            throw new Exception(wrongVersion);
        }

        public ProgressDelegate Progress
        {
            get { throw new Exception(wrongVersion); }
            set { throw new Exception(wrongVersion); }
        }
    }

    public delegate void ProgressDelegate(long bytes, long totalBytes, long totalBytesExpected);

    public class NativeCookieHandler
    {
        const string wrongVersion = "You're referencing the Portable version in your App - you need to reference the platform (iOS/Android) version";

        public void SetCookies(IEnumerable<Cookie> cookies)
        {
            throw new Exception(wrongVersion);
        }

        public List<Cookie> Cookies
        {
            get { throw new Exception(wrongVersion); }
        }
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

