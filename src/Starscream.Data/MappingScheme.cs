using System;
using AcklenAvenue.Data;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using Starscream.Domain.Entities;

namespace Starscream.Data
{
    public class MappingScheme : IDatabaseMappingScheme<MappingConfiguration>
    {
        #region IDatabaseMappingScheme<MappingConfiguration> Members

        public Action<MappingConfiguration> Mappings
        {
            get
            {
                AutoPersistenceModel autoPersistenceModel = AutoMap.Assemblies(typeof (IEntity).Assembly)
                    .Where(t => typeof (IEntity).IsAssignableFrom(t))
                    .UseOverridesFromAssemblyOf<UserAutoMappingOverride>()
                    //.IncludeBase<LessonActionBase>()
                    .Conventions.Add(DefaultCascade.All());

                return x =>
                           {
                               x.AutoMappings.Add(autoPersistenceModel);
                               x.HbmMappings.AddFromAssemblyOf<ReadOnlyRepository>();
                           };
            }
        }

        #endregion
    }
}