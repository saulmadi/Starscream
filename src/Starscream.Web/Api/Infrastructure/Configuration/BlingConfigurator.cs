using System;
using System.Reflection;
using BlingBag;
using Starscream.Domain;

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

        public Func<EventInfo, bool> EventSelector
        {
            get { return x => x.EventHandlerType == typeof(DomainEvent); }
        }

        public DomainEvent HandleEvent
        {
            get { return x => _dispatcher.Dispatch(x); }
        }

        #endregion
    }
}