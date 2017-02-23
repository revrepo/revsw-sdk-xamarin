using System;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using RevApm;
using NUnit.Framework;

namespace UnitTestApp
{
    [TestFixture]
    public class RevAPMMessageHandlerTest
    {
        List<string> DefaultResponseHeadersToSkip;

        [SetUp]
        public void Setup()
        {
            DefaultResponseHeadersToSkip = new List<string>();
            DefaultResponseHeadersToSkip.Add("Age");
            DefaultResponseHeadersToSkip.Add("CacheControl");
            DefaultResponseHeadersToSkip.Add("Cache-Control");
            DefaultResponseHeadersToSkip.Add("Connection");
            DefaultResponseHeadersToSkip.Add("ConnectionClose");
            DefaultResponseHeadersToSkip.Add("Connection-Close");
            DefaultResponseHeadersToSkip.Add("Date");
            DefaultResponseHeadersToSkip.Add("ETag");
            DefaultResponseHeadersToSkip.Add("Location");
            DefaultResponseHeadersToSkip.Add("Pragma");
            DefaultResponseHeadersToSkip.Add("ProxyAuthenticate");
            DefaultResponseHeadersToSkip.Add("Proxy-Authenticate");
            DefaultResponseHeadersToSkip.Add("RetryAfter");
            DefaultResponseHeadersToSkip.Add("Retry-After");
            DefaultResponseHeadersToSkip.Add("Server");
            DefaultResponseHeadersToSkip.Add("Trailer");
            DefaultResponseHeadersToSkip.Add("TransferEncoding");
            DefaultResponseHeadersToSkip.Add("Transfer-Encoding");
            DefaultResponseHeadersToSkip.Add("TransferEncodingChunked");
            DefaultResponseHeadersToSkip.Add("Transfer-Encoding-Chunked");
            DefaultResponseHeadersToSkip.Add("Upgrade");
            DefaultResponseHeadersToSkip.Add("Vary");
            DefaultResponseHeadersToSkip.Add("Via");
            DefaultResponseHeadersToSkip.Add("Warning");
            DefaultResponseHeadersToSkip.Add("WwwAuthenticate");
            DefaultResponseHeadersToSkip.Add("Www-Authenticate");

        }


        #region Tests

        [Test]
        public async void ValidGetRequestHtmlTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(Constants.host + Constants.html), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestHtmlTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(Constants.host + Constants.html), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPutRequestHtmlTest()
        {
            Assert.True(await MakeTest(HttpMethod.Put, new Uri(Constants.host + Constants.html), null, null), "#No custom headers & no content#");
        }


