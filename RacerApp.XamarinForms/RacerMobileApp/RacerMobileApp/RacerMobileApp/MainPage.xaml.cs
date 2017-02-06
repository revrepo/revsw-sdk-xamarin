using RacerMobileApp.Classes;
using RacerMobileApp.Model;
using RacerMobileApp.ViewModels;
using RacerMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RacerMobileApp
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();

          
        }

        public MainPageViewModel Model => BindingContext as MainPageViewModel;


        protected override void OnAppearing()
        {
            base.OnAppearing();

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

        void switchToogled(object sender, ToggledEventArgs e)
        {
            Model.LoadAllPageUrls = e.Value;
        }
        
    }
}
