using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.Model
{
    public class SessionResult
    {
        public string Uri { get; set; }

        public List<TestResult> RevTestsResult { get; set; }
        public List<TestResult> DefaultTestsResult { get; set; }

        public List<DetailedReport> DetailedReportList { get; set; }
    }
}
