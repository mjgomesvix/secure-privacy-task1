using System;

namespace ApplicationLayer.OrderAppService.DTOs
{
    public class OrderListResponseDto
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
