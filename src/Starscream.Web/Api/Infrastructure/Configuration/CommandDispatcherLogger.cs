using System;
using System.Diagnostics;
using System.Linq;
using log4net;
using NHibernate.Linq;
using Starscream.Notifications;

namespace Starscream.Web.Api.Infrastructure.Configuration
{
    public class CommandDispatcherLogger:ICommandDispatcher
    {
        readonly ICommandDispatcher _decoratedDispatcher;
        readonly ILog _logger;

        public CommandDispatcherLogger(ICommandDispatcher decoratedDispatcher, ILog logger)
        {
            _decoratedDispatcher = decoratedDispatcher;
            _logger = logger;
        }

        public void Dispatch(IUserSession userSession, object command)
        {

            try
            {
             
                _decoratedDispatcher.Dispatch(userSession,command);

               
            }
            catch (Exception e)
            {
              _logger.Error("1) Error calling " + command.GetType());
              var properties = command.GetType().GetProperties();
              var propertiesMessage = properties.Aggregate("", (current, property) => current + ("Property Name " + property.Name + " Property Value " + property.GetValue(command)));

              _logger.Error("2) "+ propertiesMessage);
              _logger.Error("3) "+e.Message);

            }
        }
    }
}
