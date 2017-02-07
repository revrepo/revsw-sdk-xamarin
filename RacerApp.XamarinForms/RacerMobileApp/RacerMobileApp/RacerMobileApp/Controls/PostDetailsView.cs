using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RacerMobileApp.Controls
{
    class PostDetailsView : ViewCell
    {
        public Label number;
        public Label revStatusCode;
        public Label revDuration;
        public Label StatusCode;
        public Label Duration;
        public Label Size;
        public Label RevSize;
        public StackLayout stack;
        public StackLayout revstack;

        public PostDetailsView()
        {
            var grid = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, HeightRequest = 50 };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });

            number = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, TextColor = Color.Black };
            revDuration = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            revStatusCode = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            StatusCode = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Duration = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Size = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center,TextColor = Color.Gray };
            RevSize = new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, TextColor = Color.Gray };

            stack = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand,};
            revstack = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, };

            stack.Children.Add(Duration);
            stack.Children.Add(Size);

            revstack.Children.Add(revDuration);
            revstack.Children.Add(RevSize);

            grid.Children.Add(number, 0, 0);
            grid.Children.Add(stack, 1, 0);
            grid.Children.Add(StatusCode, 2, 0);
            grid.Children.Add(revstack, 3, 0);
            grid.Children.Add(revStatusCode, 4, 0);
            

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
                Size.Text = "("+ (item.DefaultResponseSizeBytes/1024).ToString()+")";
                RevSize.Text = "("+ (item.RevResponseSizeBytes/1024).ToString() + ")";
            }
        }
    }
}
