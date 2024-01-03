using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes.OrderAggregate;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {
        Task<Order> CreatOrder(string buyerEmil , string BasketId , int delviryMethodId , Address address);
        Task<IReadOnlyList<Order>> GetOrderForUser(string buyerEmil);
        Task<Order> GetOrderById(int otderId , string buyerEmil);
        Task<IReadOnlyList<DeleveryMethod>> GetDeleveryMethod();
    }
}
