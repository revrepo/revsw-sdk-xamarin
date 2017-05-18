using Xamarin.Forms;

namespace WebViewTestForms
{
	public partial class WebViewTestFormsPage : ContentPage
	{
		void WebView_Navigated(object sender, WebNavigatedEventArgs e)
		{
			btn.IsEnabled = true;
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(textField.Text)) return;

			btn.IsEnabled = false;
			GoTo("https://"+textField.Text);

			textField.Text = "";
		}

		public WebViewTestFormsPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			WebView.Navigated += WebView_Navigated;

			GoTo("https://www.google.com.ua");
		}

		void GoTo(string url)
		{
			var source = new UrlWebViewSource();
			source.Url =url;
			WebView.Source = source;
		}
	}
}
