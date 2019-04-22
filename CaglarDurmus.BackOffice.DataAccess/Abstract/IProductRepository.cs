using CaglarDurmus.BackOffice.DataAccess.Concrete.EntityFramework;
using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglarDurmus.BackOffice.DataAccess.Abstract
{
    public interface IProductRepository : IEntityRepository<Product>
    {
    }
}
