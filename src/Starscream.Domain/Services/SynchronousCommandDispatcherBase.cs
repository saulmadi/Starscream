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
                InvokeMethod("Validate", validator, userSession, command);
            }

            foreach (var handler in GetMatchingCommandHandlers(command))
            {
                InitializeHandler(handler);
                InvokeMethod("Handle", handler, userSession, command);
            }
        }

        static void InvokeMethod(string methodName, object invokableObject, params object[] methodArgs)
        {
            MethodInfo handlerMethod = invokableObject.GetType().GetMethod(methodName);
            handlerMethod.Invoke(invokableObject, methodArgs);
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