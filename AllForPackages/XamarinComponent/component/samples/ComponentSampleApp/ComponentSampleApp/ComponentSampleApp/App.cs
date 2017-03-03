using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ComponentSampleApp
{
    public class App : Application
    {
        public static int count = 1;
        public App()
        {
            // The root page of your application


            var button = new Button() { Text = "Tap for send 1 request to amazon.com"};
            button.Clicked += (object sender, EventArgs e) =>
              {
                  //TODO:
                  DependencyService.Get<IHttpSample>().SendSampleHttpRequest();
                  button.Text = App.count.ToString() + " requests to amazon.com";
                  App.count++;
              };

            var content = new ContentPage
            {
                Title = "ComponentSampleApp",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = { button }

                }
            };

            MainPage = new NavigationPage(content);
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

