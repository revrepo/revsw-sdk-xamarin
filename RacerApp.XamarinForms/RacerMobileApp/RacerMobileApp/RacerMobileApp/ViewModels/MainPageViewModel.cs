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
        
        }

        private bool _loadAllPageUrls;
        public bool LoadAllPageUrls
        {
            get { return _loadAllPageUrls; }
            set { SetProperty(ref _loadAllPageUrls, value); }
        }

        

        public async void Navigate()
        {
           
            if (!string.IsNullOrEmpty(TestCount) && int.Parse(TestCount) > 0  && !string.IsNullOrEmpty(Uri))
            {
                Payload = MethodSelected == 0 ? string.Empty : Payload;

                if(Uri.StartsWith("http://") || Uri.StartsWith("https://"))
                {
                   //Do nothing  
                }              
                else
                {
                    Uri = "http://" + Uri;
                }
                    
                session = new Session()
                {
                    TestsCount = int.Parse(TestCount),
                    Payload = string.IsNullOrEmpty(Payload) ? 0 : int.Parse(Payload),
                    Method = MethodSelected == 0 ? HttpMethod.Get : HttpMethod.Post,
                    ContentType = DataTypeSelected == 0 ? "application/json" : "application/xml",
                    Uri = new Uri(Uri, UriKind.Absolute),
                    LoadAllUrls = LoadAllPageUrls
                };

               
            }
            else
            {
                string text;

                if (int.Parse(!string.IsNullOrEmpty(TestCount) ? TestCount:"0") <= 0)
                    text = "You couldn't make 0 tests.";              
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

        private string _payload;
        public string Payload
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

    }
}
