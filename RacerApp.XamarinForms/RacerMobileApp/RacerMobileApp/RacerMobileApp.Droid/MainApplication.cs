using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace RacerMobileApp.Droid
{
    [Application]
    class MainApplication : Com.Rev.Sdk.RevApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
           : base(handle, transer)
        {
        }
    }
}