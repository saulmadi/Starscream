using System;

namespace Starscream.Domain.DomainEvents
{
    public class UserEnabled
    {
        public Guid id { get; set; }

        public UserEnabled(Guid id)
        {
            this.id = id;
        }

        protected UserEnabled()
        {
            
        }
    }
}