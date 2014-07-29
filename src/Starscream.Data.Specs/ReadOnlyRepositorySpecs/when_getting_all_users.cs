using System.Collections.Generic;
using AcklenAvenue.Data.NHibernate.Testing;
using Starscream.Domain;
using Starscream.Domain.Entities;
using Starscream.Domain.ValueObjects;
using Machine.Specifications;
using NHibernate;

namespace Starscream.Data.Specs.ReadOnlyRepositorySpecs
{
    public class when_getting_all_users
    {
        static ReadOnlyRepository _readOnlyRepository;
        static IEnumerable<User> _result;
        static List<User> _users;

        Establish context =
            () =>
                {
                    ISession session = InMemorySession.New(new MappingScheme());

                    _users = new List<User>
                                 {
                                     new User("test1", "test1@test.com", new EncryptedPassword("password")),
                                     new User("test2", "test2@test.com", new EncryptedPassword("password"))
                                 };

                    _users.ForEach(x => session.Save(x));
                    session.Flush();

                    _readOnlyRepository = new ReadOnlyRepository(session);
                };

        Because of =
            () => _result = _readOnlyRepository.GetAll<User>();

        It should_return_all_the_users =
            () =>
                {
                    _result.ShouldBeLike(_users);
                };
    }
}