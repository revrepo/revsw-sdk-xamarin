using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RacerMobileApp
{
	public partial class WebPage : ContentPage
	{
		string _uri;
		public WebPage(string uri)
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
