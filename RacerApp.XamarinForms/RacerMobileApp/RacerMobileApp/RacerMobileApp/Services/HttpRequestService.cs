﻿using HtmlAgilityPack;
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
        public static async Task<TestResult> SendRequest(Uri Uri, int Payload, HttpMethod Method, string DataFormat, bool LoadAllPageUrls, bool IsRevApmRequest)
        {
           

            try
            {
                var handler = IsRevApmRequest ?
                                               DependencyService.Get<IMessageHandlerInitializer>().InitializeMessageHandler()
                                               : 
                                               new HttpClientHandler() { AllowAutoRedirect = true };

                using (handler)
                {                   
                    using (HttpClient client = new HttpClient(handler))
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
                                
                                long? length = 0;

                                
                                if (LoadAllPageUrls)
                                {
                                    
                                    var links = await ExtractAllLinks(Uri); 



                                    if (links != null && links.Count > 0)
                                    {
                                        foreach (var link in links)
                                        {
                                            if (link.StartsWith("http"))
                                            {
                                                try
                                                {
                                                    sw.Start();
                                                    using (var resp = await client.SendAsync(new HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri(link) }))
                                                    {
                                                        sw.Stop();
                                                        if (resp.IsSuccessStatusCode)
                                                        {
                                                            var byteArray = await resp.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                                                            length += byteArray.Length;                                                         
                                                        }
                                                       
                                                    }

                                                }
                                                catch (Exception e)
                                                {
                                                    Debug.WriteLine(e.Message + "   *link = " + link + "  *IsRevApm = " + IsRevApmRequest);
                                                }
                                            }
                                            else 
                                            {
                                                try
                                                {
                                                    var imageUri = Uri + link;

                                                    sw.Start();
                                                    using (var resp = await client.SendAsync(new HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri(imageUri) }))
                                                    {
                                                        sw.Stop();
                                                        if (resp.IsSuccessStatusCode)
                                                        {
                                                            var byteArray = await resp.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                                                            length += byteArray.Length;
                                                         
                                                        }                                                   
                                                    }
                                                }
                                                catch(Exception e)
                                                {
                                                    Debug.WriteLine(e.Message);
                                                }
                                            }
                                        }
                                    }
                                   
                                }

                               

                                if (response.IsSuccessStatusCode)
                                {
                                    var byteArray = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                                    length += byteArray.Length;                                 
                                }



                                return new TestResult()
                                {
                                    DurationMs = sw.ElapsedMilliseconds,
                                    StatusCode = response.StatusCode,
                                    HasError = (int)response.StatusCode == 200 ? false : true,
                                    ResponseSizeBytes = length,
                                    Method = Method.Method
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
           
        }

      

        private static async Task<List<string>> ExtractAllLinks(Uri Uri)
        {
            var doc = new HtmlWeb();
            var x = await doc.LoadFromWebAsync(Uri, UnicodeEncoding.UTF8, null);


            var links = x.DocumentNode.Descendants("link")
                                                              .Select(a => a.GetAttributeValue("href", null))
                                                              .Where(u => !string.IsNullOrEmpty(u));

            var scripts = x.DocumentNode.Descendants("script")
                                                             .Select(a => a.GetAttributeValue("src", null))
                                                             .Where(u => !string.IsNullOrEmpty(u));
          

            var images = x.DocumentNode.Descendants("img")
                                             .Select(a => a.GetAttributeValue("src", null))
                                             .Where(u => !string.IsNullOrEmpty(u));


            var list = links.Concat(images).Concat(scripts);

            return list.ToList();

            
        }

      
    }
}
