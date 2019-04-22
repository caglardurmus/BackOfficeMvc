using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglarDurmus.CustomControls.Web
{
    public partial class CustomControl
    {
        public static class Html
        {
            public static Gridview Gridview(string id, string caption)
            {
                return new Gridview(id, caption);
            }
        }
    }
}