        [Test]
        public async void ValidGetRequestXmlTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(Constants.host + Constants.xml), null, null), "#No custom headers & no content#");
        }
       

        [Test]
        public async void ValidPostRequestXmlTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(Constants.host + Constants.xml), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPutRequestXmlTest()
        {
            Assert.True(await MakeTest(HttpMethod.Put, new Uri(Constants.host + Constants.xml), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestJsonTest()
        {
			
            Assert.True( await MakeTest(HttpMethod.Get, new Uri(Constants.json), null, null),"#No custom headers & no content#");

        }

        [Test]
        public async void ValidPostRequestJsonTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(Constants.json), null, null), "#No custom headers & no content#");
        }


        [Test]
        public async void ValidPutRequestJsonTest()
        {
            Assert.True(await MakeTest(HttpMethod.Put, new Uri(Constants.json), null, null), "#No custom headers & no content#");
        }



        [Test]
        public async void ValidGetRequestImagePngTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host,Constants.image_png)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestImagePngTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.image_png)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestImageJpegTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.image_jpeg)), null, null), "#No custom headers & no content#");
        } 

        [Test]
        public async void ValidPostRequestImageJpegTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.image_jpeg)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestImageSvgTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.image_svg)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestImageSvgTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.image_svg)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestByteArrayTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.bytes20)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestByteArrayTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.bytes20)), null, null), "#No custom headers & no content#");
        }

  

        [Test]
        public async void ValidGetRequestStreamTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.stream20)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestStreamTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.stream20)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestDenyTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.deny)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestDenyTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.deny)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestCacheTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.cache)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestCacheTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.cache)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestCache20Test()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.cache20)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestCache20Test()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.cache20)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestStreamBytes20Test()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.stream_bytes20)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestStreamBytes20Test()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.stream_bytes20)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestImageWebpTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.image_webp)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestImageWebpTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.image_webp)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestUserAgentsTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.user_agent)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestUserAgentsTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.user_agent)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestHeaderTest()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.headers)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestHeadersTest()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.headers)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidGetRequestRedirect5Test()
        {
            Assert.True(await MakeTest(HttpMethod.Get, new Uri(string.Format(Constants.host, Constants.redirect5)), null, null), "#No custom headers & no content#");
        }

        [Test]
        public async void ValidPostRequestRedirect5Test()
        {
            Assert.True(await MakeTest(HttpMethod.Post, new Uri(string.Format(Constants.host, Constants.redirect5)), null, null), "#No custom headers & no content#");
        }


        #endregion
       
        private async Task<bool> MakeTest(HttpMethod method, Uri uri, HttpContent content, WebHeaderCollection headers)
        {
            // var handler = new RevAPMiOSHttpMessageHandler();
            var handler = new RevApmMessageHandler();
            var client_with_handler = new HttpClient(handler);
            var client_without_handler = new HttpClient();

            HttpRequestMessage message_with_handler = CreateMessage(method, uri, content, headers);
            HttpRequestMessage message_without_handler = CreateMessage(method, uri, content, headers);


            try
            {
               return AreResponsesEqual(await client_with_handler.SendAsync(message_with_handler), await client_without_handler.SendAsync(message_without_handler));
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
            finally
            {
                client_without_handler.Dispose();
                client_with_handler.Dispose(); 
                message_without_handler.Dispose();
                message_with_handler.Dispose();
                handler.Dispose();
            }

            
        }

       
        public HttpRequestMessage CreateMessage(HttpMethod method, Uri uri, HttpContent content, WebHeaderCollection headers)
        {
            HttpRequestMessage message = new HttpRequestMessage();
            message.Method = method;
            message.RequestUri = uri;
            message.Content = content;
            
            if (headers != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> header in headers)
                {
                    try
                    {
                        message.Headers.Add(header.Key, header.Value);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        try
                        {
                            message.Headers.TryAddWithoutValidation(header.Key, header.Value);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                            throw e;
                        }
                    }
                }
            }
            
            //TODO: ask about properties?

            return message;

        }


        // Total:  "SetCookie" + ContentLength sometimes fail

        private bool AreResponsesEqual(HttpResponseMessage message_with_handler, HttpResponseMessage message_without_handler)
        {
            if (message_without_handler.IsSuccessStatusCode != message_with_handler.IsSuccessStatusCode)
                return false;
            if (message_without_handler.ReasonPhrase.ToLower() != message_with_handler.ReasonPhrase.ToLower())            
                return false;                                   
            if (message_without_handler.StatusCode != message_with_handler.StatusCode)
                return false;
            if (message_without_handler.Version != message_with_handler.Version)
                return false;


            if (message_without_handler.Content != null)
            {
                if (!message_without_handler.Content.ReadAsByteArrayAsync().Result.SequenceEqual(message_with_handler.Content.ReadAsByteArrayAsync().Result))
                    return false;
            }


            if (message_without_handler.Content.Headers.ContentDisposition != message_with_handler.Content.Headers.ContentDisposition)
                    return false;
                if (message_without_handler.Content.Headers.ContentLength != message_with_handler.Content.Headers.ContentLength)
                    return false;
                if (message_without_handler.Content.Headers.ContentLocation != message_with_handler.Content.Headers.ContentLocation)
                    return false;
                if (message_without_handler.Content.Headers.ContentMD5 != message_with_handler.Content.Headers.ContentMD5)
                    return false;
                if (message_without_handler.Content.Headers.ContentRange != message_with_handler.Content.Headers.ContentRange)
                    return false;
                
                if (message_without_handler.Content.Headers.ContentType.CharSet != message_with_handler.Content.Headers.ContentType.CharSet ||
                    message_without_handler.Content.Headers.ContentType.MediaType!= message_with_handler.Content.Headers.ContentType.MediaType)
                    return false;
                if (message_without_handler.Content.Headers.Expires != message_with_handler.Content.Headers.Expires)
                    return false;
                if (message_without_handler.Content.Headers.LastModified != message_with_handler.Content.Headers.LastModified)
                    return false;
           

                //TODO: check custom content headers

      
            foreach (var header in message_without_handler.Headers.ToList())
            {
                if (DefaultResponseHeadersToSkip.Contains(header.Key))
                {
                    // check default headers

                    if (message_with_handler.Headers.Age != message_without_handler.Headers.Age)                   
                        return false;    
                    if (message_with_handler.Headers.CacheControl != message_without_handler.Headers.CacheControl)
                    {
                        
                        if (message_with_handler.Headers.CacheControl.NoCache != message_without_handler.Headers.CacheControl.NoCache) 
                            return false;
                        if (message_with_handler.Headers.CacheControl.Private != message_without_handler.Headers.CacheControl.Private)
                            return false;
                        if (message_with_handler.Headers.CacheControl.Public != message_without_handler.Headers.CacheControl.Public)
                            return false;
                        if (message_with_handler.Headers.CacheControl.NoStore != message_without_handler.Headers.CacheControl.NoStore)
                            return false;                     
                    }                        
                    if (message_with_handler.Headers.ConnectionClose != message_without_handler.Headers.ConnectionClose)
                        return false;
                    if (message_with_handler.Headers.ETag != message_without_handler.Headers.ETag)
                        return false;
                    if (message_with_handler.Headers.Location != message_without_handler.Headers.Location)
                        return false;
                    if (message_with_handler.Headers.RetryAfter != message_without_handler.Headers.RetryAfter)
                        return false;
                    if (message_with_handler.Headers.TransferEncoding != message_without_handler.Headers.TransferEncoding)
                    {
                       // if(message_with_handler.Headers.TransferEncoding.Count!= message_without_handler.Headers.TransferEncoding.Count)                        
                        	//return false;
                    }
                                     
                }
                else
                {
                    if (message_with_handler.Headers.Contains(header.Key))
                    {
                        if (!message_with_handler.Headers.GetValues(header.Key).SequenceEqual(header.Value))
                            return false;
                    }
                    else
                    {
                        if(header.Key!="Set-Cookie")
                        return false; //"SetCookie" header is not exist
                    }
                }
            }

          
            if (message_with_handler.RequestMessage != message_without_handler.RequestMessage)
            {
                if (message_with_handler.RequestMessage.Content != message_without_handler.RequestMessage.Content)
                    return false;
                if (message_with_handler.RequestMessage.Method != message_without_handler.RequestMessage.Method)
                    return false;
                if (message_with_handler.RequestMessage.RequestUri != message_without_handler.RequestMessage.RequestUri)
                    return false;
                if(message_with_handler.RequestMessage.Version != message_without_handler.RequestMessage.Version)
                    return false;

            }

            return true;

        }

        //[Test]
        //public void Disposed()
        //{
        //    var h = new RevAPMHttpMessageHandler(new iOSRevAPMHttpProcessor());
        //    h.Dispose();
        //    var c = new HttpClient(h);
        //    try
        //    {
        //        c.GetAsync("http://google.com").Wait();
        //        Assert.Fail("#1");
        //    }
        //    catch (AggregateException e)
        //    {
        //        Assert.IsTrue(e.InnerException is ObjectDisposedException, "#2");
        //    }
        //}
    }
}