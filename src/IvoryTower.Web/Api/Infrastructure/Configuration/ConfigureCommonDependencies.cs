using System;
using System.Reflection;
using AutoMapper;
using Autofac;
using BlingBag;
using IvoryTower.Domain;
using IvoryTower.Domain.Services;
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

                               ConfigureCommandAndEventHandlers(container);
                           };
            }
        }

        #endregion

        static void ConfigureCommandAndEventHandlers(ContainerBuilder container)
        {
            container.RegisterType<BlingInitializer<DomainEvent>>().As<IBlingInitializer<DomainEvent>>();
            container.RegisterType<BlingConfigurator>().As<IBlingConfigurator<DomainEvent>>();
            container.RegisterType<AutoFacBlingDispatcher>().As<IBlingDispatcher>();
            container.RegisterType<SynchronousCommandDispatcher>().As<ICommandDispatcher>();
        }

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