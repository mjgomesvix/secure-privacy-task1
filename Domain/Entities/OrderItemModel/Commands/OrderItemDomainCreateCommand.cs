using DomainLayer.Entities.OrderModel;

namespace DomainLayer.Entities.OrderItemModel.Commands
{
    public class OrderItemDomainCreateCommand : OrderItemDomainUpdateCommand
    {
        public Order Order { get; set; }
    }
}
