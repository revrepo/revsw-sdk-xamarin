using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                
                var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                var dict = new NSMutableDictionary();
                foreach (var s in dic)
                {
                    NSString Key = new NSString(s.Key);
                    NSString Value = new NSString(s.Value);
                    dict.SetValueForKey(Key, Value);
                }
                
                var contentData = NSKeyedArchiver.ArchivedDataWithRootObject(dict);
                NSURLRequest.Body = contentData;
                if (request.Content.Headers != null)
                {
                    var headersString = request.Content.Headers.ToString();
                    Console.WriteLine(headersString);
                    
                    //var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(headersString);
                    
                    var dict2 = new NSMutableDictionary();
                    dict2.SetValueForKey(new NSString("Content - Type"), new NSString("application / json"));
                    dict2.SetValueForKey(new NSString("charset"), new NSString("utf-8"));
                    
                    
                    //NSURLRequest.Headers = dict2;
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
