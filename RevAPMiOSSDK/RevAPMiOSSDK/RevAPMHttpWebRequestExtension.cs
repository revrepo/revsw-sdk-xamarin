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
using System.Threading.Tasks;

namespace RevAPMiOSSDK
{
   static  class RevAPMHttpWebRequestExtension
    {
        private static async Task<RevAPMWebResponse> RevAPMGetResponseAsync(this HttpWebRequest request)
        {
			NSMutableUrlRequest nsRequest = new NSMutableUrlRequest(request.RequestUri, NSUrlRequestCachePolicy.ReloadIgnoringCacheData, request.Timeout/1000);
             
            nsRequest.HttpMethod = request.Method;

            //Get Headers
			var headersKeys = new List<object>();
			var headersValues = new List<object>();

            for (int i=0; i< request.Headers.Count; i++)
            {
                try
                {
                    headersKeys.Add(request.Headers.AllKeys[i]);
                    headersValues.Add(string.Join(",", request.Headers.GetValues(request.Headers.AllKeys[i])));
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
					throw ex;
                }
                
            }

			var headers = NSDictionary.FromObjectsAndKeys(headersValues.ToArray(), headersKeys.ToArray());

			nsRequest.Headers = headers;



            //TODO: Body can be set not for all HTTP Methods
            //nsrequest body
            //  if(headersKeys.Contains("Content-Length")||headersKeys.Contains("Transfer-Encoding")||nsRequest.HttpMethod != "HEAD" || nsRequest.HttpMethod != "GET") { }
            //nsRequest.Body = NSData.FromStream(await request.GetRequestStreamAsync().ConfigureAwait(false));

            NSUrlAsyncResult NSresponse;

            if (nsRequest.HttpMethod != "GET")
            {
                nsRequest.Body = NSData.FromStream(await request.GetRequestStreamAsync().ConfigureAwait(false));
            }

            NSresponse = await NSUrlConnection.SendRequestAsync(nsRequest, new NSOperationQueue()).ConfigureAwait(false);
            RevAPMWebResponse response = new RevAPMWebResponse(NSresponse);
             
            return response;
        }
    }
}
