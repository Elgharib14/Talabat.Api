using AutoMapper;
using Talabat.Api.DTOS;
using Talabat.Core.Entityes.OrderAggregate;

namespace Talabat.Api.Hellper
{
    public class OrderItemPicturUrlReslover : IValueResolver<OrderItem , OrderItemDto ,string>
    {
        private readonly IConfiguration configuration;

        public OrderItemPicturUrlReslover(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.product.PicturUrl))
            {
                return $"{configuration["ApiBaseUrl"]}{source.product.PicturUrl}";
            }
            return string.Empty;
        }
    }
}
