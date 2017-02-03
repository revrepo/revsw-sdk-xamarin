using RacerMobileApp.Classes;
using RacerMobileApp.Model;
using RacerMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RacerMobileApp.Views
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();

            this.BindingContext = new HistoryPageViewModel();
        }

        public HistoryPageViewModel Model => BindingContext as HistoryPageViewModel;


        public async void ListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

            if (e.Item != null)
            {
                await Navigation.PushAsync(new TestResultPage(e.Item as SessionResult));
            }

        }
        public void ClearHistoryBtnClicked(object sender, EventArgs e)
        {
            Settings.History = string.Empty;
            Model.List = new List<RacerMobileApp.Model.SessionResult>();
            Model.IsClearHistoryEnabled = false;
        }
    }
}
