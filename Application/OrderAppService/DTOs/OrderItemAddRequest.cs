namespace ApplicationLayer.OrderAppService.DTOs
{
    public class OrderItemAddRequest
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnityPrice { get; set; }
    }
}
