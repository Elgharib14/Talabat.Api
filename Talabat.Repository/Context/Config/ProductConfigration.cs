using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;

namespace Talabat.Repository.Context.Config
{
    internal class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // we will write the Fluent api here

            builder.HasOne(p => p.ProductType).WithMany()
                .HasForeignKey(p=>p.ProductTypeId);
            builder.HasOne(p => p.ProductBrand).WithMany();

            builder.Property(p=>p.Name).IsRequired().HasMaxLength(128);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
