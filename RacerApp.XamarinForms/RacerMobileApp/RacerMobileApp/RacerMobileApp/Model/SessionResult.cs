using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.Model
{
    public class SessionResult
    {
        public string Uri { get; set; }

        public Session Session { get; set; }
        public List<TestResult> RevTestsResult { get; set; }
        public List<TestResult> DefaultTestsResult { get; set; }

        public ObservableCollection<DetailedReport> DetailedReportList { get; set; }

        public DateTime Date { get; set; }
    }
}
