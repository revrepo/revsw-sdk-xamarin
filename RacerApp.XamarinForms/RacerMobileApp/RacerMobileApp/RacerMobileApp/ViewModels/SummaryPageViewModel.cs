using RacerMobileApp.Classes;
using RacerMobileApp.Model;
using RacerMobileApp.Services;
using System;
using System.Collections.Generic;
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

        //  private static int TotalTests;
        //  private static int TestNumber;

        //private int _testNumber;
        //public int TestNumber
        //{
        //    get { return _testNumber; }
        //    set { SetProperty(ref _testNumber, value); }
        //}

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


        //private string _loadingText;
        //public string LoadingText
        //{
        //    get { return _loadingText; }
        //    set { SetProperty(ref _loadingText, value); }
        //}

        //private bool _isLoading;
        //public bool IsLoading
        //{
        //    get { return _isLoading; }
        //    set { SetProperty(ref _isLoading, value); }
        //}

        //private bool _isReady;
        //public bool IsReady
        //{
        //    get { return _isReady; }
        //    set { SetProperty(ref _isReady, value); }
        //}
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

            RevApmTestsResult = new List<TestResult>();
            DefaultTestsResult = new List<TestResult>();

            //IsLoading = true;
            //IsReady = false;

            //LoadingText = "Default requests processing " + TestNumber + "/" + TotalTests;

            //MakeTests(session, DefaultTestsResult, false);

            //LoadingText = "RevApm requests processing " + TestNumber + "/" + TotalTests;

            //MakeTests(session, RevApmTestsResult, true);
          

            //IsLoading = false;
            //IsReady = true;
        }

        public void CalculateRevStatistics(List<TestResult> results)
        {
            RevMinValue = StaticticCalculator.CalculateMinValue(results);
            RevMaxValue = StaticticCalculator.CalculateMaxValue(results);
            RevMediana = StaticticCalculator.CalculateMedianaValue(results);
            RevAverage = StaticticCalculator.CalculateAverageValue(results);
            RevStandartDeviation = StaticticCalculator.CalculateStandardDeviation(results);
            RevExpectedValue = StaticticCalculator.CalculateWeighteedAverage(results);
        }

        public void CalculateStatistics(List<TestResult> results)
        {
            MinValue = StaticticCalculator.CalculateMinValue(results);
            MaxValue = StaticticCalculator.CalculateMaxValue(results);
            Mediana = StaticticCalculator.CalculateMedianaValue(results);
            Average = StaticticCalculator.CalculateAverageValue(results);
            StandartDeviation = StaticticCalculator.CalculateStandardDeviation(results);
            ExpectedValue = StaticticCalculator.CalculateWeighteedAverage(results);
        }


        //public async Task MakeTests(Session session, List<TestResult> list, bool IsRevApm)
        //{
        //    list = await SendRequests(session, IsRevApm);
        //}

        public async Task<List<TestResult>> SendRequests(Session session,bool IsRevApmTest)
        {
          
            List<TestResult> list = new List<TestResult>();

            for(int i = 1; i <= TotalTests; i++)
            {               
               var response = await HttpRequestService.SendRequest(session.Uri, session.Payload, session.Method, session.ContentType, IsRevApmTest);
                list.Add(response);
            }

            return list;
        }
    }
}
