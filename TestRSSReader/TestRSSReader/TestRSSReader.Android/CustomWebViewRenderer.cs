using System;
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
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
		{
			base.OnElementChanged(e);

			if (this.Control == null)
			{
				var view = new Android.Webkit.WebView(this.Context);

				var chromeClient = Com.Nuubit.Sdk.NuubitSDK.CreateWebChromeClient();

				view.SetWebChromeClient(chromeClient);

				view.SetWebViewClient(Com.Nuubit.Sdk.NuubitSDK.CreateWebViewClient(Context,view,Com.Nuubit.Sdk.NuubitSDK.OkHttpCreate(Com.Nuubit.Sdk.NuubitConstants.DefaultTimeoutSec,false,false)));

				SetNativeControl(view);

				view.LoadUrl(e.NewElement.Source.ToString());
			}
		}
	}
}
