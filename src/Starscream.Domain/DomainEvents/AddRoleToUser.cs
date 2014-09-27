using System;

namespace Starscream.Domain.DomainEvents
{
    public class AddRoleToUser 
    {
        public Guid UserId { get; set; }
        public Guid RolId { get; set; }

        public AddRoleToUser(Guid userId, Guid rolId)
        {
            UserId = userId;
            RolId = rolId;
        }
    }
}