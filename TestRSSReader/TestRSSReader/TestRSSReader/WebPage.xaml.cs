using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestRSSReader
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPage : ContentPage
    {
		Item _item;
		
        public WebPage(Item item)
        {
            InitializeComponent();
			_item = item;
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			webView.Source = _item.Link;

		}
    }

  
}
