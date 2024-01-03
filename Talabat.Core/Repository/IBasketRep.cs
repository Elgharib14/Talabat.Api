using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;

namespace Talabat.Core.Repository
{
    public interface IBasketRep
    {
        Task<CustomerBasket?> GetBasket(string basketId);
        Task<CustomerBasket> UpdateBasket(CustomerBasket basket);
        Task<bool> DeleteBasket(string basketId);
    }
}
