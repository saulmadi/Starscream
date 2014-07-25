using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using IvoryTower.Domain.Entities;

namespace IvoryTower.Data
{
    public class UserLoginSessionAutoMappingOverride : IAutoMappingOverride<UserLoginSession>
    {
        public void Override(AutoMapping<UserLoginSession> mapping)
        {
            mapping.Id().GeneratedBy.Assigned();
        }
    }
}