using System;
using AcklenAvenue.Testing.Moq.ExpectedObjects;
using Machine.Specifications;
using Moq;
using Starscream.Domain.DomainEventHandlers;
using Starscream.Domain.DomainEvents;
using Starscream.Domain.Entities;
using Starscream.Domain.Services;
using Starscream.Domain.ValueObjects;
using It = Machine.Specifications.It;

namespace Starscream.Domain.Specs
{
    public class when_sending_a_an_email_after_creating_a_password_reset_token
    {
        const string Email = "bob@email.com";
        static PasswordResertEmailSender _eventHandler;
        static readonly Guid UserId = Guid.NewGuid();
        static readonly Guid TokenId = Guid.NewGuid();
        static IEmailSender _emailSender;

        Establish context =
            () =>
            {
                var readOnlyRepository = Mock.Of<IReadOnlyRepository>();
                _emailSender = Mock.Of<IEmailSender>();
                _eventHandler = new PasswordResertEmailSender(readOnlyRepository, _emailSender);

                var value = new User("Bob", Email, new EncryptedPassword("something"));
                Mock.Get(readOnlyRepository)
                    .Setup(x => x.GetById<User>(UserId))
                    .Returns(value);
            };

        Because of =
            () => _eventHandler.Handle(new PasswordResetTokenCreated(TokenId, UserId));

        It should_send_the_email =
            () => Mock.Get(_emailSender).Verify(x => x.Send(Email, WithExpected.Object(new PasswordResetEmail(TokenId))));
    }
}