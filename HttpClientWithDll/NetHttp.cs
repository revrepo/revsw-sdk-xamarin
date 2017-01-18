using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Net.Http;
using System.Json;
using System.Text;
using RevSDK.iOS;
using System.Net.Http.Headers;

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
		public async Task PutSample(bool secure)
		{
			//var client = new HttpClient ();
			var client = new HttpClient(new RevSDKHttpHandler(new HttpClientHandler()));
			ad.HandlerType = typeof(HttpMessageInvoker).GetField("handler", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(client).GetType();

			var jsonObject = new JsonObject();
			jsonObject.Add("DocumentsCount", "1");
			jsonObject.Add("Id", "757f4078-5ed8-4f29-a66c-cbfec6a0fbc6");
			jsonObject.Add("Name", Guid.NewGuid().ToString());
			jsonObject.Add("Description", "sample description");
			jsonObject.Add("Deleted", false);
			jsonObject.Add("UpdatedAt", "2017-01-08T12:27:20.1175366-05:00");

			var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

			var response = await client.PutAsync(Application.PUTUrl, content);
			await response.Content.ReadAsStreamAsync().ContinueWith(t =>
				  {
					  ad.RenderStream(t.Result);
				  });

		}

		public async Task DeleteSample(bool secure)
		{
			//var client = new HttpClient ();
			var client = new HttpClient(new RevSDKHttpHandler(new HttpClientHandler()));
			ad.HandlerType = typeof(HttpMessageInvoker).GetField("handler", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(client).GetType();

			var response = await client.DeleteAsync(Application.PUTUrl);
			await response.Content.ReadAsStreamAsync().ContinueWith(t =>
				  {
					  ad.RenderStream(t.Result);
				  });

		}

		public async Task OPTIONSSample(bool secure)
		{
			//var client = new HttpClient ();

			var client = new HttpClient(new RevSDKHttpHandler(new HttpClientHandler()));
			ad.HandlerType = typeof(HttpMessageInvoker).GetField("handler", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(client).GetType();

			var request = new HttpRequestMessage()
			{
				RequestUri = new Uri(Application.OPTIONSUrl),
				Method = HttpMethod.Options,
			};
			request.Headers.Add("Access-Control-Request-Headers", "accept, content-type");
			request.Headers.Add("Access-Control-Request-Method", "POST");
			request.Headers.Add("Origin", "http://jabos.stage.sharp-dev.net");

			var response = await client.SendAsync(request);
			await response.Content.ReadAsStreamAsync().ContinueWith(t =>
				  {
					  ad.RenderStream(t.Result);
				  });

		}

		public async Task AuthSample(bool secure)
		{
			//var client = new HttpClient ();

			var client = new HttpClient(new RevSDKHttpHandler(new HttpClientHandler()));
			ad.HandlerType = typeof(HttpMessageInvoker).GetField("handler", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(client).GetType();

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "postman", "password"))));

			var response = await client.GetAsync(Application.AuthUrl);
			if (response.Content.Headers != null)
			{
				ad.HeadersString = "CHeaders:\n" + response.Content.Headers.ToString();
			}
			if (response.Headers != null)
			{
				ad.HeadersString += response.Headers.ToString();
			}

			await response.Content.ReadAsStreamAsync().ContinueWith(t =>
				  {
					  ad.RenderStream(t.Result);
				  });

		}

	}
}

