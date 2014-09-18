using System;

namespace Starscream.Domain.Entities
{
    public class Profile : Entity
    {
        public virtual string Name { get; protected set; }

        protected Profile()
        {
        }

        public Profile(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}