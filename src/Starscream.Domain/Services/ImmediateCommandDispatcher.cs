using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BlingBag;

namespace Starscream.Domain.Services
{
    public class ImmediateCommandDispatcher : SynchronousCommandDispatcherBase
    {
        readonly IBlingInitializer<DomainEvent> _blingInitializer;
        readonly IEnumerable<ICommandHandler> _commandHandlers;
        readonly IEnumerable<ICommandValidator> _commandValidators;

        public ImmediateCommandDispatcher(IBlingInitializer<DomainEvent> blingInitializer, IEnumerable<ICommandHandler> commandHandlers, IEnumerable<ICommandValidator> commandValidators)
        {
            _blingInitializer = blingInitializer;
            _commandHandlers = commandHandlers;
            _commandValidators = commandValidators;
        }

        protected override void InitializeHandler(object handler)
        {
            _blingInitializer.Initialize(handler);
        }

        protected override IEnumerable FindHandlers(Type genericCommandType)
        {
            return _commandHandlers.Where(x => x.GetType().GetInterfaces().Any(i => i == genericCommandType));
        }

        protected override IEnumerable FindValidators(Type genericCommandValidatorType)
        {
            return _commandValidators.Where(x => x.GetType().GetInterfaces().Any(i => i == genericCommandValidatorType));
        }
    }
}