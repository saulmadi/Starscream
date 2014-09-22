using System;
using System.IO;
using System.Linq;
using AcklenAvenue.Email;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using AutoMapper;
using BlingBag;
using Castle.DynamicProxy;
using Starscream.Data;
using Starscream.Domain;
using Starscream.Domain.Services;
using StarScream.EmailClients.DotNet;
using Starscream.Notifications;
using StarScream.TemplateEngines.Razor;
using Starscream.Web.Api.emails;
using Starscream.Web.Api.Infrastructure.Authentication;

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
                           container.RegisterType<BaseUrlProvider>().As<IBaseUrlProvider>();
                           container.RegisterType<ApiUserMapper>().As<IApiUserMapper<Guid>>();
                          
                           
                          
                           ConfigureCommandAndEventHandlers(container);
                           AutoRegisterEmailTemplates(container);

                           AutoRegisterAllDomainEvents(container);
                           AutoRegisterAllCommandHandlers(container);
                       };
            }
        }
        void AutoRegisterAllCommandHandlers(ContainerBuilder container)
        {
            container.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.GetInterfaces().Any(i => i.Name.StartsWith("ICommandHandler")))
                .AsImplementedInterfaces();
        }

        void AutoRegisterAllDomainEvents(ContainerBuilder container)
        {
            container.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.GetInterfaces().Any(i => i.Name.StartsWith("IBlingHandler")))
                .AsImplementedInterfaces();
        }

        #endregion

        static void ConfigureCommandAndEventHandlers(ContainerBuilder container)
        {
            container.RegisterType<BlingInitializer<DomainEvent>>().As<IBlingInitializer<DomainEvent>>();
            container.RegisterType<BlingConfigurator>().As<IBlingConfigurator<DomainEvent>>();
            container.RegisterType<AutoFacBlingDispatcher>().As<IBlingDispatcher>();
            container.RegisterType<ImmediateCommandDispatcher>().Named<ICommandDispatcher>("implementor");

            container.RegisterDecorator<ICommandDispatcher>((c, inner) => new CommandDispatcherLogger(inner),"implementor" );
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
                .Where(x => typeof(ICommandHandler).IsAssignableFrom(x)!=true)
                
                .AsImplementedInterfaces();
        }
    }
}