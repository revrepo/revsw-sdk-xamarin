using System;
using System.Diagnostics;
using System.Net.Http;
using UIKit;

namespace AppForTestNugetPackage.iOS
{
	public partial class ViewController : UIViewController
	{
		
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public  override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			Button.AccessibilityIdentifier = "myButton";
			Button.TouchUpInside += async delegate 
            {
             
                for (int i=0; i < 1000; i++)
                {
                    try
                    {
 
                        using (SampleHttpRequest.CustomMessageHandler = new RevAPMiOSSDK.RevAPMiOSHttpMessageHandler())
                        {
                         await  SampleHttpRequest.SendRevAPMHttpRequest();
                        }
                 

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }                   
                  
                }

               
			};
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

