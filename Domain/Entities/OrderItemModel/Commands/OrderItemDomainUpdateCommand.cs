using DomainLayer.Entities.ProductModel;

namespace DomainLayer.Entities.OrderItemModel.Commands
{
    public class OrderItemDomainUpdateCommand
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnityPrice { get; set; }
    }
}
