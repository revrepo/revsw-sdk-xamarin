using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RacerMobileApp.Views
{
   public class TestResultPage : TabbedPage
    {
        public TestResultPage(Session session)
        {
            //Children.Add(new NavigationPage(new SummaryPage(session)));
            //Children.Add (new NavigationPage(new DetailsPage()));

            Children.Add(new SummaryPage(session));
            Children.Add(new DetailsPage());
        }
    }
}
