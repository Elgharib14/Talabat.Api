using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entityes;
using Talabat.Core.Entityes.OrderAggregate;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Talabat.Core.Specification.OrderSpec;

namespace Talapat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRep basketRep;
        private readonly IUnitOfWork unitOfWork;

        //private readonly IGenaricRepo<Product> genaricRepo;
        //private readonly IGenaricRepo<DeleveryMethod> delevery;
        //private readonly IGenaricRepo<Order> orderrepo;

        public OrderService(
            IBasketRep basketRep , 
            IUnitOfWork unitOfWork


            //IGenaricRepo<Product> genaricRepo , 
            //IGenaricRepo<DeleveryMethod> delevery , 
            //IGenaricRepo<Order> orderrepo
            )
        {
            this.basketRep = basketRep;
            this.unitOfWork = unitOfWork;
            //this.genaricRepo = genaricRepo;
            //this.delevery = delevery;
            //this.orderrepo = orderrepo;
        }
        public async Task<Order> CreatOrder(string buyerEmil, string BasketId, int delviryMethodId, Address address)
        {
            // 1. Get Basket From BasketRepo
            var basket = await basketRep.GetBasket(BasketId);

           // 2. Get Select Item From at Basket From Product Repo

            var orderItems = new List<OrderItem>(); 
            if(basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await unitOfWork.Repository<Product>().GetbyId(item.Id);

                    var productItemorder = new ProductItemOrder(product.Id , product.Name , product.PictureUrl);

                    var orderItem = new OrderItem(productItemorder , product.Price , item.Quntity);
               
                    orderItems.Add(orderItem);
                }
            }

            // 3. Calculat SubTotal

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity );

            //4. Get Delivery Method From Delivery Method Repo
 
            var deliverymethod = await unitOfWork.Repository<DeleveryMethod>().GetbyId(delviryMethodId);

           
            // 5. Creat Order

            var order = new Order(buyerEmil , address , deliverymethod , orderItems , subTotal);
            await unitOfWork.Repository<Order>().Add(order);

            // 6. Save To database

           var result = await unitOfWork.Complete();

            if(result <= 0)
            {
                return null;
            }
            return order;

        }

        public async Task<IReadOnlyList<DeleveryMethod>> GetDeleveryMethod()
        {
            var delivery = await unitOfWork.Repository<DeleveryMethod>().GetAll();
            return delivery;
        }

        public async Task<Order> GetOrderById(int otderId, string buyerEmil)
        {
            var spec =  new OrderSpec( otderId,buyerEmil);
            var order = await unitOfWork.Repository<Order>().GetbyIdWithSpec(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUser(string buyerEmil)
        {
            var spec = new OrderSpec(buyerEmil);
            var orders = await unitOfWork.Repository<Order>().GetWithSpecAll(spec);
            return orders;
        }
    }
}
