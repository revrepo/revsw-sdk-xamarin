using Newtonsoft.Json;
using RacerMobileApp.Classes;
using RacerMobileApp.Model;
using RacerMobileApp.ViewModels;
using RacerMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RacerMobileApp
{
    public partial class MainPage : ContentPage
    {
		async void BrowserStart_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new WebPageNew((BindingContext as MainPageViewModel).Uri));
		}

        public MainPage()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Settings.History))
            {
                this.BindingContext = new MainPageViewModel()
                {
                    MethodSelected = 0,
                    DataTypeSelected = 0,
                    LoadAllPageUrls = false,
                    Uri = "ebay.com",
                    Payload = "0",
					TestCount = "10"
                };
            }
            else
            {
                var history = JsonConvert.DeserializeObject<List<SessionResult>>(Settings.History);
                var lastHistoryItemSession = history.Last().Session;

                this.BindingContext = new MainPageViewModel()
                {
                    MethodSelected = lastHistoryItemSession.Method == HttpMethod.Get ? 0 : 1,
                    DataTypeSelected = lastHistoryItemSession.ContentType == "application/json" ? 0 : 1,
                    LoadAllPageUrls = lastHistoryItemSession.LoadAllUrls,
                    Uri = lastHistoryItemSession.Uri.AbsoluteUri,
                    Payload = lastHistoryItemSession.Payload.ToString(),
                    TestCount = lastHistoryItemSession.TestsCount.ToString()
                };

            }
           
            

        }

        public MainPageViewModel Model => BindingContext as MainPageViewModel;


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(this.BindingContext==null)
            this.BindingContext = new MainPageViewModel();

           // loadPageUrlsSwitch.IsToggled = false;
        }
        public async void Start(object sender, EventArgs e)
        {
            Model.Navigate();

            if (Model.session != null)
            {
              await  Navigation.PushAsync(new TestResultPage(Model.session));
            }
            
        }

        public async void HistoryBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryPage());
        }

        //void switchToogled(object sender, ToggledEventArgs e)
        //{
        //    Model.LoadAllPageUrls = e.Value;
        //}
        
    }
}
