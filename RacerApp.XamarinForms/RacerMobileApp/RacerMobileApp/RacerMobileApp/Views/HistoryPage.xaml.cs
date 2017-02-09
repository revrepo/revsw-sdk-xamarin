using Newtonsoft.Json;
using RacerMobileApp.Classes;
using RacerMobileApp.Model;
using RacerMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!string.IsNullOrEmpty(Settings.History))
                Model.List = JsonConvert.DeserializeObject<ObservableCollection<SessionResult>>(Settings.History);
           
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

            if (Model.List !=null || Model.List.Count > 0)
            {
                Settings.History = string.Empty;
            }

            this.BindingContext = new HistoryPageViewModel();
            Model.IsClearHistoryEnabled = false;
        }
    }
}
