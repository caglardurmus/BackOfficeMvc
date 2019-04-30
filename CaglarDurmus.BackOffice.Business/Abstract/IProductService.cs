using CaglarDurmus.BackOffice.Business.Concrete;
using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglarDurmus.BackOffice.Business.Abstract
{
    public interface IProductService
    {
        Product GetProduct(int productId);
        List<Product> GetAll();
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetProductsByProductName(string productName);
        List<Product> GetProductsByStock(int stock);
        List<Product> GetByFilter(int? categoryId, string productName, int? stock);
        void Update(Product product);
        void Delete(Product product);
        void Add(Product product);
        void SaveProduct(int? id, string productName, int categoryID, decimal unitPrice, string quantityPerUnit, Int16 unitsInStock);
        List<Product> GetWithCategories();
    }
}
