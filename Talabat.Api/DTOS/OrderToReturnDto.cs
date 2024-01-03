using Talabat.Core.Entityes.OrderAggregate;

namespace Talabat.Api.DTOS
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public string Status { get; set; } 
        public Address ShippingAddress { get; set; }
        public string DeleveryMethod { get; set; }
        public decimal DeleveryMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } 
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; } 
    }
}

