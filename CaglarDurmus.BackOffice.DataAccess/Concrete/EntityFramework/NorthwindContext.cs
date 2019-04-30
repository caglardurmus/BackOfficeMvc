﻿using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglarDurmus.BackOffice.DataAccess.Concrete.EntityFramework
{
    public class NorthwindContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        #region ConnectionString 

        //public NorthwindContext()
        //{
        //    this.Database.Connection.ConnectionString = @"data source=(localdb)\MSSQLLocalDB; initial catalog=Northwind; integrated security=true";
            
        //}

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasRequired(p => p.Category).WithMany(c => c.Products);
        }

    }
}
