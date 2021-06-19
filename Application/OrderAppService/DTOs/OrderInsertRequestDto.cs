using System.Collections.Generic;

namespace ApplicationLayer.OrderAppService.DTOs
{
    public class OrderInsertRequestDto
    {
        public string PersonId { get; set; }
        public List<OrderItemAddRequest> Items { get; set; }
    }
}
