using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.ViewModels
{
   public class DetailsPageViewModel : BaseViewModel
    {
        public static List<DetailedReport> DetailedReportList;

        public DetailsPageViewModel()
        {
            DetailedReportList = new List<DetailedReport>();
        }

        public DetailsPageViewModel(SessionResult sessionResult)
        {
            List = new List<DetailedReport>();
            List = sessionResult.DetailedReportList;
           
        }

        private List<DetailedReport> list;
        public List<DetailedReport> List
        {
            get { return list; }
            set { SetProperty(ref list, value); }
        }
    }
}
