using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starscream.Domain.Entities
{
   

    public class UserAbility : Entity
    {

        public virtual string description { get; protected set; }

        protected UserAbility()
        {
            
        }

        public UserAbility(string description)
        {
            this.description = description;
            Id = Guid.NewGuid();

        }


    }
}
