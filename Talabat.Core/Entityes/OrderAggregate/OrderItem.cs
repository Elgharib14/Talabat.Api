using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entityes.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem( ProductItemOrder product, decimal price, int quantity)
        {
           
            this.product = product;
            Price = price;
            Quantity = quantity;
        }

      
        public ProductItemOrder product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
       
    }
}
