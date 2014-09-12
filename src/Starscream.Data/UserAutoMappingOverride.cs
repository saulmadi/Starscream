using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Starscream.Domain.Entities;

namespace Starscream.Data
{
    public class UserAutoMappingOverride : IAutoMappingOverride<UserEmailLogin>
    {
        public void Override(AutoMapping<UserEmailLogin> mapping)
        {                      
        }
    }
}