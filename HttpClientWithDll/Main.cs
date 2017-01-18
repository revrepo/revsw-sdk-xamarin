//
// This sample shows how to use the two Http stacks in MonoTouch:
// The System.Net.WebRequest.
// The MonoTouch.Foundation.NSMutableUrlRequest
//

using UIKit;

namespace HttpClientSample
{
	public class Application
	{
		// URL where we fetch the wisdom from
		public const string SecureUrl = "https://www.gmail.com";
		public const string UnsecureUrl = "http://www.cnn.com";
		public const string POSTUrl = "http://jabos.com/api/Tags";
		public const string PUTUrl = "http://jabos.com/api/Tags/757f4078-5ed8-4f29-a66c-cbfec6a0fbc6";
		public const string OPTIONSUrl = "http://jabos.com/api/Nodes";
		public const string AuthUrl = "https://echo.getpostman.com/basic-auth";


		static void Main (string[] args)
		{
			UIApplication.Main (args);
		}

		public static void Busy ()
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
		}

		public static void Done ()
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
		}

	}

}
