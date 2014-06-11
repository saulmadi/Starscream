using System;
using IvoryTower.Domain.Services;

namespace IvoryTower.Domain.Entities
{
    public class UserLoginSession : IEntity, IUserSession
    {
        public virtual Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime Expires { get; set; }
    }
}