using DomainLayer.Entities.OrderItemModel;
using DomainLayer.Entities.OrderItemModel.Commands;
using DomainLayer.Entities.OrderModel.Commands;
using DomainLayer.Resources;
using Support.ExceptionsManagement;
using System.Linq;

namespace DomainLayer.Entities.OrderModel
{
    public static class OrderFactory
    {
        public static Order Create(this OrderDomainCreateCommand orderDomainCreateCommand)
        {
            orderDomainCreateCommand.CheckIntegrity();

            var order = new Order() { PersonId = orderDomainCreateCommand.Person.Id };

            var orderItems = orderDomainCreateCommand.Items.Select(i =>
            {
                return new OrderItemDomainCreateCommand
                {
                    Order = order,
                    Product = i.Product,
                    Quantity = i.Quantity,
                    UnityPrice = i.UnityPrice
                }.Create();
            });

            order.Add(orderItems);

            return order;
        }

        private static void CheckIntegrity(this OrderDomainCreateCommand orderDomainCreateCommand)
        {
            var integrityCheckup = new IntegrityCheckup();
            integrityCheckup.CheckRequired(orderDomainCreateCommand.Person, string.Format(DomainMessages.Required, "Person"));
            integrityCheckup.CheckIsTrue(orderDomainCreateCommand.Items.Any(), string.Format(DomainMessages.InformAtLeastOne, "Order Item"));

            integrityCheckup.ThrowExceptions();
        }
    }
}
