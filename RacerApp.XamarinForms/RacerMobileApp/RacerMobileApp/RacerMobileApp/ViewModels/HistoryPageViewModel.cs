using Newtonsoft.Json;
using RacerMobileApp.Classes;
using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.ViewModels
{
   public class HistoryPageViewModel : BaseViewModel
    {
        private List<SessionResult> _list;
        public List<SessionResult> List
        {
            get { return _list = JsonConvert.DeserializeObject<List<SessionResult>>(Settings.History); }
            set { SetProperty(ref _list, value); }
        }

        private bool _isEnabled;
        public bool IsClearHistoryEnabled
        {
            get { return _isEnabled = List== null || List.Count == 0 ? false : true; }
            set { SetProperty(ref _isEnabled, value); }
        }
        
    }
}
