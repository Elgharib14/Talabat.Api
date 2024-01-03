using AutoMapper;
using Talabat.Api.DTOS;
using Talabat.Core.Entityes;
using static System.Net.WebRequestMethods;

namespace Talabat.Api.Hellper
{
    public class ProductPictuerReslovUrl : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictuerReslovUrl(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl)) 
            {
                return $"{configuration["ApiBaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty ;
        }
    }
}
