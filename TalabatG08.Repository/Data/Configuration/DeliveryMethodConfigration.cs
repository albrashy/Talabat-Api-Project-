﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites.Order_Aggregate;

namespace TalabatG08.Repository.Data.Configuration
{
    public class DeliveryMethodConfigration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(DM => DM.Cost)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
