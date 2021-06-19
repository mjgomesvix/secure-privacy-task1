using System;

namespace DomainLayer.Base.Interfaces
{
    public interface IDomainEntityRepositorable
    {
        string Id { get; }
        DateTime CreatedAt { get; }
    }
}
