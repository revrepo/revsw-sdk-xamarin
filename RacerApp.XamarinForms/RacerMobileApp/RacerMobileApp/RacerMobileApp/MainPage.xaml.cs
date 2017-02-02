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

        public async void Start(object sender, EventArgs e)
        {
            Model.Navigate();

            if (Model.session != null)
            {
                //var navPage = new NavigationPage(new TestResultPage(Model.session));
                //NavigationPage.SetHasBackButton(navPage, true);

              await  Navigation.PushAsync(new TestResultPage(Model.session));
            }
            

           // await App.Current.MainPage.Navigation.PushAsync(navPage);
        }
    }
}
