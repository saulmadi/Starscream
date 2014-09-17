using Starscream.Domain.Services;
using Starscream.Notifications;

namespace Starscream.Domain.Specs.Stubs
{
    public class TestCommandValidator : ICommandValidator<TestCommand>
    {
        public TestCommand CommandValidated { get; private set; }

        public void Validate(IUserSession userSession, TestCommand command)
        {
            CommandValidated = command;
        }
    }
}