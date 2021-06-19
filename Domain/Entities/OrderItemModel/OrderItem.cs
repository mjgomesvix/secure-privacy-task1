using DomainLayer.Base.Interfaces;
using Support.Common;
using System;

namespace DomainLayer.Entities.OrderItemModel
{
    public class OrderItem : IDomainEntityRepositorable
    {
        protected internal OrderItem()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = SystemTime.Now();
        }

        public virtual string Id { get; private set; }

        public virtual DateTime CreatedAt { get; private set; }
        public virtual string OrderId { get; protected internal set; }
        public virtual string ProductId { get; protected internal set; }
        public virtual int Quantity { get; protected internal set; }
        public virtual decimal UnityPrice { get; protected internal set; }
        public virtual decimal TotalPrice => Quantity * UnityPrice;
    }
}
