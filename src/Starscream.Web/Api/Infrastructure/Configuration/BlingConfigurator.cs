using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BlingBag;
using Castle.DynamicProxy;
using Starscream.Domain;
using Starscream.Notifications;

namespace Starscream.Web.Api.Infrastructure.Configuration
{
    

    public class BlingConfigurator : IBlingConfigurator<DomainEvent>
    {
        readonly IBlingDispatcher _dispatcher;

        public BlingConfigurator(IBlingDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            
        }

        #region IBlingConfigurator<DomainEvent> Members

        public object GetHandler(object obj)
        {
            
            return obj;
        }

        public Func<EventInfo, bool> EventSelector
        {
            get { return x => x.EventHandlerType == typeof(DomainEvent); }
        }

        public DomainEvent HandleEvent
        {
            get { return x =>
                         {
                             
                                 _dispatcher.Dispatch(x);
                             
                             
                         }; }
        }



        
        #endregion
    }
}