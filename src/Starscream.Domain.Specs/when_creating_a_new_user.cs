using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using Starscream.Domain.Application.CommandHandlers;
using Starscream.Domain.Application.Commands;
using Starscream.Domain.DomainEvents;
using Starscream.Domain.Entities;
using Starscream.Domain.Services;
using Starscream.Domain.ValueObjects;
using Starscream.Notifications;
using It = Machine.Specifications.It;

namespace Starscream.Domain.Specs
{
    public class when_creating_a_new_user
    {
        static CreateEmailLoginUser _command;
        static IWriteableRepository _writeableRepository;
        static ICommandHandler<CreateEmailLoginUser> _handler;
        static UserEmailCreated _expectedEvent;
        static object _eventRaised;
        static UserEmailLogin _userCreated;
        

        Establish context =
            () =>
            {
                _command = new CreateEmailLoginUser("email", new EncryptedPassword("password"), "name", "password");

                _userCreated = Builder<UserEmailLogin>.CreateNew()
                    .With(user => user.Email, _command.Email)
                    .With(user => user.Name, _command.Name)
                    .With(user => user.EncryptedPassword,_command.EncryptedPassword.Password)
                    .With(user => user.PhoneNumber, _command.PhoneNumber)
                    .Build();

                _writeableRepository = Mock.Of<IWriteableRepository>();
                Mock.Get(_writeableRepository)
                    .Setup(repository => repository.Create(Moq.It.IsAny<UserEmailLogin>()))
                    .Returns(_userCreated);

                _handler = new UserEmailCreator(_writeableRepository);

                _expectedEvent = new UserEmailCreated(_userCreated.Id,_command.Email, _command.Name, _command.PhoneNumber);
                _handler.NotifyObservers += x => _eventRaised = x;
            };

        Because of =
            () => _handler.Handle(Mock.Of<IUserSession>(), _command);

        It should_create_the_new_user =
            () => Mock.Get(_writeableRepository).Verify(
                x =>
                    x.Create(Moq.It.Is<UserEmailLogin>(u =>
                        u.Name == _command.Name
                        && u.Email == _command.Email
                        && u.EncryptedPassword == _command.EncryptedPassword.Password
                        && u.PhoneNumber == _command.PhoneNumber)));

        It should_throw_the_expected_event =
            () => _eventRaised.ShouldBeLike(_expectedEvent);
    }
}