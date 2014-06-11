using System;

namespace IvoryTower.Domain
{
    public class UserSession : IEntity, IUserSession
    {
        public virtual Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime Expires { get; set; }
    }
}