//
// This file contains the sample code to use System.Net.HttpClient
// using the HTTP handler selected in the IDE UI (or given to mtouch)
//

using System.Reflection;
using System.Threading.Tasks;
using System.Net.Http;
using RevSDK.iOS;
using System.Json;
using System.Text;
using System;

namespace HttpClientSample
{
	public class NetHttp
	{
		AppDelegate ad;

		public NetHttp (AppDelegate ad)
		{
			this.ad = ad;
		}

		public async Task HttpSample (bool secure)
		{
			//var client = new HttpClient ();
			var client = new HttpClient(new RevSDKHttpHandler(new HttpClientHandler()));
			ad.HandlerType = typeof(HttpMessageInvoker).GetField("handler", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue (client).GetType ();
			ad.RenderStream (await client.GetStreamAsync (secure ? Application.SecureUrl : Application.UnsecureUrl));
		}

		public async Task PostSample(bool secure)
		{
			//var client = new HttpClient ();

			var client = new HttpClient(new RevSDKHttpHandler(new HttpClientHandler()));
			ad.HandlerType = typeof(HttpMessageInvoker).GetField("handler", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(client).GetType();

			var jsonObject = new JsonObject();
			jsonObject.Add("DocumentsCount", "1");
			jsonObject.Add("Id", Guid.NewGuid().ToString());
			jsonObject.Add("Name", Guid.NewGuid().ToString());
			jsonObject.Add("Description", "sample description");
			jsonObject.Add("Deleted", false);
			jsonObject.Add("UpdatedAt", "2017-01-08T12:27:20.1175366-05:00");

			var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

			var response = await client.PostAsync(Application.POSTUrl, content);
			await response.Content.ReadAsStreamAsync().ContinueWith(t =>
				  {
					  ad.RenderStream(t.Result);
				  });

		}
	}
}

