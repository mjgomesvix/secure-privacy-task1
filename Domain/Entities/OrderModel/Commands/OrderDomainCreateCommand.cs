using DomainLayer.Entities.PersonModel;
using System.Collections.Generic;

namespace DomainLayer.Entities.OrderModel.Commands
{
    public class OrderDomainCreateCommand
    {
        public Person Person { get; set; }
        public IEnumerable<OrderItemDomainAddCommand> Items { get; set; }
    }
}
