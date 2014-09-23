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
                _logger.Info(command.GetType() + " called ");
                _logger.Warn("Warning");
                _decoratedDispatcher.Dispatch(userSession,command);
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                Console.WriteLine(e.Message);
            }
        }
    }
}
