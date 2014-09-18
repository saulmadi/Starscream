using System;
using Starscream.Domain.Services;
using Starscream.Notifications;

namespace Starscream.Domain.Entities
{
    public class UserLoginSession : Entity, IUserSession
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

        public virtual User User { get; protected set; }
        public virtual DateTime Expires { get; protected set; }
    }
}