using System;

namespace Starscream.Domain.Application.Commands
{
    public class UserRoleAdded 
    {
        public Guid UserId { get; protected set; }
        public Guid RolId { get; protected set; }

        public UserRoleAdded(Guid userId, Guid rolId)
        {
            UserId = userId;
            RolId = rolId;
        }
    }
}