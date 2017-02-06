using RacerMobileApp.Model;
using RacerMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RacerMobileApp.Views
{
    public partial class SendEmailPage : ContentPage
    {
        SessionResult sessionresult;
        public SendEmailPage(SessionResult sessionReport)
        {
            InitializeComponent();

            this.sessionresult = sessionReport;
            HtmlWebViewSource HTML = new HtmlWebViewSource();

            HTML.Html = Emailer.CreateHtml(sessionReport);

            webView.Source = HTML;
#if DEBUG
            addresseeEntry.Text = "romaniukvova@gmail.com";
            subjectEntry.Text = "RacerApp test session result";
#endif
        }

        public void SendEmail(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(addresseeEntry.Text)&& !string.IsNullOrEmpty(subjectEntry.Text))
            Emailer.SendEmail(sessionresult, addresseeEntry.Text, subjectEntry.Text);
        }
    }
}
