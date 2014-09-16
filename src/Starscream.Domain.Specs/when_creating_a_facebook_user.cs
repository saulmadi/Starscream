using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using Machine.Specifications;
using Moq;
using NHibernate;
using Starscream.Domain.Application.CommandHandlers;
using Starscream.Domain.Application.Commands;
using Starscream.Domain.DomainEvents;
using Starscream.Domain.Entities;
using Starscream.Domain.Services;
using It = Machine.Specifications.It;

namespace Starscream.Domain.Specs
{
    public class when_creating_a_facebook_user
    {
        static CreateFacebookLoginUser _command;
        static IWriteableRepository _writeableRepository;
        static ICommandHandler<CreateFacebookLoginUser> _handler;
        static UserFacebookCreated _expectedEvent;
        static object _eventRaised;

        Establish context =
            () =>
            {

                _command = Builder<CreateFacebookLoginUser>.CreateNew().Build();

                _writeableRepository = Mock.Of<IWriteableRepository>();

                _handler = new UserFacebookCreator(_writeableRepository);

                _expectedEvent = new UserFacebookCreated(_command.email, _command.name, _command.id);
                _handler.NotifyObservers += x => _eventRaised = x;

            };

        Because of =
            () => { _handler.Handle(Mock.Of<IUserSession>(), _command); };

        It should_create_facebook_user =
            () =>
            {
                Mock.Get(_writeableRepository).Verify(
                    repository => 
                        repository.Create(Moq.It.Is<UserFacebookLogin>(
                        z => z.FirstName == _command.firstName &&
                        z.LastName == _command.lastName &&
                        z.FacebookId == _command.id &&
                        z.ImageUrl == _command.imageUrl &&
                        z.URL == _command.link &&
                        z.Name == _command.name

                        )
                        ));
            };

        It should_throw_the_expected_event =
           () => _eventRaised.ShouldBeLike(_expectedEvent);
    }
}
