using DomainLayer.Entities.ProductModel;

namespace DomainLayer.Entities.OrderModel.Commands
{
    public class OrderItemDomainAddCommand
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnityPrice { get; set; }
    }
}
