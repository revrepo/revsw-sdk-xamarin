using RacerMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RacerMobileApp.Controls
{
    public class MethodDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate PostMethodTemplate;
        private readonly DataTemplate GetMethodTemplate;


        public MethodDataTemplateSelector()
        {
            // Retain instances!
            this.PostMethodTemplate = new DataTemplate(typeof(PostDetailsView));
            this.GetMethodTemplate = new DataTemplate(typeof(DetailsViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var result = item as DetailedReport;

            if (result.Method == "GET")
            {
                return GetMethodTemplate;
            }
            else
            {
                return PostMethodTemplate;
            }
        }

    }
}
