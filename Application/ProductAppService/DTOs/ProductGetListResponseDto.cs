using System;

namespace ApplicationLayer.ProductAppService.DTOs
{
    public class ProductGetListResponseDto
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
    }
}
