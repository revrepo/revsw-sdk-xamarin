//
// This file contains the sample code to use System.Net.WebRequest
// on the iPhone to communicate with HTTP and HTTPS servers
//
// Author:
//   Miguel de Icaza
//

using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.IO;

namespace HttpClientSample
{
	public class DotNet {
		AppDelegate ad;

		public DotNet (AppDelegate ad)
		{
			this.ad = ad;
		}

		//
		// Asynchronous HTTP request
		//
		public void HttpSample ()
		{
			Console.WriteLine("HttpSample");
			Application.Busy ();
			HttpWebRequest.RegisterPrefix("http://", new WebRequestCreator());
			var request = HttpWebRequest.Create(Application.WisdomUrl);//WebRequest.Create (Application.WisdomUrl);
			//request.GetResponseAsync
			request.BeginGetResponse (FeedDownloaded, request);

			/*try
			{
				string _url = "http://mydomain/Api/Log/";

				string str = @"23122016";
				System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
				byte[] arr = encoding.GetBytes(str);
				WebRequest.RegisterPrefix("http://", new WebRequestCreator());

				WebRequest request = WebRequest.Create(_url);
				//request.AllowWriteStreamBuffering = false;
				//request.Method = "POST";

				//request.ContentType = "application/json";
				//request.ContentLength = arr.Length;

				//Stream dataStream = request.GetRequestStream();
				//dataStream.Write(arr, 0, arr.Length);
				//dataStream.Close();
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				string returnString = response.StatusCode.ToString();
			}
			catch (WebException ex)
			{
				Console.WriteLine("http errror", ex.Message);
			}*/

		}

		//
		// Invoked when we get the stream back from the twitter feed
		// We parse the RSS feed and push the data into a
		// table.
		//
		void FeedDownloaded (IAsyncResult result)
		{
			Application.Done ();
			var request = result.AsyncState as HttpWebRequest;

			try {
				var response = request.EndGetResponse (result);
				ad.RenderStream (response.GetResponseStream ());
			} catch (Exception e) {
				Debug.WriteLine (e);
			}
		}

		//
		// Asynchornous HTTPS request
		//
		public void HttpSecureSample ()
		{

			WebRequest.RegisterPrefix("https://", new WebRequestCreator());
			var https = (HttpWebRequest) WebRequest.Create ("http://www.e1.ru");
			//
			// To not depend on the root certficates, we will
			// accept any certificates:
			//
			ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, ssl) =>  true;

			https.BeginGetResponse (GmailDownloaded, https);
		}

		//
		// This sample just gets the result from calling
		// https://gmail.com, an HTTPS secure connection,
		// we do not attempt to parse the output, but merely
		// dump it as text
		//
		void GmailDownloaded (IAsyncResult result)
		{
			Application.Done ();
			var request = result.AsyncState as HttpWebRequest;

			try {
            		var response = request.EndGetResponse (result);
				ad.RenderStream (response.GetResponseStream ());
			} catch {
				// Error
			}
		}

		//
		// For an explanation of this AcceptingPolicy class, see
		// http://mono-project.com/UsingTrustedRootsRespectfully
		//
		// This will not be needed in the future, when MonoTouch
		// pulls the certificates from the iPhone directly
		//
		class AcceptingPolicy : ICertificatePolicy {
			public bool CheckValidationResult (ServicePoint sp, X509Certificate cert, WebRequest req, int error)
			{
				// Trust everything
				return true;
			}
		}
	}
}
