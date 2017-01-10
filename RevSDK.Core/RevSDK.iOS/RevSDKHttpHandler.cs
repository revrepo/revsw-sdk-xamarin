using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace RevSDK.iOS
{
	public class RevSDKHttpHandler: DelegatingHandler
	{
		public RevSDKHttpHandler(HttpMessageHandler innerHandler)
			: base(innerHandler)
		{
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			Console.WriteLine("Request:");
			Console.WriteLine(request.ToString());
            
            string URLString = request.RequestUri.ToString();
            
            var NSURLRequest = new NSMutableUrlRequest(new NSUrl(URLString), NSUrlRequestCachePolicy.ReloadRevalidatingCacheData, 20);

			if (request.Content != null)
			{
				var content = await request.Content.ReadAsStringAsync();
				Console.WriteLine(content);
				var NSContent = new NSString(content);
				var NSContentData = NSContent.Encode(NSStringEncoding.UTF8);

				NSURLRequest.Body = NSContentData;

				if (request.Content.Headers != null)
				{
					Console.WriteLine(request.Content.Headers);

					var responseHeadersCollection = request.Content.Headers;
					var keys = new object[responseHeadersCollection.Count()];
					var objects = new object[responseHeadersCollection.Count()];
					int i = 0;

					foreach (var pair in responseHeadersCollection)
					{
						Console.WriteLine("{0}={1}", pair.Key, pair.Value.ToArray()[0]);
						keys[i] = pair.Key;
						objects[i] = pair.Value.ToArray()[0];
						i += 1;
					}

					//var keys = new object[] { "Content-Type", "charset" };
					//var objects = new object[] { "application/json", "utf-8" };
					var dict = NSDictionary.FromObjectsAndKeys(objects, keys);
					NSURLRequest.Headers = dict;
					//NSURLRequest.Headers.SetValueForKey(new NSString("application/json"), new NSString("Content-Type"));
					//NSURLRequest.Headers.SetValueForKey(new NSString("utf-8"), new NSString("charset"));


					//NSURLRequest.Headers.SetValuesForKeysWithDictionary(dict2);
					Console.WriteLine(NSURLRequest.Headers);
					//NSURLRequest.H
				}
                
                
            }
            Console.WriteLine();
            
            
            
            if (request.Method != null)
            {
                NSURLRequest.HttpMethod = request.Method.ToString();
            }

            HttpResponseMessage response = new HttpResponseMessage();
			//var connectionDelegate = new MyDelegate();
			//NSUrlConnection.FromRequest(request, connectionDelegate);
			NSOperationQueue OpsQueue = new NSOperationQueue();
			OpsQueue.AddOperation(() =>
			{
				//MyMethod();
				//callback.Invoke(result);
			});
			//}; 
			var result = await NSUrlConnection.SendRequestAsync(NSURLRequest, OpsQueue);
			if (result.Response != null)
			{
				var dataString = result.Data.ToString();
				var content = new StringContent(dataString,Encoding.UTF8);
					response.Content = content;
			}
			//callback.Invoke((System.IAsyncResult)result);

			//return result;

			/*Console.WriteLine("Response:");
			Console.WriteLine(response.ToString());
			if (response.Content != null)
			{
				Console.WriteLine(await response.Content.ReadAsStringAsync());
			}
			Console.WriteLine();*/

			return response;
		}
	}

}
