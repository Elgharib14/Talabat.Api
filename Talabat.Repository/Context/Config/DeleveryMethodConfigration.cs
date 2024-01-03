using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes.OrderAggregate;

namespace Talabat.Repository.Context.Config
{
    internal class DeleveryMethodConfigration : IEntityTypeConfiguration<DeleveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeleveryMethod> builder)
        {
            builder.Property(D => D.Cost)
              .HasColumnType("decimal(18,2)");
        }
    }
}
