using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Forms;

namespace TestRSSReader
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var data = await new HttpClient(App.Handler).GetStringAsync("http://rssfeeds.usatoday.com/usatoday-NewsTopStories");

            XDocument document = XDocument.Parse(data);

            var feeds = from feed in document.Descendants("item")
                select new Item
                {
                    Title = feed.Element("title").Value,
                    Link = feed.Element("link").Value,
                    Description = feed.Element("description").Value
                };

            lst.ItemsSource = feeds;
        }

        private void Lst_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Item;

            Navigation.PushModalAsync(new WebPage(item));
        }
    }

    public class Item
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }
    }
}
