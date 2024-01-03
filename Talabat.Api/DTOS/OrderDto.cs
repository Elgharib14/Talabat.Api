using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entityes.OrderAggregate;

namespace Talabat.Api.DTOS
{
    public class OrderDto
    {
        [Required]
        public string basketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }

        public AddressDto address { get; set; }
    }
}
