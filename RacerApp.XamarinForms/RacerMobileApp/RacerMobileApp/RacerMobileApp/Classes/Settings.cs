using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacerMobileApp.Classes
{
   public static class Settings
    {
        public static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private const string HistoryKey = "history_key";
        private static readonly string HistoryDefault = string.Empty;


        public static string History
        {
            get { return AppSettings.GetValueOrDefault<string>(Settings.HistoryKey, HistoryDefault); }
            set {   AppSettings.AddOrUpdateValue<string>(HistoryKey, value); }
        }
    }
}
