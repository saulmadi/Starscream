using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starscream.Notifications
{
    public class CommandDispatcherLogger:ICommandDispatcher
    {
        readonly ICommandDispatcher _decoratedDispatcher;

        public CommandDispatcherLogger(ICommandDispatcher decoratedDispatcher)
        {
            _decoratedDispatcher = decoratedDispatcher;
        }

        public void Dispatch(IUserSession userSession, object command)
        {

            try
            {
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
