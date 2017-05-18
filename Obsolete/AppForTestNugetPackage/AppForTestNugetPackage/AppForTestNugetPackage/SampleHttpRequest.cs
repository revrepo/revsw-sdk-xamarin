
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppForTestNugetPackage
{
    public class SampleHttpRequest
    {
        public static HttpMessageHandler CustomMessageHandler;

        public static async Task<string> SendRevAPMHttpRequest()
        {
            using(var client = new HttpClient(CustomMessageHandler))
            {
                using (HttpRequestMessage message =  new HttpRequestMessage()
                    { Method = new HttpMethod("GET"), RequestUri = new Uri("https://www.revapm.com/")})
                {
                    using (var response = await client.SendAsync(message))
                    {
                        return response.StatusCode.ToString();
                    }

                }
            }
           
        }

    }
}

