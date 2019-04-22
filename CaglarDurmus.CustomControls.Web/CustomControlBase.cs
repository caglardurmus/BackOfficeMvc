using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CaglarDurmus.CustomControls.Web
{
    public abstract class CustomControlBase : ICloneable
    {
        public CustomControlBase(string id, string caption)
        {
            this.Id = id;
            this.Caption = caption;

            Initilaze();
        }
        public CustomControlBase(string id)
        {
            this.Id = id;

            Initilaze();
        }
        public CustomControlBase()
        {
            Initilaze();
        }

        private void Initilaze()
        {
            this.Controls = new List<CustomControlBase>();
        }
        public string Id { get; protected internal set; }
        public string Caption { get; protected internal set; }
        protected internal List<CustomControlBase> Controls;

        public abstract MvcHtmlString RenderControl();

        public object Clone()
        {
            return this.MemberwiseClone();
        }


        public CustomControl.GridviewColumn AsGridviewColumn { get { return this as CustomControl.GridviewColumn; } }
        public CustomControl.Gridview AsGridview { get { return this as CustomControl.Gridview; } }
    }
}
