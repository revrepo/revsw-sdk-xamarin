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
		public const string POSTUrl = "http://193.124.114.46:3001/users";


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
