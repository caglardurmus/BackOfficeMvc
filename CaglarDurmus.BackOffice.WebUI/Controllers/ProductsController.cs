﻿using CaglarDurmus.BackOffice.Business.Abstract;
using CaglarDurmus.BackOffice.Business.DependencyResolvers.Ninject;
using CaglarDurmus.BackOffice.Entities.Concrete;
using CaglarDurmus.CustomControls.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CaglarDurmus.CustomControls.Web.CustomControl;

namespace CaglarDurmus.BackOffice.WebUI.Controllers
{
    public class ProductsController : BaseController
    {
        public ActionResult Index()
        {
            this.SetPageTitle("Ürünler");
            return View();
        }

        public ActionResult productsGrid()
        {
            try
            {
                var grid = new Gridview("gridProduct", "Ürünler Tablosu");
                var data = InstanceFactory.GetInstance<IProductService>().GetAll();

                grid.CellPreparing += new CustomControl.CellPreparingEventHandler(grid_CellPreparing);

                grid.AddColumns(new GridviewColumn("Düzenle", "Edit"),
                                new GridviewColumn("Sil", "Delete"),
                                new GridviewColumn("Id", "ProductID"),
                                new GridviewColumn("Ürün Adı", "ProductName"),
                                new GridviewColumn("Kategori", "CategoryID"),
                                new GridviewColumn("Birim Adeti", "QuantityPerUnit"),
                                new GridviewColumn("Ürün Fiyatı", "UnitPrice"),
                                new GridviewColumn("Stok", "UnitsInStock")).DataBind(data);

                return Content(grid.RenderControl().ToString());
            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }
        void grid_CellPreparing(object sender, CustomControl.CellPreparingEventArgs e)
        {
            var entity = e.DataItem as Product;
            if (e.Column.Value == "Edit")
            {
                var url = Url.Action("Edit", new { id = entity.ProductID });
                e.Cell.SetCaption(new CustomControl.Link(url, "fas fa-edit").RenderControl().ToString());
            }
            if (e.Column.Value == "Delete")
            {
                var url = Url.Action("Delete", new { id = entity.ProductID });
                e.Cell.SetCaption(new CustomControl.Link(url, "fas fa-times").RenderControl().ToString());

            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            this.SetPageTitle("Ürünler - Detay");

            try
            {
                var entity = new Product();
                if (id.HasValue)
                {
                    entity = InstanceFactory.GetInstance<IProductService>().GetProduct(id.Value);
                }

                return View(entity);

            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }

        [HttpPost]
        public ActionResult Edit(
            int? id,
            string productName,
            string categoryID,
            string unitPrice,
            string quantityPerUnit,
            string unitsInStock)
        {
            this.SetPageTitle("Ürünler - Detay");

            try
            {
                InstanceFactory.GetInstance<IProductService>().SaveProduct(
                    id: id,
                    productName: productName,
                    categoryID: Convert.ToInt32(categoryID),
                    unitPrice: Convert.ToDecimal(unitPrice),
                    quantityPerUnit: quantityPerUnit,
                    unitsInStock: Convert.ToInt16(unitsInStock));

                return RedirectWithAlertMessage("İşlem Başarılı!", "Index");

            }
            catch (Exception ex)
            {
                var e = ex.Message;
                return RedirectWithAlertMessage("İşlem Başarısız!", "Edit", id);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            this.SetPageTitle("Ürünler - Detay");

            try
            {
                var productService = InstanceFactory.GetInstance<IProductService>();
                var entity = productService.GetProduct(id);
                productService.Delete(entity);
                return RedirectWithAlertMessage("İşlem Başarılı", "Index");

            }
            catch (Exception ex)
            {
                return Content(ex.Message.ToString());
            }
        }
    }
}