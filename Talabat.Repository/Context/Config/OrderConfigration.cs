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
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //this line to mack relation between taples(order,Address) [1:1] 
            builder.OwnsOne(o => o.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());
            // this to convert the data in enum to string whin saving in data base(first lamdaExpresion)
            // (second lamdaExpresion) to when return from database [return as enum OrderStatus]
            builder.Property(o => o.Status)
                .HasConversion(
                    OStatus => OStatus.ToString(),
                    Ostatus =>(OrderStatus) Enum.Parse(typeof(OrderStatus), Ostatus)
                );
            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
