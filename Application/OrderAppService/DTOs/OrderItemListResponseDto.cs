using System;

namespace ApplicationLayer.OrderAppService.DTOs
{
    public class OrderItemListResponseDto
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnityPrice { get; set; }
    }
}
