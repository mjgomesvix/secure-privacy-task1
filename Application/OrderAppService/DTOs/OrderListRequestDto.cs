using System;

namespace ApplicationLayer.OrderAppService.DTOs
{
    public class OrderListRequestDto
    {
        public string PersonId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
