using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RacerMobileApp.Controls
{
    class DetailsViewCell : ViewCell
    {
        public Label number;
        public Label revStatusCode;
        public Label revDuration;
        public Label StatusCode;
        public Label Duration;
        public StackLayout stack;

        public DetailsViewCell()
        {
            var grid = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, HeightRequest=50};
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2,GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });

            number = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center , TextColor=Color.Black};
            revDuration = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            revStatusCode = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            StatusCode = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center }; 
            Duration = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };

            //stack = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Orientation = StackOrientation.Horizontal };


            grid.Children.Add(number, 0, 0);            
            grid.Children.Add(Duration, 1, 0);
            grid.Children.Add(StatusCode, 2, 0);
            grid.Children.Add(revDuration, 3, 0);
            grid.Children.Add(revStatusCode, 4, 0);


            //stack.Children.Add(number);
            //stack.Children.Add(revDuration);
            //stack.Children.Add(revStatusCode);
            //stack.Children.Add(Duration);
            //stack.Children.Add(StatusCode);

            this.View = grid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var item = BindingContext as DetailedReport;

            if (item != null)
            {
                number.Text = item.Number.ToString();
                revDuration.Text = item.RevDuration.ToString();
                revStatusCode.Text = item.RevStatusCode;
                StatusCode.Text = item.StatusCode;
                Duration.Text = item.Duration.ToString();
            }
        }
    }
}
