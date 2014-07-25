using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using IvoryTower.Domain.Entities;

namespace IvoryTower.Data
{
    public class UserAutoMappingOverride : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.Id().GeneratedBy.Assigned();
        }
    }
}