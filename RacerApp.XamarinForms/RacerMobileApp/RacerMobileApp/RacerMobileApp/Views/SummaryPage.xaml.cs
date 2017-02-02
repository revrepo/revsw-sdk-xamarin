using Acr.UserDialogs;
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
    public partial class SummaryPage : ContentPage
    {
        Session session;
        public SummaryPage(Session session)
        {
            InitializeComponent();

            this.session = session;
            this.BindingContext = new SummaryPageViewModel(session);
        }

          public SummaryPageViewModel Model => BindingContext as SummaryPageViewModel;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

          

            if (Model.DefaultTestsResult == null|| Model.DefaultTestsResult.Count==0)
            {
              

                var config = new ProgressDialogConfig() { Title = "Processing..." };
                var _progressDialog = UserDialogs.Instance.Progress(config);
                _progressDialog.Show();

                Model.DefaultTestsResult = await Model.SendRequests(session, false);
                Model.RevApmTestsResult = await Model.SendRequests(session, true);

                Model.CalculateRevStatistics(Model.RevApmTestsResult);
                Model.CalculateStatistics(Model.DefaultTestsResult);

                if (DetailsPageViewModel.DetailedReportList == null)
                    DetailsPageViewModel.DetailedReportList = new List<DetailedReport>();

                for (int i = 0; i < Model.RevApmTestsResult.Count; i++)
                {
                    DetailsPageViewModel.DetailedReportList.Add(new DetailedReport()
                    {
                        Number = i + 1,
                        RevStatusCode = Model.RevApmTestsResult[i].StatusCode.ToString(),
                        RevDuration = Model.RevApmTestsResult[i].DurationMs,
                        StatusCode = Model.DefaultTestsResult[i].StatusCode.ToString(),
                        Duration = Model.DefaultTestsResult[i].DurationMs
                    });
                }



                _progressDialog.Hide();
            }
            

        }
       
    }
}
