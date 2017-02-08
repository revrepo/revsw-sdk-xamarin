using Newtonsoft.Json;
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

          
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
         

            if(Model.List !=null || Model.List.Count > 0)
            {
                var lastItem = Model.List.Last();
                var list = new List<RacerMobileApp.Model.SessionResult>();
                list.Add(lastItem);

                Settings.History = JsonConvert.SerializeObject(list);

                Model.List = JsonConvert.DeserializeObject<List<RacerMobileApp.Model.SessionResult>>(Settings.History);
               

            }
            
            Model.IsClearHistoryEnabled = false;
        }
    }
}
