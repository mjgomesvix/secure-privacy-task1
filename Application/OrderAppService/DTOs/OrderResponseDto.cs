using System.Collections.Generic;

namespace ApplicationLayer.OrderAppService.DTOs
{
    public class OrderResponseDto : OrderListResponseDto
    {
        public IEnumerable<OrderItemListResponseDto> Items { get; set; }
    }
}