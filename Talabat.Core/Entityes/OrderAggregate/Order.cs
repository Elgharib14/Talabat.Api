using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entityes.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order( string buyerEmail, Address shippingAddress, DeleveryMethod deleveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
           
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeleveryMethod = deleveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

      
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public DeleveryMethod DeleveryMethod { get; set; }
       
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        //[NotMapped]
        //public decimal Total  => SubTotal + DeleveryMethod.Cost; 
        public decimal GetTotal()
        => SubTotal + DeleveryMethod.Cost;

        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
