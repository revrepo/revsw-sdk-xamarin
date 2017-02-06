using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.ViewModels
{
   public class DetailsPageViewModel : BaseViewModel
    {
        public static ObservableCollection<DetailedReport> DetailedReportList;

        public DetailsPageViewModel()
        {
            DetailedReportList = new ObservableCollection<DetailedReport>();
        }

        public DetailsPageViewModel(SessionResult sessionResult)
        {
            List = new ObservableCollection<DetailedReport>();
            List = sessionResult.DetailedReportList;
           
        }

        private ObservableCollection<DetailedReport> list;
        public ObservableCollection<DetailedReport> List
        {
            get { return list; }
            set { SetProperty(ref list, value); }
        }
    }
}
