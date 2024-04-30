using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Entites.Order_Aggregate;
using TalabatG08.Repository.Data.Configuration;

namespace TalabatG08.Repository.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //  modelBuilder.ApplyConfiguration(new ProductConfigration());

           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());   

           // base.OnModelCreating(modelBuilder);

        }


        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }  

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }
}
