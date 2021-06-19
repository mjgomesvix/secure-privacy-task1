using DomainLayer.Base.Interfaces;
using Support.Common;
using System;
using System.Collections.Generic;

namespace DomainLayer.Entities.PersonModel
{
    [Serializable]
    public class Person : IDomainEntityRepositorable
    {
        private ISet<long> _phones;

        protected internal Person()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = SystemTime.Now();
            _phones = new HashSet<long>();
        }
        public virtual string Id { get; private set; }
        public virtual DateTime CreatedAt { get; private set; }
        public virtual string Name { get; protected internal set; }
        public virtual DateTime Birthday { get; protected internal set; }
        public virtual IEnumerable<long> Phones => _phones;

        protected internal virtual void Add(long phone)
        {
            _phones.Add(phone);
        }

        protected internal virtual void Add(IEnumerable<long> phones)
        {
            _phones = new HashSet<long>();
            foreach (var phone in phones)
                _phones.Add(phone);
        }
    }
}
