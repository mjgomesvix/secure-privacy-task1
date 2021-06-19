using DomainLayer.Base.Interfaces;
using Support.Common;
using System;

namespace DomainLayer.Entities.ProductModel
{
    public class Product : IDomainEntityRepositorable
    {
        protected internal Product()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = SystemTime.Now();
        }

        public virtual string Id { get; private set; }

        public virtual DateTime CreatedAt { get; private set; }
        public virtual string Name { get; protected internal set; }
        public virtual string Unity { get; protected internal set; }
        public virtual decimal Price { get; protected internal set; }
    }
}
