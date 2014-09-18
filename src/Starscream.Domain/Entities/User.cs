using System;
using Starscream.Domain.ValueObjects;

namespace Starscream.Domain.Entities
{
    public class User : Entity
    {
        public virtual string Name { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual Profile Profile { get; protected set; }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
            Id = Guid.NewGuid();
        }

        protected User()
        {
            
        }

        public virtual void ChangeEmailAddress(string emailAddress)
        {
            Email = emailAddress;
        }

        public virtual void AssignProfile(Profile profile)
        {
            Profile = profile;
        }
    }
}