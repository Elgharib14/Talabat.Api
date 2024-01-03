using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entityes.OrderAggregate
{
    public class DeleveryMethod : BaseEntity
    {
        public DeleveryMethod()
        {
            
        }
        public DeleveryMethod( string shortName, string description, decimal cost, string deleverTime)
        {
          
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deleverTime;
        }

      
        public string ShortName { get; set; }   
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; }
    }
}
