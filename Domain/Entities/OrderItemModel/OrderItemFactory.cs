using DomainLayer.Entities.OrderItemModel.Commands;
using DomainLayer.Resources;
using Support.ExceptionsManagement;

namespace DomainLayer.Entities.OrderItemModel
{
    public static class OrderItemFactory
    {
        public static OrderItem Create(this OrderItemDomainCreateCommand orderItemDomainCreateCommand)
        {
            orderItemDomainCreateCommand.CheckIntegrityOnCreating();

            return new OrderItem()
            {
                OrderId = orderItemDomainCreateCommand.Order.Id,
                ProductId = orderItemDomainCreateCommand.Product.Id,
                Quantity = orderItemDomainCreateCommand.Quantity,
                UnityPrice = orderItemDomainCreateCommand.UnityPrice
            };
        }

        public static void Update(this OrderItem orderItem, OrderItemDomainUpdateCommand orderItemDomainUpdateCommand)
        {
            orderItemDomainUpdateCommand.CheckIntegrityOnUpdating();

            orderItem.ProductId = orderItemDomainUpdateCommand.Product.Id;
            orderItem.Quantity = orderItemDomainUpdateCommand.Quantity;
            orderItem.UnityPrice = orderItemDomainUpdateCommand.UnityPrice;
        }

        private static void CheckIntegrityOnCreating(this OrderItemDomainCreateCommand orderItemDomainCreateCommand)
        {
            var integrityCheckup = new IntegrityCheckup();
            integrityCheckup.CheckRequired(orderItemDomainCreateCommand.Order, string.Format(DomainMessages.Required, "Order"));
            integrityCheckup.CheckRequired(orderItemDomainCreateCommand.Product, string.Format(DomainMessages.Required, "Product"));
            integrityCheckup.CheckRequired(orderItemDomainCreateCommand.Quantity, string.Format(DomainMessages.Required, "Quantity"));
            integrityCheckup.CheckRequired(orderItemDomainCreateCommand.UnityPrice, string.Format(DomainMessages.Required, "Unity Price"));
            integrityCheckup.ThrowExceptions();
        }
        private static void CheckIntegrityOnUpdating(this OrderItemDomainUpdateCommand orderItemDomainUpdateCommand)
        {
            var integrityCheckup = new IntegrityCheckup();
            integrityCheckup.CheckRequired(orderItemDomainUpdateCommand.Product, string.Format(DomainMessages.Required, "Product"));
            integrityCheckup.CheckRequired(orderItemDomainUpdateCommand.Quantity, string.Format(DomainMessages.Required, "Quantity"));
            integrityCheckup.CheckRequired(orderItemDomainUpdateCommand.UnityPrice, string.Format(DomainMessages.Required, "Unity Price"));
            integrityCheckup.ThrowExceptions();
        }
    }
}
