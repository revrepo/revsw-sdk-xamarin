using RacerApp.Service;
using RacerMobileApp.Classes;
using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RacerMobileApp.Services
{
   public class HttpRequestService
    {
        public static HttpMessageHandler CustomMessageHandler { get; set; }
        public static async Task<TestResult> SendRequest(Uri Uri, int Payload, HttpMethod Method, string DataFormat, bool IsRevApmRequest)
        {
            HttpClient client;
            if (IsRevApmRequest)
            {
                DependencyService.Get<IMessageHandlerInitializer>().InitializeMessageHandler();
                client = new HttpClient(CustomMessageHandler);
            }
            else
            {
                client = new HttpClient();
            }

            using (client)
            {
                using (HttpRequestMessage requestMessage = new HttpRequestMessage() { Method = Method, RequestUri = Uri })
                {
                    requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(DataFormat));

                    if (Payload > 0)
                    {
                        var content = await new StubDataGenerator().GetAsync(DataFormat, Payload * 1024);

                        requestMessage.Content = new StringContent(content, UnicodeEncoding.UTF8, DataFormat);
                    }
                    
                    Stopwatch sw = new Stopwatch();

                    sw.Start();

                    using (var response = await client.SendAsync(requestMessage))
                    {
                        sw.Stop();

                        int length = 0;

                        if (response.IsSuccessStatusCode)
                        {
                            
                            var byteArray = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                            length = byteArray.Length;
                                                        
                        }

                        return new TestResult()
                        {
                            DurationMs = sw.ElapsedMilliseconds,
                            StatusCode = response.StatusCode,
                            HasError = (int)response.StatusCode==200? false : true,
                            ResponseSizeBytes = length
                        };
                    }

                    
                }


            }
        }
    }
}
