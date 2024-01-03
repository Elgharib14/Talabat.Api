using AutoMapper;
using Talabat.Api.DTOS;
using Talabat.Core.Entityes;
using Talabat.Core.Entityes.OrderAggregate;

namespace Talabat.Api.Hellper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(p => p.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(p => p.ProductType.Name))
                .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductPictuerReslovUrl>());

            CreateMap< Talabat.Core.Entityes.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<AddressDto, Talabat.Core.Entityes.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(o => o.DeleveryMethod, o => o.MapFrom(s => s.DeleveryMethod.ShortName))
                .ForMember(o => o.DeleveryMethodCost, o => o.MapFrom(s => s.DeleveryMethod.Cost));


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(o => o.ProductId, o => o.MapFrom(s => s.product.ProductId))
                .ForMember(o => o.ProductName, o => o.MapFrom(s => s.product.ProductName))
                .ForMember(o => o.PicturUrl, o => o.MapFrom(s => s.product.PicturUrl))
                .ForMember(o => o.PicturUrl, o => o.MapFrom<OrderItemPicturUrlReslover>());


        }
    }
}
