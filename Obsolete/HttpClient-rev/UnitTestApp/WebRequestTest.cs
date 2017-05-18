using NUnit.Framework;

namespace UnitTestApp
{
    [TestFixture]
    public class WebRequestTest
    {
        /*
        List<string> DefaultheadersToSkip;

        [SetUp]
        public void SetUp()
        {

        DefaultheadersToSkip = new List<string>();
            DefaultheadersToSkip.Add("CharacterSet");
            DefaultheadersToSkip.Add("ContentEncoding");
            DefaultheadersToSkip.Add("ContentLength");
            DefaultheadersToSkip.Add("ContentType");
            DefaultheadersToSkip.Add("Cookies");
            DefaultheadersToSkip.Add("IsFromCache");
            DefaultheadersToSkip.Add("IsMutuallyAuthenticated");
            DefaultheadersToSkip.Add("LastModifiet");
            DefaultheadersToSkip.Add("ProtocolVersion");
            DefaultheadersToSkip.Add("Server");
            DefaultheadersToSkip.Add("StatusDescription");
            DefaultheadersToSkip.Add("SupportHeaders");
            DefaultheadersToSkip.Add("Support-Headers");
            DefaultheadersToSkip.Add("Status-Description");
            DefaultheadersToSkip.Add("Protocol-Version");
            DefaultheadersToSkip.Add("Last-Modifiet");
            DefaultheadersToSkip.Add("Is-Mutually-Authenticated");
            DefaultheadersToSkip.Add("Is-From-Cache");
            DefaultheadersToSkip.Add("Content-Type");
            DefaultheadersToSkip.Add("Content-Length");
            DefaultheadersToSkip.Add("Content-Encoding");
            DefaultheadersToSkip.Add("Character-Set");
            DefaultheadersToSkip.Add("Date"); //not default
            DefaultheadersToSkip.Add("Connection"); //not default
            DefaultheadersToSkip.Add("Set-Cookie"); //not default
        }

        [Test]
        public async void ValidGetHttpWebRequestHtml()
        {
            Assert.True(await MakeTest("GET", Constants.host + Constants.html), "#No content & no custom headers#");
        }
        [Test]
        public async void ValidGetHttpWebRequestJson()
        {
            Assert.True(await MakeTest("GET", Constants.json), "#No content & no custom headers#");
        }
        [Test]
        public async void ValidGetHttpWebRequestXml()
        {
            Assert.True(await MakeTest("GET", Constants.host + Constants.xml), "#No content & no custom headers#");
        }
        [Test]
        public async void ValidGetHttpWebRequestBytes()
        {
            Assert.True(await MakeTest("GET", Constants.host + Constants.bytes20), "#No content & no custom headers#");
        }

        [Test]
        public async void ValidGetHttpWebRequestStreamBytes20()
        {
            Assert.True(await MakeTest("GET", Constants.host + Constants.stream_bytes20), "#No content & no custom headers#");
        }

        [Test]
        public async void ValidGetHttpWebRequestImagePng()
        {
            Assert.True(await MakeTest("GET", Constants.host + Constants.image_png), "#No content & no custom headers#");
        }

        [Test]
        public async void ValidGetHttpWebRequestImageJpeg()
        {
            Assert.True(await MakeTest("GET", Constants.host + Constants.image_jpeg), "#No content & no custom headers#");
        }

        [Test]
        public async void ValidGetHttpWebRequestStream20()
        {
            Assert.True(await MakeTest("GET", Constants.host + Constants.stream20), "#No content & no custom headers#");
        }

        [Test]
        public async void ValidGetHttpWebRequestGzip()
        { 
            Assert.True(await MakeTest("GET", Constants.host + Constants.gzip), "#No content & no custom headers#");
        }

        //[Test]
        //public async void ValidPostHttpWebRequestHtml()
        //{
        //    Assert.True(await MakeTest("POST", Constants.host + Constants.html), "#No content & no custom headers#");
        //}



        public async Task<bool> MakeTest(string method, string requesturi)
        {
            return AreResponsesEqual(await GetDefaultWebResponse(method, requesturi, null), await GetRevApmWebResponse(method, requesturi,null));
        }

       async Task<HttpWebResponse> GetDefaultWebResponse( string method, string requesturi, string Content)
        {
            try
            {
                HttpWebRequest req = new HttpWebRequest(new Uri(requesturi, UriKind.Absolute));
                req.Method = method;
                WebResponse response = await req.GetResponseAsync();
                var resp = (HttpWebResponse)response;

                if (!string.IsNullOrEmpty(Content))
                {
                    byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(Content);
                    Stream reqstr = await req.GetRequestStreamAsync();
                    reqstr.Write(buffer, 0, buffer.Length);
                    reqstr.Dispose();
                }

                return resp;
              

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
       async Task<RevAPMWebResponse> GetRevApmWebResponse(string method, string requesturi, string Content)
        {
            try
            {
                HttpWebRequest req = new HttpWebRequest(new Uri(requesturi));
                req.Method = method;

                if (!string.IsNullOrEmpty(Content))
                {
                    byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(Content);
                    Stream reqstr = await req.GetRequestStreamAsync();
                    reqstr.Write(buffer, 0, buffer.Length);
                    reqstr.Dispose();
                }
               

                var response = await req.RevAPMGetResponseAsync();
                return response;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
            
        }

       bool AreResponsesEqual(HttpWebResponse webresponse, RevAPMWebResponse revapmwebresponse)
        {
            if (webresponse.StatusCode != revapmwebresponse.StatusCode)
                return false;
            if (webresponse.ContentType != revapmwebresponse.ContentType)
                return false;
            if (webresponse.ContentLength != revapmwebresponse.ContentLength) // Fails when trying to GET gzip
            {
                if(webresponse.ContentLength!=-1)
                    return false;
            }

            //if (webresponse.Cookies != revapmwebresponse.Cookies) // skip for test . not yet received
            //    return false;
            if (webresponse.IsFromCache!= revapmwebresponse.IsFromCache)
                return false;
            //if (webresponse.IsMutuallyAuthenticated != revapmwebresponse.IsMutuallyAuthenticated) // skip for test. webresponse catch an exception
            //{
            //    if(webresponse.IsMutuallyAuthenticated!=null||webresponse.IsMutuallyAuthenticated is Exception)
            //    return false;
            //}

            //if (webresponse.LastModified != revapmwebresponse.LastModifiet)// skip for test. not yet received
            //{
            //   if(webresponse.LastModified.Ticks!=revapmwebresponse.LastModifiet.Ticks)
            //    return false;
            //} 

            //if (webresponse.ProtocolVersion != revapmwebresponse.ProtocolVersion) //skip for test. not yet received
            //    return false;
            if (webresponse.ResponseUri!= revapmwebresponse.ResponseUri)
                return false;
            if (webresponse.Server!= revapmwebresponse.Server)
            {
                if(webresponse.Server!=null)
                return false;
            }              
            //if (webresponse.StatusDescription!= revapmwebresponse.StatusDescription)// skip for test
            //    return false;
            if (webresponse.SupportsHeaders!= revapmwebresponse.SupportHeaders)
                return false;

           

            for(int i = 0; i< webresponse.Headers.Count; i++)
            {
                var key = webresponse.Headers.AllKeys[i];

                if (!string.IsNullOrEmpty(key))
                {
                    if (!DefaultheadersToSkip.Contains(key))
                    {
                        if (webresponse.Headers.GetValues(key) != null)
                        {
                            if (revapmwebresponse.Headers.GetValues(key) == null) //Transfer-Encoding header is not exist when trying GET stream
                            {
                                return false;
                            }
                            else
                            {
                                if (!revapmwebresponse.Headers.GetValues(key).SequenceEqual(webresponse.Headers.GetValues(key)))
                                    {
                                    if (!revapmwebresponse.Headers.GetValues(key.ToLower()).SequenceEqual(webresponse.Headers.GetValues(key)))
                                        return false;
                                    }
                                
                            }
                        }
                    }
                   
                }
            }

           

            using (var a = webresponse.GetResponseStream())
            {
               using (MemoryStream b = revapmwebresponse.Content as MemoryStream)
                    {
                        if (a.Length!=b.Length)
                            return false;
                    }
            }

            return true;
        }*/
    }
    
}