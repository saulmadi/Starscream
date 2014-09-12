using System;
using Starscream.Domain.Services;

namespace Starscream.Domain.Entities
{
    public class UserLoginSession : Entity, IUserSession
    {
        protected UserLoginSession()
        {            
        }

        public UserLoginSession(Guid token, UserEmailLogin user, DateTime expires)
        {
            Id = token;
            User = user;
            Expires = expires;
        }

        public virtual UserEmailLogin User { get; protected set; }
        public virtual DateTime Expires { get; protected set; }
    }
}