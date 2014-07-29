using System;

namespace Starscream.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}