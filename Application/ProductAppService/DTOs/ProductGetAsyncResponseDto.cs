namespace ApplicationLayer.ProductAppService.DTOs
{
    public class ProductGetAsyncResponseDto : ProductGetListResponseDto
    {
        public string Unity { get; set; }
        public decimal Price { get; set; }
    }
}
