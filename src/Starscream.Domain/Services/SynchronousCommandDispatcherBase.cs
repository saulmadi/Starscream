using System;
using System.Collections;
using System.Reflection;

namespace Starscream.Domain.Services
{
    public abstract class SynchronousCommandDispatcherBase : ICommandDispatcher
    {
        public void Dispatch(IUserSession userSession, object command)
        {
            foreach (var validator in GetMatchingCommandValidators(command))
            {
                MethodInfo handlerMethod = validator.GetType().GetMethod("Validate");
                handlerMethod.Invoke(validator, new[] { userSession, command });
            }

            foreach (object handler in GetMatchingCommandHandlers(command))
            {
                InitializeHandler(handler);
                MethodInfo handlerMethod = handler.GetType().GetMethod("Handle");
                handlerMethod.Invoke(handler, new[] {userSession, command});
            }
        }

        IEnumerable GetMatchingCommandHandlers(object command)
        {
            Type handlerType = typeof(ICommandHandler<>);
            Type genericHandlerType = handlerType.MakeGenericType(command.GetType());
            return FindHandlers(genericHandlerType);
        }

        IEnumerable GetMatchingCommandValidators(object command)
        {
            Type handlerType = typeof(ICommandValidator<>);
            Type genericHandlerType = handlerType.MakeGenericType(command.GetType());
            return FindValidators(genericHandlerType);
        }

        protected abstract void InitializeHandler(object handler);
        protected abstract IEnumerable FindHandlers(Type genericCommandHandlerType);
        protected abstract IEnumerable FindValidators(Type genericCommandValidatorType);
    }
}