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
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using System.IO;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;

namespace RevAPMiOSSDK
{
    public class iOSRevAPMHttpProcessor: RevAPM.IRevAPMHttpProcessor
    {

        private List<string> ContentHeadersToSkip;
        private TimeSpan? TimeOut { get; set; }

        public iOSRevAPMHttpProcessor()
        {

            ContentHeadersToSkip = new List<string>();
            ContentHeadersToSkip.Add("Allow");
            ContentHeadersToSkip.Add("Content-Disposition");
            ContentHeadersToSkip.Add("Content-Encoding");
            ContentHeadersToSkip.Add("Content-Language");
            ContentHeadersToSkip.Add("Content-Length");
            ContentHeadersToSkip.Add("ContentLength");
            ContentHeadersToSkip.Add("Content-Location");
            ContentHeadersToSkip.Add("Content-MD5");
            ContentHeadersToSkip.Add("Content-Range");
            ContentHeadersToSkip.Add("Content-Type");
            ContentHeadersToSkip.Add("Expires");
            ContentHeadersToSkip.Add("Last-Modified");
            ContentHeadersToSkip.Add("allow");
            ContentHeadersToSkip.Add("content-md5");
            ContentHeadersToSkip.Add("expires");

        }

        public iOSRevAPMHttpProcessor(TimeSpan? Timeout)
        {

            ContentHeadersToSkip = new List<string>();
            ContentHeadersToSkip.Add("Allow");
            ContentHeadersToSkip.Add("Content-Disposition");
            ContentHeadersToSkip.Add("Content-Encoding");
            ContentHeadersToSkip.Add("Content-Language");
            ContentHeadersToSkip.Add("Content-Length");
            ContentHeadersToSkip.Add("ContentLength");
            ContentHeadersToSkip.Add("Content-Location");
            ContentHeadersToSkip.Add("Content-MD5");
            ContentHeadersToSkip.Add("Content-Range");
            ContentHeadersToSkip.Add("Content-Type");
            ContentHeadersToSkip.Add("Expires");
            ContentHeadersToSkip.Add("Last-Modified");
            ContentHeadersToSkip.Add("allow");
            ContentHeadersToSkip.Add("content-md5");
            ContentHeadersToSkip.Add("expires");

            this.TimeOut = TimeOut;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            TimeSpan _timeout = TimeOut != null ? TimeOut.Value : new TimeSpan(0, 0, 30);
           
            NSMutableUrlRequest nsRequest = new NSMutableUrlRequest(request.RequestUri, NSUrlRequestCachePolicy.ReloadIgnoringCacheData, _timeout.Seconds);

            nsRequest.HttpMethod = request.Method.Method.ToString();

            //get headers
            var headerKeys = new List<object>();
            var headerValues = new List<object>();


            foreach (var header in request.Headers)
            {
                try
                {
                    headerKeys.Add(header.Key);
                    headerValues.Add(string.Join(",", header.Value));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("key: " + header.Key + ", value: " + string.Join("*,*", header.Value));
                    Debug.WriteLine(ex.Message);
                    throw new ArgumentException("value", ex);
                }


            }

            var headers = NSDictionary.FromObjectsAndKeys(headerValues.ToArray(), headerKeys.ToArray());

            nsRequest.Headers = headers;


            //TODO: WHERE IS MultipartContent, MultipartFormDataContent
            if (request.Content != null)
            {
                try
                {
                    if (request.Content is StringContent)
                        nsRequest.Body = NSData.FromString(await request.Content.ReadAsStringAsync().ConfigureAwait(false));

                    else if (request.Content is FormUrlEncodedContent)
                        nsRequest.Body = NSData.FromString(await request.Content.ReadAsStringAsync().ConfigureAwait(false));

                    else if (request.Content is StreamContent)
                        nsRequest.Body = NSData.FromStream(await request.Content.ReadAsStreamAsync().ConfigureAwait(false));

                    else if (request.Content is ByteArrayContent)
                        nsRequest.Body = NSData.FromArray(await request.Content.ReadAsByteArrayAsync().ConfigureAwait(false));

                    else if (request.Content is MultipartFormDataContent)
                    {

                    }
                    else if (request.Content is MultipartFormDataContent)
                    {

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw ex;
                }

            }





            var nsUrlAsyncResult = await NSUrlConnection.SendRequestAsync(nsRequest, new NSOperationQueue()).ConfigureAwait(false);

            var result = new HttpResponseMessage();

            var nsHttpUrlResponse = (NSHttpUrlResponse)nsUrlAsyncResult.Response;

            result.StatusCode = (System.Net.HttpStatusCode)(int)nsHttpUrlResponse.StatusCode;
            result.RequestMessage = request;

            if (nsUrlAsyncResult.Data != null)
            {

                var Content = new MemoryStream();

                using (nsUrlAsyncResult.Data)
                {
                    nsUrlAsyncResult.Data.AsStream().CopyTo(Content);
                    Content.Position = 0;
                    result.Content = new StreamContent(Content);
                }


            }

            for (int i = 0; i < nsHttpUrlResponse.AllHeaderFields.Keys.Length; i++)
            {
                try
                {
                    string key = nsHttpUrlResponse.AllHeaderFields.Keys[i].ToString();
                    object value = (nsHttpUrlResponse.AllHeaderFields.Values[i] is NSString) ? nsHttpUrlResponse.AllHeaderFields.Values[i] : null;

                    if (key != null && value != null)
                    {

                        if (!ContentHeadersToSkip.Contains(key))
                        {
                            TryAddHeadersToResponse(result.Headers, key, value);
                        }
                        else
                        {
                            TryAddHeadersToContent(result.Content.Headers, key, value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw ex;
                }


            }



            return result;

        }

        private void TryAddHeadersToContent(HttpContentHeaders responseheaders, string key, object value)
        {

            if (key == "Allow" || key == "allow")
            {
                //Do nothing, because header property has only get method
            }
            else if (key == "Content-Encoding" || key == "content-encoding" || key == "ContentEncoding")
            {
                //Do nothing, because header property has only get method
            }
            else if (key == "Content-Language" || key == "contentlanguage" || key == "ContentLanguage")
            {
                //Do nothing, because header property has only get method
            }
            else if (key == "Content-Disposition" || key == "contentdisposition" || key == "ContentDisposition")
            {
                responseheaders.ContentDisposition = new ContentDispositionHeaderValue(value.ToString());
            }
            else if (key == "Content-Length" || key == "contentlength" || key == "ContentLength")
            {
                if (responseheaders.ContentLength != 0 && responseheaders.ContentLength != null)
                    responseheaders.ContentLength = long.Parse(value.ToString());
            }
            else if (key == "Content-Location" || key == "contentlocation" || key == "ContentLocation")
            {
                responseheaders.ContentLocation = new Uri(value.ToString());
            }
            else if (key == "Content-MD5" || key == "content-md5" || key == "ContentMD5")
            {
                //TODO
            }
            else if (key == "Content-Range" || key == "contentrange" || key == "ContentRange")
            {
                responseheaders.ContentRange = new ContentRangeHeaderValue(long.Parse(value.ToString()));
            }
            else if (key == "Content-Type" || key == "contenttype" || key == "ContentType")
            {
                responseheaders.ContentType = MediaTypeHeaderValue.Parse(value.ToString());
            }
            else if (key == "Expires" || key == "expires")
            {
                //TODO: it sometimes contains value but should be null
                //responseheaders.Expires = DateTimeOffset.Parse(value.ToString());
            }
            else if (key == "Last-Modifiet" || key == "lastmodifiet" || key == "LastModifiet")
            {
                //TODO: it sometimes contains value but should be null
                //responseheaders.LastModified = DateTimeOffset.Parse(value.ToString());
            }
            else
            {
                responseheaders.Add(key, value.ToString());
            }



        }

        private void TryAddHeadersToResponse(HttpResponseHeaders responseheaders, string key, object value)
        {

            //TODO: it sometimes contains value but should be null
            if (key == "Age" || key == "age")
            {
                if (value == null || value.ToString() == "0")
                {
                    responseheaders.Age = null;
                }
                else
                {
                    responseheaders.Age = TimeSpan.Parse(value.ToString());
                }

            }
            else if (key == "Cache-Control" || key == "CacheControl" || key == "cachecontrol" || key == "cache-control")
            {
                responseheaders.CacheControl = CacheControlHeaderValue.Parse(value.ToString());
            }
            else if (key == "ConnectionClose" || key == "Connection-Close" || key == "connection-close")
            {
                responseheaders.ConnectionClose = bool.Parse(value.ToString());
            }
            else if (key == "Date" || key == "Date")
            {
                responseheaders.Date = DateTime.Parse(value.ToString());
            }
            else if (key == "Etag" || key == "etag")
            {
                //TODO: there is difference between W/\"..." and "..."
                responseheaders.ETag = EntityTagHeaderValue.Parse(value.ToString());
                //value.ToString().Contains("W/") ?
                //                  new EntityTagHeaderValue(value.ToString().Remove(0, 2), true) :
                //                  new EntityTagHeaderValue(value.ToString());
            }
            else if (key == "Location" || key == "location")
            {
                responseheaders.Location = new Uri(value.ToString());
            }
            else if (key == "RetryAfter" || key == "Retry-After" || key == "retry-after")
            {
                responseheaders.RetryAfter = RetryConditionHeaderValue.Parse(value.ToString());
            }
            else if (key == "TransferEncodingChunked" || key == "Transfer-Encoding-Chunked" || key == "transfer-encoding-chunked")
            {
                responseheaders.TransferEncodingChunked = bool.Parse(value.ToString());
            }
            else
            {
                //TODO: check if header has no setter
                try
                {
                    responseheaders.Add(key, value.ToString());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    try
                    {
                        responseheaders.TryAddWithoutValidation(key, value.ToString());
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);

                    }

                }
            }

        }
    }
}
