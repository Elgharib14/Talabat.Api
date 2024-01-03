using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.Entityes.OrderAggregate;

namespace Talabat.Core.Specification.OrderSpec
{
    public class OrderSpec :BaseSpecification<Order>
    {
        public OrderSpec(string email) : base(o=>o.BuyerEmail == email)
        {
            Includs.Add(o => o.DeleveryMethod);
            Includs.Add(o => o.Items);

            AddOrderByDesc(o => o.OrderDate);
            
        }


        public OrderSpec(int id,string email) : base(o => o.BuyerEmail == email&&o.Id == id)
        {
            Includs.Add(o => o.DeleveryMethod);
            Includs.Add(o => o.Items);

            AddOrderByDesc(o => o.OrderDate);

        }

    }
}
