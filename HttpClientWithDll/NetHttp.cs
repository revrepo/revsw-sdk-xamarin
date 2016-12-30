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
using System.IO;

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
			jsonObject.Add("username","John Smith");
			jsonObject.Add("password","johnpwd");
			jsonObject.Add("email","john@smith.foo");

			var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

			var response = await client.PostAsync(Application.POSTUrl, content);
			await response.Content.ReadAsStreamAsync().ContinueWith(t =>
				  {
					  ad.RenderStream(t.Result);
				  });

		}
	}
}

