using System;

namespace ApplicationLayer.OrderAppService.DTOs
{
    public class OrderGetAsyncResponseDto
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
