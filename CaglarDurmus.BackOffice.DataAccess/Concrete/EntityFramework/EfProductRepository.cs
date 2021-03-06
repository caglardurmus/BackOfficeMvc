﻿using CaglarDurmus.BackOffice.DataAccess.Abstract;
using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CaglarDurmus.BackOffice.DataAccess.Concrete.EntityFrameowork;

namespace CaglarDurmus.BackOffice.DataAccess.Concrete.EntityFramework
{
    public class EfProductPepository : EfEntityRepositoryBase<Product, NorthwindContext>, IProductRepository
    {
        NorthwindContext _context;
        public EfProductPepository(NorthwindContext context)
        {
            _context = context;
        }

        public List<Product> GetWithCategories()
        {
            return _context.Set<Product>().ToList();
        }


    }
}
