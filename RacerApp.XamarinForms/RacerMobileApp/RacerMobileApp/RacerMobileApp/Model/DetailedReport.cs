using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.Model
{
   public class DetailedReport
    {
        public int Number { get; set; }
        public long Duration { get; set; }
        public string StatusCode { get; set; }

        public long RevDuration { get; set; }
        public string RevStatusCode { get; set; }
        public string Method { get; set; }

        public long? DefaultResponseSizeBytes { get; set; }

        public long? RevResponseSizeBytes { get; set; }
    }
}
