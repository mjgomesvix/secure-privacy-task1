using DomainLayer.Base.Interfaces;
using DomainLayer.Entities.OrderItemModel;
using Support.Common;
using System;
using System.Collections.Generic;

namespace DomainLayer.Entities.OrderModel
{
    public class Order : IDomainEntityRepositorable
    {
        private readonly List<OrderItem> _orderItems;
        protected internal Order()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = SystemTime.Now();

            _orderItems = new List<OrderItem>();
        }

        public virtual string Id { get; private set; }

        public virtual DateTime CreatedAt { get; private set; }
        public virtual string PersonId { get; protected internal set; }

        public virtual IEnumerable<OrderItem> Items => _orderItems;

        protected internal virtual void Add(IEnumerable<OrderItem> orderItems)
        {
            _orderItems.AddRange(orderItems);
        }
    }
}
