using RacerMobileApp.Model;
using RacerMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RacerMobileApp.ViewModels
{
   public class MainPageViewModel : BaseViewModel
    {
       public Session session;
       
        public  MainPageViewModel()
        {
          #if DEBUG
            Uri = "https://www.xamarin.com";
          #endif
        }

        public async void Navigate()
        {
            if (int.Parse(TestCount) > 0 && Payload >= 0 && !string.IsNullOrEmpty(Uri))
            {
                Method = MethodSelected == 0 ? HttpMethod.Get : HttpMethod.Post;
                DataType = DataTypeSelected == 0 ? "application/json" : "application/xml";

                session = new Session()
                {
                    TestsCount = int.Parse(TestCount),
                    Payload = Payload,
                    Method = Method,
                    ContentType = DataType,
                    Uri = new Uri(Uri, UriKind.Absolute)
                };

               
            }
            else
            {
                string text;

                if (int.Parse(TestCount) <= 0)
                    text = "You couldn't make 0 tests.";
                else if (Payload <= 0)
                    text = "Payload couldn't be less then 0.";
                else if (string.IsNullOrEmpty(Uri))
                    text = "Please, paste your http link for make tests";
                else
                    text = "There are empty fields. Please, make sure that all fields are filled";

                await App.Current.MainPage.DisplayAlert("Notification", text, "OK");
            }
        }

        private string _testCount;
        public string TestCount
        {
            get { return _testCount; }
            set { SetProperty(ref _testCount, value); }
        }

        private int _methodSelected;
        public int MethodSelected
        {
            get { return _methodSelected; }
            set { SetProperty(ref _methodSelected, value); }
        }

        private int _dataTypeSelected;
        public int DataTypeSelected
        {
            get { return _dataTypeSelected; }
            set { SetProperty(ref _dataTypeSelected, value); }
        }

        private int _payload;
        public int Payload
        {
            get { return _payload; }
            set { SetProperty(ref _payload, value); }
        }

        private string _uri;
        public string Uri
        {
            get { return _uri; }
            set { SetProperty(ref _uri, value); }
        }

        private HttpMethod _method;
        public HttpMethod Method
        {
            get { return _method; }
            set { SetProperty(ref _method, value); }
        }

        private string _dataType;
        public string DataType
        {
            get { return _dataType; }
            set { SetProperty(ref _dataType, value); }
        }
    }
}
