using System;
using IvoryTower.Domain.Services;

namespace IvoryTower.Domain.Entities
{
    public class UserLoginSession : IEntity, IUserSession
    {
        protected UserLoginSession()
        {            
        }

        public UserLoginSession(Guid token, User user, DateTime expires)
        {
            Id = token;
            User = user;
            Expires = expires;
        }

        public virtual Guid Id { get; protected set; }
        public virtual User User { get; protected set; }
        public virtual DateTime Expires { get; protected set; }
    }
}