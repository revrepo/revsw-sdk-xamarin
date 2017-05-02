using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RacerMobileApp.Views
{
    public partial class WebPageNew : ContentPage
    {
        string _uri;
        public WebPageNew(string uri)
        {
            InitializeComponent();
            _uri = uri;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            webView.Source = _uri;
        }

    }
}
