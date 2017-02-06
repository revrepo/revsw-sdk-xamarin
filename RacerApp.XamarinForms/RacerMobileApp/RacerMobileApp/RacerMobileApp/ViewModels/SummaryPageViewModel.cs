using Acr.UserDialogs;
using RacerMobileApp.Classes;
using RacerMobileApp.Model;
using RacerMobileApp.Services;
using RacerMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.ViewModels
{
   public class SummaryPageViewModel : BaseViewModel
    {
        Session session;

        public List<TestResult> RevApmTestsResult { get; set; }
        public List<TestResult> DefaultTestsResult { get; set; }


        private int _totalTests;
        public int TotalTests
        {
            get { return _totalTests; }
            set { SetProperty(ref _totalTests, value); }
        }

        private string _min;
        public string MinValue
        {
            get { return _min; }
            set { SetProperty(ref _min, value); }
        }
        private string _max;
        public string MaxValue
        {
            get { return _max; }
            set { SetProperty(ref _max, value); }
        }
        private string _mediana;
        public string Mediana
        {
            get { return _mediana; }
            set { SetProperty(ref _mediana, value); }
        }

        private string _average;
        public string Average
        {
            get { return _average; }
            set { SetProperty(ref _average, value); }
        }

        private string _standartDeviation;
        public string StandartDeviation
        {
            get { return _standartDeviation; }
            set { SetProperty(ref _standartDeviation, value); }
        }

        private string _expectedValue;
        public string ExpectedValue
        {
            get { return _expectedValue; }
            set { SetProperty(ref _expectedValue, value); }
        }

        private int _revTotalTests;
        public int RevTotalTests
        {
            get { return _revTotalTests; }
            set { SetProperty(ref _revTotalTests, value); }
        }

        private string _revMin;
        public string RevMinValue
        {
            get { return _revMin; }
            set { SetProperty(ref _revMin, value); }
        }
        private string _revMax;
        public string RevMaxValue
        {
            get { return _revMax; }
            set { SetProperty(ref _revMax, value); }
        }
        private string _revMediana;
        public string RevMediana
        {
            get { return _revMediana; }
            set { SetProperty(ref _revMediana, value); }
        }

        private string _revAverage;
        public string RevAverage
        {
            get { return _revAverage; }
            set { SetProperty(ref _revAverage, value); }
        }

        private string _revStandartDeviation;
        public string RevStandartDeviation
        {
            get { return _revStandartDeviation; }
            set { SetProperty(ref _revStandartDeviation, value); }
        }

        private string _revExpectedValue;
        public string RevExpectedValue
        {
            get { return _revExpectedValue; }
            set { SetProperty(ref _revExpectedValue, value); }
        }

        public SummaryPageViewModel(Session session)
        {
            this.session = session;
            TotalTests = session.TestsCount;

            //RevApmTestsResult = new List<TestResult>();
            //DefaultTestsResult = new List<TestResult>();
        }

        public SummaryPageViewModel(SessionResult sessionResult)
        {

            RevApmTestsResult = new List<TestResult>();
            DefaultTestsResult = new List<TestResult>();

            RevApmTestsResult = sessionResult.RevTestsResult;
            DefaultTestsResult = sessionResult.DefaultTestsResult;


            if (DetailsPageViewModel.DetailedReportList == null || DetailsPageViewModel.DetailedReportList.Count == 0)
                DetailsPageViewModel.DetailedReportList = new ObservableCollection<DetailedReport>();


            DetailsPageViewModel.DetailedReportList = sessionResult.DetailedReportList;

            CalculateStatistics(DefaultTestsResult);
            CalculateRevStatistics(RevApmTestsResult);
        }


        public void CalculateRevStatistics(List<TestResult> results)
        {
            RevMinValue = StaticticsCalculator.CalculateMinValue(results);
            RevMaxValue = StaticticsCalculator.CalculateMaxValue(results);
            RevMediana = StaticticsCalculator.CalculateMedianaValue(results);
            RevAverage = StaticticsCalculator.CalculateAverageValue(results);
            RevStandartDeviation = StaticticsCalculator.CalculateStandardDeviation(results);
            RevExpectedValue = StaticticsCalculator.CalculateWeighteedAverage(results);
        }

        public void CalculateStatistics(List<TestResult> results)
        {
            MinValue = StaticticsCalculator.CalculateMinValue(results);
            MaxValue = StaticticsCalculator.CalculateMaxValue(results);
            Mediana = StaticticsCalculator.CalculateMedianaValue(results);
            Average = StaticticsCalculator.CalculateAverageValue(results);
            StandartDeviation = StaticticsCalculator.CalculateStandardDeviation(results);
            ExpectedValue = StaticticsCalculator.CalculateWeighteedAverage(results);
        }


        public async Task<List<TestResult>> SendRequests(Session session,bool IsRevApmTest)
        {
          
            List<TestResult> list = new List<TestResult>();

            var config = new ProgressDialogConfig();
            var progressDialog = UserDialogs.Instance.Progress(config);

            progressDialog.Show();

            for (int i = 1; i <= TotalTests; i++)
            {
                if(IsRevApmTest)
                   progressDialog.Title = "Loading RevAPM : " + i + "/" + TotalTests;
                else
                   progressDialog.Title = "Loading Default : " + i + "/" + TotalTests;

                var response = await HttpRequestService.SendRequest(session.Uri, session.Payload, session.Method, session.ContentType, session.LoadAllUrls, IsRevApmTest);
                list.Add(response);
            }

            progressDialog.Hide();

            return list;
        }
    }
}
