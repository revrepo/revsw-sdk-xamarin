using RacerMobileApp.Classes;
using System;
using RacerMobileApp.Services;
using System.Collections.Generic;
using System.Text;
using RacerMobileApp.iOS.Classes.Dependencies;

[assembly: Xamarin.Forms.Dependency(typeof(RevApmMessageHandlerInitializer))]
namespace RacerMobileApp.iOS.Classes.Dependencies
{
    public class RevApmMessageHandlerInitializer : IMessageHandlerInitializer
    {
        public void InitializeMessageHandler()
        {
           HttpRequestService.CustomMessageHandler = new RevAPMiOSSDK.RevAPMiOSHttpMessageHandler();
        }
    }
}
