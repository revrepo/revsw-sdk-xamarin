using Acr.UserDialogs;
using Newtonsoft.Json;
using RacerMobileApp.Classes;
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
        SessionResult sessionResult;

        public SummaryPage(Session session)
        {
            InitializeComponent();

            this.session = session;
            this.BindingContext = new SummaryPageViewModel(session);
            rerunBtn.IsVisible = false;
        }

        public SummaryPage(SessionResult sessionResult)
        {
            InitializeComponent();
            this.BindingContext = new SummaryPageViewModel(sessionResult);
            this.sessionResult = sessionResult;
            this.session = sessionResult.Session;
            //rerunBtn.IsVisible = false;

        }

          public SummaryPageViewModel Model => BindingContext as SummaryPageViewModel;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

          

            if (Model.DefaultTestsResult == null|| Model.DefaultTestsResult.Count==0)
            {

                if (Model.DefaultTestsResult == null)
                    Model.DefaultTestsResult = new List<TestResult>();

                if (Model.RevApmTestsResult == null)
                    Model.RevApmTestsResult = new List<TestResult>();

                Model.DefaultTestsResult = await Model.SendRequests(session, false);
                Model.RevApmTestsResult = await Model.SendRequests(session, true);

                Model.CalculateRevStatistics(Model.RevApmTestsResult);
                Model.CalculateStatistics(Model.DefaultTestsResult);

                if (DetailsPageViewModel.DetailedReportList == null)
                    DetailsPageViewModel.DetailedReportList = new System.Collections.ObjectModel.ObservableCollection<DetailedReport>();

                for (int i = 0; i < Model.RevApmTestsResult.Count; i++)
                {
                    DetailsPageViewModel.DetailedReportList.Add(new DetailedReport()
                    {
                        Number = i + 1,
                        RevStatusCode = Model.RevApmTestsResult[i].StatusCode.ToString(),
                        RevDuration = Model.RevApmTestsResult[i].DurationMs,
                        StatusCode = Model.DefaultTestsResult[i].StatusCode.ToString(),
                        Duration = Model.DefaultTestsResult[i].DurationMs,
                        Method = Model.DefaultTestsResult[i].Method,
                        RevResponseSizeBytes = Model.RevApmTestsResult[i].ResponseSizeBytes,
                        DefaultResponseSizeBytes = Model.DefaultTestsResult[i].ResponseSizeBytes
                    });
                }

                var sessionresult = new SessionResult()
                {
                    Uri = session.Uri.AbsoluteUri,
                    RevTestsResult = Model.RevApmTestsResult,
                    DefaultTestsResult = Model.DefaultTestsResult,
                    DetailedReportList = DetailsPageViewModel.DetailedReportList,
                    Date = DateTime.Now,
                    Session = session

                };

                this.sessionResult = sessionresult;

                if (string.IsNullOrEmpty(Settings.History))
                {

                    var historylist = new List<SessionResult>();

                    historylist.Add(sessionresult);

                    Settings.History = JsonConvert.SerializeObject(historylist);
                }
                else
                {
                    var history = JsonConvert.DeserializeObject<List<SessionResult>>(Settings.History);

                    history.Add(sessionresult);

                    Settings.History = JsonConvert.SerializeObject(history);
                }

             
      
            }
            

        }

        public void RerunBtnClicked(object sender, EventArgs e)
        {
            this.sessionResult = null;

            DetailsPageViewModel.DetailedReportList = null;  

            this.BindingContext = new SummaryPageViewModel(this.session);

            this.OnAppearing();

        }

        async void SendEmailEvent(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SendEmailPage(sessionResult));

        }

    }
}
