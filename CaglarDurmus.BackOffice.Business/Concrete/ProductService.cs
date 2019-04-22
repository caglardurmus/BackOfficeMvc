using CaglarDurmus.BackOffice.Business.Abstract;
using CaglarDurmus.BackOffice.Business.Utilities;
using CaglarDurmus.BackOffice.Business.ValidationRules.FluentValidation;
using CaglarDurmus.BackOffice.DataAccess.Abstract;
using CaglarDurmus.BackOffice.DataAccess.Concrete;
using CaglarDurmus.BackOffice.DataAccess.Concrete.EntityFramework;
using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglarDurmus.BackOffice.Business.Concrete
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void SaveProduct(int? id,
            string productName,
            int categoryID,
            decimal unitPrice,
            string quantityPerUnit,
            Int16 unitsInStock)
        {
            var entity = new Product(); try
            {
                if (id.HasValue)
                {
                    entity = this.GetProduct(id.Value);
                    if (entity == null)
                    {
                        throw new Exception(String.Format("{0} Id'li ürün bulunamadı!", id.Value));
                    }
                }

                entity.ProductName = productName;
                entity.CategoryID = categoryID;
                entity.QuantityPerUnit = quantityPerUnit;
                entity.UnitPrice = unitPrice;
                entity.UnitsInStock = unitsInStock;

                if (id.HasValue)
                {
                    this.Update(entity);
                }
                else
                {
                    this.Add(entity);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);
            _productRepository.Update(product);
        }
        public void Add(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);
            _productRepository.Add(product);
        }
        public void Delete(Product product)
        {
            try
            {
                _productRepository.Delete(product);
            }
            catch
            {
                throw new Exception("Silinemedi!");
            }

        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productRepository.GetAll(p => p.CategoryID == categoryId);
        }

        public List<Product> GetProductsByProductName(string productName)
        {
            return _productRepository.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));
        }

        public List<Product> GetProductsByStock(int stock)
        {
            return _productRepository.GetAll(p => p.UnitsInStock <= stock);
        }

        public List<Product> GetByFilter(int? categoryId, string productName, int? stock)
        {

            var list = _productRepository.GetAll();

            if (categoryId.HasValue)
            {
                list = list.Where(x => x.CategoryID == categoryId).ToList();
            }
            if (!string.IsNullOrWhiteSpace(productName))
            {
                list = list.Where(x => x.ProductName.ToLower().Contains(productName.ToLower())).ToList();
            }
            if (stock.HasValue)
            {
                list = list.Where(x => x.UnitsInStock <= stock).ToList();
            }
            return list;
        }

        public Product GetProduct(int productId)
        {
            return _productRepository.Get(p => p.ProductID == productId);
        }
    }
}
