using CaglarDurmus.BackOffice.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaglarDurmus.BackOffice.Entities.Concrete;
using CaglarDurmus.BackOffice.DataAccess.Abstract;

namespace CaglarDurmus.BackOffice.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }
    }
}
