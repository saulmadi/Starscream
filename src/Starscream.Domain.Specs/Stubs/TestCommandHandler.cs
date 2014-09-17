using Starscream.Domain.Services;
using Starscream.Notifications;

namespace Starscream.Domain.Specs.Stubs
{
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public TestCommand CommandHandled { get; private set; }

        public void Handle(IUserSession userIssuingCommand, TestCommand command)
        {
            CommandHandled = command;
            if (NotifyObservers != null) NotifyObservers(new TestEvent(command));
        }

        public event DomainEvent NotifyObservers;
    }
}