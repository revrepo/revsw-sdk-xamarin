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
        public DetailsPageViewModel Model => BindingContext as DetailsPageViewModel;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Model.List == null)
                Model.List = new List<RacerMobileApp.Model.DetailedReport>();

            if (DetailsPageViewModel.DetailedReportList != null && DetailsPageViewModel.DetailedReportList.Count > 0)
                Model.List = DetailsPageViewModel.DetailedReportList;


        }
    }
}
