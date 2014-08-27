using System;

namespace Starscream.Domain.Entities
{
    public class PasswordResetToken:IEntity
    {
        protected PasswordResetToken()
        {            
        }

        public PasswordResetToken(Guid id, User user, DateTime created)
        {
            Id = id;
            User = user;
            Created = created;
        }

        public virtual Guid Id { get; protected set; }
        public virtual User User { get; protected set; }
        public virtual DateTime Created { get; protected set; }
    }
}