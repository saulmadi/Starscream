using System;
using IvoryTower.Domain.ValueObjects;

namespace IvoryTower.Domain.Entities
{
    public class User : IEntity
    {
        protected User()
        {
        }

        public User(string name, string emailAddress, EncryptedPassword encryptedPassword)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = emailAddress;
            EncryptedPassword = encryptedPassword.Password;
        }

        public User(string name, string emailAddress, EncryptedPassword encryptedPassword, string phoneNumber) :this(name, emailAddress, encryptedPassword)
        {
            PhoneNumber = phoneNumber;            
        }

        public virtual string Name { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string PhoneNumber { get; protected set; }
        public virtual string EncryptedPassword { get; protected set; }

        #region IEntity Members

        public virtual Guid Id { get; protected set; }

        #endregion

        public virtual void ChangeEmailAddress(string emailAddress)
        {
            Email = emailAddress;
        }
    }
}