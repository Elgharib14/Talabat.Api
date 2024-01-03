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
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(orderitem => orderitem.product, x => x.WithOwner());
            builder.Property(orderitem => orderitem.Price)
               .HasColumnType("decimal(18,2)");
        }
    }
}
