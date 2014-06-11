using System;
using System.Reflection;
using AutoMapper;
using Autofac;
using IvoryTower.Web.Api.Infrastructure.Authentication;

namespace IvoryTower.Web.Api.Infrastructure.Configuration
{
    public class ConfigureCommonDependencies : IBootstrapperTask<ContainerBuilder>
    {
        #region IBootstrapperTask<ContainerBuilder> Members

        public Action<ContainerBuilder> Task
        {
            get
            {
                return container =>
                           {
                               AutoRegisterDataAndDomain(container);
                               container.RegisterInstance(Mapper.Engine).As<IMappingEngine>();
                               container.RegisterType<ApiUserMapper>().As<IApiUserMapper<Guid>>();
                           };
            }
        }

        #endregion

        static void AutoRegisterDataAndDomain(ContainerBuilder container)
        {
            Assembly data = Assembly.Load("IvoryTower.Data");
            Assembly domain = Assembly.Load("IvoryTower.Domain");

            container
                .RegisterAssemblyTypes(data, domain)
                .AsImplementedInterfaces();
        }
    }
}