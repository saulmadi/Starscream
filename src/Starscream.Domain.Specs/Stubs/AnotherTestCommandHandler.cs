﻿using Starscream.Domain.Services;
using Starscream.Notifications;

namespace Starscream.Domain.Specs.Stubs
{
    public class AnotherTestCommandHandler : ICommandHandler<AnotherTestCommand>
    {
        public AnotherTestCommand CommandHandled { get; private set; }

        public void Handle(IUserSession userIssuingCommand, AnotherTestCommand command)
        {
            CommandHandled = command;
            if (NotifyObservers != null) NotifyObservers(new TestEvent(command));
        }

        public event DomainEvent NotifyObservers;
    }
}