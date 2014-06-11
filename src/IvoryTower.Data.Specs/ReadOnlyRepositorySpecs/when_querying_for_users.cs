using System.Collections.Generic;
using System.Linq;
using AcklenAvenue.Data.NHibernate.Testing;
using IvoryTower.Domain;
using IvoryTower.Domain.Entities;
using IvoryTower.Domain.ValueObjects;
using Machine.Specifications;
using NHibernate;

namespace IvoryTower.Data.Specs.ReadOnlyRepositorySpecs
{
    public class when_querying_for_users
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
                                     new User("test2-match", "test2@test.com", new EncryptedPassword("password")),
                                     new User("test3-match", "test2@test.com", new EncryptedPassword("password"))
                                 };

                    _users.ForEach(x => session.Save(x));
                    session.Flush();

                    _readOnlyRepository = new ReadOnlyRepository(session);
                };

        Because of =
            () => _result = _readOnlyRepository.Query<User>(x => x.Name.Contains("match"));

        It should_return_the_matching_users =
            () => _result.ShouldBeLike(_users.Where(x => x.Name.Contains("match")));
    }
}