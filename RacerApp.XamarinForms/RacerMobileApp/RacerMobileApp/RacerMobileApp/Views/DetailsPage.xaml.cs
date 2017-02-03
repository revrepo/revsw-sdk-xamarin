using RacerMobileApp.Model;
using RacerMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RacerMobileApp.Views
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage()
        {
            InitializeComponent();
            this.BindingContext = new DetailsPageViewModel();
        }

        public DetailsPage(SessionResult sessionResult)
        {
            InitializeComponent();
            this.BindingContext = new DetailsPageViewModel(sessionResult);
        }

        public DetailsPageViewModel Model => BindingContext as DetailsPageViewModel;
        protected override void OnAppearing()
        {
            base.OnAppearing();


            if (DetailsPageViewModel.DetailedReportList != null && DetailsPageViewModel.DetailedReportList.Count > 0)
            {
                

                if (Model.List == null)
                    Model.List = new List<RacerMobileApp.Model.DetailedReport>();

                Model.List = DetailsPageViewModel.DetailedReportList;
            }
               

           



        }
    }
}
