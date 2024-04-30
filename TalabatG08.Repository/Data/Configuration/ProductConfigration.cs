using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;

namespace TalabatG08.Repository.Data.Configuration
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P=>P.Name).IsRequired().HasMaxLength(100);
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P => P.PictureUrl).IsRequired();
            builder.Property(P => P.Price).IsRequired().HasColumnType("decimal(18,2)");


            builder.HasOne(p => p.productBrand)
                   .WithMany()
                   .HasForeignKey(p=>p.ProductBrandId);

            builder.HasOne(p => p.productType)
                   .WithMany()
                   .HasForeignKey(p => p.ProductTypeId);


        }
    }
}
