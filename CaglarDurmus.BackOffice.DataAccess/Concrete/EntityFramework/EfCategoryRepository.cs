using CaglarDurmus.BackOffice.DataAccess.Abstract;
using CaglarDurmus.BackOffice.DataAccess.Concrete.EntityFramework;
using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglarDurmus.BackOffice.DataAccess.Concrete.EntityFrameowork
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryRepository
    {
    }
}
