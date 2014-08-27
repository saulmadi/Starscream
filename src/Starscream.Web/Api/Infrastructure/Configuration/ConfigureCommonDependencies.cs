using System;
using System.Linq;
using Autofac;
using AutoMapper;
using BlingBag;
using Starscream.Data;
using Starscream.Domain;
using StarScream.Domain.Email;
using Starscream.Domain.Services;
using StarScream.EmailClients.DotNet;
using StarScream.TemplateEngines.Razor;
using Starscream.Web.Api.Infrastructure.Authentication;
using Starscream.Web.emails;

namespace Starscream.Web.Api.Infrastructure.Configuration
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
                           AutoRegisterEmailTemplates(container);
                       };
            }
        }

        #endregion

        static void ConfigureCommandAndEventHandlers(ContainerBuilder container)
        {
            container.RegisterType<BlingInitializer<DomainEvent>>().As<IBlingInitializer<DomainEvent>>();
            container.RegisterType<BlingConfigurator>().As<IBlingConfigurator<DomainEvent>>();
            container.RegisterType<AutoFacBlingDispatcher>().As<IBlingDispatcher>();
            container.RegisterType<ImmediateCommandDispatcher>().As<ICommandDispatcher>();
        }

        static void AutoRegisterEmailTemplates(ContainerBuilder container)
        {
            container.RegisterAssemblyTypes(typeof (PasswordResetEmailTemplate).Assembly)
                .Where(x => typeof (IEmailBodyTemplate).IsAssignableFrom(x) ||
                            typeof (IEmailSubjectTemplate).IsAssignableFrom(x)
                ).AsImplementedInterfaces();
        }

        static void AutoRegisterDataAndDomain(ContainerBuilder container)
        {
            container
                .RegisterAssemblyTypes(new[]
                                       {
                                           typeof (ReadOnlyRepository).Assembly,
                                           typeof (IEntity).Assembly,
                                           typeof (DotNetSmtpClient).Assembly,
                                           typeof (EmailSender).Assembly,
                                           typeof (RazorViewEngine).Assembly                                          
                                       })
                .Where(x => x.GetInterfaces().Any())
                .AsImplementedInterfaces();
        }
    }
}