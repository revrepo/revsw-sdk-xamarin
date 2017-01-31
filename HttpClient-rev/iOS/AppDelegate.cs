using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Foundation;
using ObjCRuntime;
using UIKit;

namespace WebViewTestForms.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App());
			NSUrlProtocol.RegisterClass(new Class(typeof(CustomNSUrlProtocol)));
			RevSDK.RevSDK.StartWithSDKKey("a1c4fa56-5945-4f42-b070-e1926641ba8e");  //mike

			return base.FinishedLaunching(app, options);
		}
	}

	public class CustomNSUrlConnectionDataDelegate : NSUrlConnectionDataDelegate
	{
		public NSUrlProtocol Handler { get; private set; }
		public CustomNSUrlConnectionDataDelegate(NSUrlProtocol protocol)
		{
			this.Handler = protocol;
		}
		public override void ReceivedResponse(NSUrlConnection connection, NSUrlResponse response)
		{
			this.Handler.Client.ReceivedResponse(this.Handler, response, NSUrlCacheStoragePolicy.NotAllowed);
		}
		public override void FinishedLoading(NSUrlConnection connection)
		{
			this.Handler.Client.FinishedLoading(this.Handler);
		}
		public override void ReceivedData(NSUrlConnection connection, NSData data)
		{
			this.Handler.Client.DataLoaded(this.Handler, data);
		}
		public override void FailedWithError(NSUrlConnection connection, NSError error)
		{
			this.Handler.Client.FailedWithError(this.Handler, error);
		}
	}

	public class CustomNSUrlProtocol : NSUrlProtocol
	{
		private NSUrlSessionDataTask Task;
		[Export("canInitWithRequest:")]
		public static bool canInitWithRequest(NSUrlRequest request)
		{
			return true;
		}
		[Export("canonicalRequestForRequest:")]
		public static new NSUrlRequest GetCanonicalRequest(NSUrlRequest request)
		{
			return request;
		}
		[Export("initWithRequest:cachedResponse:client:")]
		public CustomNSUrlProtocol(NSUrlRequest request, NSCachedUrlResponse cachedResponse, INSUrlProtocolClient client)
		: base(request, cachedResponse, client)
		{
		}

		public override void StartLoading()
		{

     		Task =  Session.CurrentSession.CreateDataTask(Request, (NSData data, NSUrlResponse response, NSError error) =>
			{
				if (response == null)
				{
					this.Client.FailedWithError(this, error);
				}
				else
				{
					this.Client.ReceivedResponse(this, response, NSUrlCacheStoragePolicy.Allowed);
					this.Client.DataLoaded(this, data);
					this.Client.FinishedLoading(this);
				}
			});

			Task.Resume();
		}

		public override void StopLoading()
		{
			if (Task != null)
			{
				Task.Cancel();
				Task = null;
			}
		}
	}

	public static class Session
	{
		private static NSUrlSession _instance;

		public static NSUrlSession CurrentSession
		{
			get
			{
				if (_instance == null)
				{
					_instance = NSUrlSession.FromConfiguration(NSUrlSessionConfiguration.DefaultSessionConfiguration, new NSUrlSessionDelegate(), null);
					//Set configuration for the NSUrlSession
				}
				return _instance;
			}
		}
	}
}
