using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CaglarDurmus.CustomControls.Web
{
    public partial class CustomControl
    {
        public class Link : CustomControlBase
        {
            public Link() : base()
            {

            }
            public Link(string href, string icon)
            {
                this.Icon = icon;
                this.Href = href;
            }

            private string Icon { get; set; }
            private string Href { get; set; }

            public override MvcHtmlString RenderControl()
            {
                StringBuilder html = new StringBuilder();
                html.AppendFormat(string.Format(@"<a href=""{0}""><i class=""{1}"" style=""text-align: center; width: 100%;""></i></a>", this.Href, this.Icon));

                return MvcHtmlString.Create(html.ToString());
            }
        }
    }
}
