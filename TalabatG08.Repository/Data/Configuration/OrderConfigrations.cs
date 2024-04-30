using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites.Order_Aggregate;

namespace TalabatG08.Repository.Data.Configuration
{
    public class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Status)
                 .HasConversion(Ostuts => Ostuts.ToString(), Ostuts =>(OrderStatus) Enum.Parse(typeof(OrderStatus), Ostuts));

            builder.Property(O => O.Subtotal)
                   .HasColumnType("decimal(18,2)");

            builder.OwnsOne(O=> O.ShippingAddress, SA => SA.WithOwner());

            builder.HasOne(o => o.DeliveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
