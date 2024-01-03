using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.Entityes.OrderAggregate;

namespace Talabat.Repository.Context
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Here we Should Write the FluentApi But We Well Write it
            //in Folder Config in class ProductConfigration and we implement the config in this method (OnModelCreating )
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> productTypes { get; set; }
        public DbSet<Order> orders { get; set; }
        public  DbSet<DeleveryMethod> DeleveryMethods { get; set;}
        public DbSet<OrderItem> OrderItems { get; set; }   
    }
}
