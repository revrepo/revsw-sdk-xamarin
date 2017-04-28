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
using Nuubit.SDK;
using RacerMobileApp.Services;

namespace RacerMobileApp.Droid
{
    [Application(Debuggable = false)]
	class MainApplication : Com.Nuubit.Sdk.NuubitApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
           : base(handle, transer)
        {
        }
    }
}