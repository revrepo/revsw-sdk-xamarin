using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Nuubit.SDK;
using Xamarin.Forms;

namespace TestRSSReader
{
    public partial class App : Application
    {
        public static HttpClientHandler Handler;
        public App()
        {
            InitializeComponent();

            MainPage = new TestRSSReader.MainPage();

            if (Handler == null)
            {
                //Handler = new NuubitMessageHandler();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
