using System;
using System.Threading.Tasks;
using Android.Webkit;
using TestRSSReader;
using TestRSSReader.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace TestRSSReader.Droid
{
	public class CustomWebViewRenderer:WebViewRenderer
	{
		//Android.Webkit.WebView _webView;

		protected override async void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
		{
			base.OnElementChanged(e);

			var webView = Control;
			var chromeClient = Com.Nuubit.Sdk.NuubitSDK.CreateWebChromeClient();
			webView.SetWebChromeClient(chromeClient);

			webView.SetWebViewClient(Com.Nuubit.Sdk.NuubitSDK.CreateWebViewClient(Context, webView, Com.Nuubit.Sdk.NuubitSDK.OkHttpCreate(Com.Nuubit.Sdk.NuubitConstants.DefaultTimeoutSec, false, false)));

		}


	}
}
