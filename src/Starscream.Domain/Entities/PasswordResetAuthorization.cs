using System;

namespace Starscream.Domain.Entities
{
    public class PasswordResetAuthorization:IEntity
    {
        protected PasswordResetAuthorization()
        {            
        }

        public PasswordResetAuthorization(Guid id, User user, DateTime created)
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