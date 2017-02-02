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
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private const string UserNameKey = "username_key";
        private static readonly string UserNameDefault = string.Empty;

        private const string SomeIntKey = "int_key";
        private static readonly int SomeIntDefault = 6251986;
    }
}
