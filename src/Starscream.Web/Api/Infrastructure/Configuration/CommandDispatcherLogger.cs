using System;
using System.Diagnostics;
using log4net;
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
              _logger.Error("Error calling " + command.GetType());
              _logger.Error(command);
              _logger.Error(e.Message);

            }
        }
    }
}
