using ComponentSampleApp.iOS;
using RevApm;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(HttpRequestSample))]
namespace ComponentSampleApp.iOS
{
    public class HttpRequestSample : IHttpSample
    {
        //Sample "GET" http request to "https://www.amazon.com/" for testing core functionality and take analitics by request
        public  async void SendSampleHttpRequest()
        {

            var _messagehandler = new RevApmMessageHandler();

            HttpClient _client_with_messagehanlder = new HttpClient(_messagehandler);

            HttpRequestMessage _requestmessage = new HttpRequestMessage(HttpMethod.Get, "https://www.amazon.com/");

            HttpResponseMessage _response = await _client_with_messagehanlder.SendAsync(_requestmessage);
        }

    }
}
