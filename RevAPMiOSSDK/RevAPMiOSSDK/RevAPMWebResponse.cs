/*************************************************************************
 *
 * REV SOFTWARE CONFIDENTIAL
 *
 * [2013] - [2015] Rev Software, Inc.
 * All Rights Reserved.
 *
 * NOTICE:  All information contained herein is, and remains
 * the property of Rev Software, Inc. and its suppliers,
 * if any.  The intellectual and technical concepts contained
 * herein are proprietary to Rev Software, Inc.
 * and its suppliers and may be covered by U.S. and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from Rev Software, Inc.
 */
using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace RevAPMiOSSDK
{
  sealed class RevAPMWebResponse 
    {
        public Stream Content;

        public HttpStatusCode StatusCode { get; set; }

        public WebHeaderCollection Headers;
        public Uri ResponseUri { get; set; }
        public string Method { get; set; }

        public string CharacterSet { get; set; }
        public string ContentEncoding { get; set; }
        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        public CookieCollection Cookies { get; set; }
        public bool IsFromCache { get; set; }
        public bool IsMutuallyAuthenticated { get; set; }
        public DateTime LastModifiet { get; set; }
        public Version ProtocolVersion { get; set; }
        public string Server { get; set; }
        public string StatusDescription { get; set; }
        public bool SupportHeaders { get; set; }



       // public string Accept { get { return GetValues("Accept", Headers); } }
       // public string Connection { get { return GetValues("Connection", Headers); } }
       //// public string ContentLength { get { return GetValues("Content-Length", Headers); } }
       //// public string ContentType { get { return GetValues("Content-Type", Headers); } }
       // public string Date { get { return GetValues("Date", Headers); } }
       // public string Expect { get { return GetValues("Expect", Headers); } }
       // public string IfModifiedSince { get { return GetValues("If-Modified-Since", Headers); } }
       // public string Referer { get { return GetValues("Referer", Headers); } }
       // public string TransfeEencoding { get { return GetValues("Transfer-Encoding", Headers); } }
       // public string UserAgent { get { return GetValues("User-Agent", Headers); } }


        public RevAPMWebResponse(NSUrlAsyncResult Nsresponse)
        {
            var response = (NSHttpUrlResponse)Nsresponse.Response;
            
            
            this.StatusCode = (System.Net.HttpStatusCode)(int)response.StatusCode;
            this.ResponseUri = response.Url.AbsoluteUrl;
           
            //content     
            if (Nsresponse.Data != null)
            {
                Content = new MemoryStream();
                using (Nsresponse.Data)
                {
                    Nsresponse.Data.AsStream().CopyTo(Content);
                    Content.Position = 0;
                }
            }
			

            this.ResponseUri = new Uri(response.Url.ToString());


            
            Headers = new WebHeaderCollection();
            if (response.AllHeaderFields.Count != 0)
            {
                SupportHeaders = true;

                if (Content != null && Content.Length != 0)               
                    ContentLength = Content.Length;

              
          
                foreach (var responseheader in response.AllHeaderFields)
                {
					

                    if (responseheader.Key.ToString() == "CharacterSet" || responseheader.Key.ToString() == "Character-Set" || responseheader.Key.ToString() == "character-set")
                        CharacterSet = responseheader.Value.ToString();
                   else if (responseheader.Key.ToString() == "ContentEncoding" || responseheader.Key.ToString() == "Content-Encoding" || responseheader.Key.ToString() == "content-encoding")
                        ContentEncoding = responseheader.Value.ToString();
                    else if (responseheader.Key.ToString() == "ContentType" || responseheader.Key.ToString() == "Content-Type" || responseheader.Key.ToString() == "content-type")
                    {
                        ContentType = string.IsNullOrEmpty(responseheader.Value.ToString()) ? response.MimeType.ToString() : responseheader.Value.ToString();                       
                    }                       
                    else if (responseheader.Key.ToString() == "Cookies" || responseheader.Key.ToString() == "cookies")
                    {
                        //TODO: think about that
                       // Cookies = new CookieCollection();
                    }
                    else if (responseheader.Key.ToString() == "IsFromCache" || responseheader.Key.ToString() == "Is-From-Cache" || responseheader.Key.ToString() == "isfromcache" || responseheader.Key.ToString() == "is-from-cache")
                        IsFromCache = bool.Parse(responseheader.Value.ToString());
                    else if (responseheader.Key.ToString() == "IsMutuallyAuthenticated" || responseheader.Key.ToString() == "ismutuallyauthenticated" || responseheader.Key.ToString() == "is-mutually-authenticated" || responseheader.Key.ToString() == "Is-Mutually-Authenticated")
                        IsMutuallyAuthenticated = bool.Parse(responseheader.Value.ToString());
                    else if (responseheader.Key.ToString() == "LastModified" || responseheader.Key.ToString() == "Last-Modified" || responseheader.Key.ToString() == "last-modified")
                        LastModifiet = DateTime.Parse(responseheader.Value.ToString());
                    else if (responseheader.Key.ToString() == "ProtocolVersion" || responseheader.Key.ToString() == "Protocol-Version" || responseheader.Key.ToString() == "protocol-version")
                    {
                        ProtocolVersion = !string.IsNullOrEmpty(responseheader.Value.ToString()) ? new Version(responseheader.Value.ToString()) : default(Version);
                        //ProtocolVersion = Version.Parse(responseheader.Value.ToString());
                    }                      
                    else if (responseheader.Key.ToString() == "Server" || responseheader.Key.ToString() == "server")
                        Server = responseheader.Value.ToString();
                    else if (responseheader.Key.ToString() == "StatusDescription" || responseheader.Key.ToString() == "Status-Description" || responseheader.Key.ToString() == "status-description")                   
                        StatusDescription = responseheader.Value.ToString();
                    
                      
                    else
                    {
                        try
                        {
                            Headers.Add(responseheader.Key.ToString(), responseheader.Value.ToString());
                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            throw ex;
                        }
                        
                    }

                }
            }
            
        }

    }
}
