using HtmlAgilityPack;
using RacerApp.Service;
using RacerMobileApp.Classes;
using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RacerMobileApp.Services
{
   public class HttpRequestService 
    {
		public static HttpClientHandler DefaultRacerHttpClientHandler = new Nuubit.SDK.NuubitMessageHandler();

		private static HttpClient RevClient;
        private static HttpClient DefaultClient = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = true });


		 static  HttpRequestService()
		{
			RevClient = new HttpClient(DefaultRacerHttpClientHandler);
		}

        public static async Task<TestResult> SendRequest(Session session, bool IsRevApmRequest)
        { 
			HttpResponseMessage response = null;
            var sw = new Stopwatch();
            long? length = 0;         
          
            try
                {

                           var client = IsRevApmRequest ? RevClient : DefaultClient;
                             
							client.DefaultRequestHeaders.Clear();
							client.DefaultRequestHeaders.Add("Connection", "keep-alive");
							client.DefaultRequestHeaders.Add("Keep-Alive", "600");
                           
							using(var request = new HttpRequestMessage() { Method = session.Method, RequestUri = session.Uri })
                            {
                                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(session.ContentType));

                            if (session.Payload > 0)
                            {
                                var content = await new StubDataGenerator().GetAsync(session.ContentType, session.Payload * 1024);
                                request.Content = new StringContent(content, UnicodeEncoding.UTF8, session.ContentType);
                            }

                                sw.Start();

                                response = await client.SendAsync(request);

                                sw.Stop();

                            if (response.IsSuccessStatusCode)
                            {
                                length += await CalculateResponseBufferSize(response);
                            }

                            if (session.LoadAllUrls)
                            {
                                var links = await ExtractAllLinks(session.Uri).ConfigureAwait(false);

                                if (links != null && links.Count > 0)
                                {
                                    length += await TryLoadAllPagesUrl(links, sw, session.Uri, client);
                                }
                            }


                         }
                  

                return new TestResult()
                {
                    DurationMs = sw.ElapsedMilliseconds,
                    StatusCode = (int)response.StatusCode,
					HasError = response.StatusCode == System.Net.HttpStatusCode.OK ? false : true,
                    ResponseSizeBytes = length,
                    Method = session.Method.Method
                };
               }
            catch (Exception e)
            {
             	Debug.WriteLine(e.Message);
                 return new TestResult()
				{
					DurationMs = sw.ElapsedMilliseconds,
					StatusCode = (int)HttpStatusCode.InternalServerError,
					HasError = true,
					ResponseSizeBytes = length,
					Method = session.Method.Method
				};
            
            }
            finally
            {
				if(response != null)
				{
					response.Dispose();
				}
            }
            
                
        }


        private static async Task<List<string>> ExtractAllLinks(Uri Uri)
        {
            var doc = new HtmlWeb();
            var content = await doc.LoadFromWebAsync(Uri, UnicodeEncoding.UTF8, null);


            var links = content.DocumentNode.Descendants("link")
                                                              .Select(a => a.GetAttributeValue("href", null))
                                                              .Where(u => !string.IsNullOrEmpty(u));

            var scripts = content.DocumentNode.Descendants("script")
                                                             .Select(a => a.GetAttributeValue("src", null))
                                                             .Where(u => !string.IsNullOrEmpty(u));
          

            var images = content.DocumentNode.Descendants("img")
                                             .Select(a => a.GetAttributeValue("src", null))
                                             .Where(u => !string.IsNullOrEmpty(u));


            var list = links.Concat(images).Concat(scripts);

            return list.ToList();

            
        }

        private static async Task<long?> CalculateResponseBufferSize(HttpResponseMessage response)
        {
			//var data = await response.Content.ReadAsStringAsync();
            var byteArray = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            return byteArray.Length;
        }


        private static bool IsUriAbsolute(string uri)
        {
            if (uri.StartsWith("http"))
                return true;
            else
                return false;
        }

        private static string MakeUriValid(string uriTail, Uri uriHost)
        {
            return string.Concat(uriHost, uriTail);
        }

        private static async Task<long?> LoadUrl(string Uri, HttpClient client, Stopwatch sw, long? length)
        {
          //  Debug.WriteLine("***" + Uri);
            try
            {
                    using (var request = new HttpRequestMessage() { RequestUri = new Uri(Uri), Method = HttpMethod.Get })
                    {
                        sw.Start();
                       using (var response = await client.SendAsync(request))
                       {
                        sw.Stop();

                        if (response.IsSuccessStatusCode)
                            return await CalculateResponseBufferSize(response);
                        else
                            Debug.WriteLine("***" + Uri);
                        return 0;

                       }
                    }
                   
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine("**From catch***" + Uri);
                return 0;
            }
           
        }

        private static async Task<long?> TryLoadAllPagesUrl(List<string> links, Stopwatch sw, Uri Uri, HttpClient client)
        {

            long? length = 0;

            foreach (var link in links)
            {
                

                if (IsUriAbsolute(link))
                {
                   length+= await LoadUrl(link, client, sw, length);
                }
                else
                {
                    var validLink = MakeUriValid(link, Uri);
                    length+= await LoadUrl(validLink, client, sw, length);
                }
            }
            return length;
        }

    }
}
