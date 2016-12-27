using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Reflection;

using Foundation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.IO;
using UIKit;
using System.Runtime.InteropServices;


namespace HttpClientSample
{
	public class MyWebRequest : WebRequest
	{
		private Uri _uri;

		public override string Method
		{
			get { 
				return base.Method; 
			}
			set {
				base.Method = value; 
			}
		}

		public delegate string AsyncMethodCaller(string URLString, out int threadId);

		public MyWebRequest(Uri uri) //: base(uri)
		{
			Console.WriteLine("My class!!!");

			_uri = uri;
		}

		public override Uri RequestUri { get { return _uri; } }

		public override WebResponse GetResponse()
		{

			WebResponse response = null;

			return response;

		}

		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			Console.WriteLine("Overrided!!!");

			//int threadId;
			//IAsyncResult result = null;
			var request = new NSMutableUrlRequest(new NSUrl(_uri.ToString()), NSUrlRequestCachePolicy.ReloadRevalidatingCacheData, 20);

			//var connectionDelegate = new MyDelegate();
			//NSUrlConnection.FromRequest(request, connectionDelegate);
			NSOperationQueue OpsQueue = new NSOperationQueue();
			OpsQueue.AddOperation(() =>
			{
				MyMethod();
				//callback.Invoke(result);
			});
		//}; 
			Task<NSUrlAsyncResult> result =  NSUrlConnection.SendRequestAsync(request, OpsQueue);
			//callback.Invoke((System.IAsyncResult)result);
			return result;



			//AsyncMethodCaller caller = new AsyncMethodCaller(ConnectMethod);

			// Initiate the asychronous call.
			//result = caller.BeginInvoke(3000, out threadId, null, null);

			/*Thread.Sleep(0);
			Console.WriteLine("Main thread {0} does some work.",
				Thread.CurrentThread.ManagedThreadId);*/

			// Wait for the WaitHandle to become signaled.
			//result.AsyncWaitHandle.WaitOne();

			// Perform additional processing here.
			// Call EndInvoke to retrieve the results.
			//string returnValue = caller.EndInvoke(out threadId, result);

			// Close the wait handle.
			//result.AsyncWaitHandle.Close();
			//result.IsCompleted = 
			//return result; //base.BeginGetResponse(callback, state);
		}



		public string MMethod(string URLString, out int threadId)
		{
			Console.WriteLine("Performing request with NSURL");

			var request = new NSMutableUrlRequest(new NSUrl(URLString), NSUrlRequestCachePolicy.ReloadRevalidatingCacheData, 20);

			var connectionDelegate = new MyDelegate();
			NSUrlConnection.FromRequest(request, connectionDelegate);


			/*var connection = NSUrlConnection.FromRequest(request, new MyDelegate((body) =>
			{
				//completed();
				request.Dispose();
			}, (reason) =>
			{
				Console.WriteLine("upload failed: " + reason);
				//completed();
			}));*/

			//var OpsQueue = new NSOperationQueue();
			//OpsQueue.
			//Task<NSUrlAsyncResult> result = connection.SendRequestAsync(request, OpsQueue);


			//Thread.Sleep(callDuration);
			threadId = Thread.CurrentThread.ManagedThreadId;
			return null;//connectionDelegate.ResponseContent; //String.Format("My call time was {0}.", callDuration.ToString());
		}

		public void MyMethod()
		{
		 			Console.WriteLine("Operation ended");


		}

	}

	class WebRequestCreator : IWebRequestCreate
	{

		public WebRequest Create(Uri uri)
		{
			return new MyWebRequest(uri);
		}
	}

	public class MyDelegate : NSUrlConnectionDataDelegate
	{
		//Action<string> success_callback;
		//Action<string> failure_callback;
		public NSMutableData data { get; set; }
		int status_code;

		public MyDelegate(/*Action<string> success, Action<string> failure*/)
		{
			//success_callback = success;
			//failure_callback = failure;
			data = new NSMutableData();
		}

		public override void ReceivedData(NSUrlConnection connection, NSData d)
		{
			data.AppendData(d);
		}

		public override void ReceivedResponse(NSUrlConnection connection, NSUrlResponse response)
		{
			var http_response = response as NSHttpUrlResponse;
			if (http_response == null)
			{
				Console.WriteLine("Received non HTTP url response: '{0}'", response);
				status_code = -1;
				return;
			}

			status_code = (int)http_response.StatusCode;
			Console.WriteLine("Status code of result:   '{0}'", status_code);
		}

		public override void FailedWithError(NSUrlConnection connection, NSError error)
		{
			//if (failure_callback != null)
				//failure_callback(error.LocalizedDescription);
		}

		public override void FinishedLoading(NSUrlConnection connection)
		{
			if (status_code != 200)
			{
				//failure_callback(string.Format("Did not receive a 200 HTTP status code, received '{0}'", status_code));
				return;
			}

			//success_callback(data.ToString());
		}
	}
}

