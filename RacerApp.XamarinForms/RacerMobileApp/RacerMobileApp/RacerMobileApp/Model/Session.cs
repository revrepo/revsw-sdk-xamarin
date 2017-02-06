using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.Model
{
   public class Session
    {
        public Uri Uri { get; set; }
        public int TestsCount { get; set; }
        public int Payload { get; set; }
        public HttpMethod Method { get; set; }
        public string ContentType { get; set; }
        public bool LoadAllUrls { get; set; }

    }
}
