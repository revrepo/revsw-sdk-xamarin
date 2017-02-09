using Newtonsoft.Json;
using RacerMobileApp.Classes;
using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.ViewModels
{
   public class HistoryPageViewModel : BaseViewModel
    {
        private ObservableCollection<SessionResult> _list;
        public ObservableCollection<SessionResult> List
        {
            get { return _list = JsonConvert.DeserializeObject<ObservableCollection<SessionResult>>(Settings.History); }
            set { SetProperty(ref _list, value); }
        }

        private bool _isEnabled;
        public bool IsClearHistoryEnabled
        {
            get { return _isEnabled = !string.IsNullOrEmpty(Settings.History); }
            set { SetProperty(ref _isEnabled, value); }
        }
        
    }
}
