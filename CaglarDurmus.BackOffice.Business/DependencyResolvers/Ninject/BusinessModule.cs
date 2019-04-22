using Ninject.Modules;
using CaglarDurmus.BackOffice.Business.Abstract;
using CaglarDurmus.BackOffice.Business.Concrete;
using CaglarDurmus.BackOffice.DataAccess.Abstract;
using CaglarDurmus.BackOffice.DataAccess.Concrete.EntityFrameowork;
using CaglarDurmus.BackOffice.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglarDurmus.BackOffice.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductService>().InSingletonScope();
            Bind<IProductRepository>().To<EfProductPepository>().InSingletonScope();

            Bind<ICategoryService>().To<CategoryService>().InSingletonScope();
            Bind<ICategoryRepository>().To<EfCategoryRepository>().InSingletonScope();

            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IUserRepository>().To<EfUserRepository>().InSingletonScope();
        }

    }
}
