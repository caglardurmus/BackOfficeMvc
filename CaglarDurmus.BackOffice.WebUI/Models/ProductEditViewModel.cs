using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaglarDurmus.BackOffice.WebUI.Models
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}