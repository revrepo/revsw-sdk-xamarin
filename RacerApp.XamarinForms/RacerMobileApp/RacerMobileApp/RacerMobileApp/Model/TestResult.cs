using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.Model
{
   public class TestResult
    {
        public long ResponseSizeBytes { get; set; }
        public bool HasError { get; set; }
        public long DurationMs { get; set; }
        public HttpStatusCode StatusCode { get; set; }
   
    }
}
