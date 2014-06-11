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
    public class when_getting_first_or_default_user_by_query_with_match
    {
        static ReadOnlyRepository _readOnlyRepository;
        static User _result;
        static User _userToFind;

        Establish context =
            () =>
                {
                    ISession session = InMemorySession.New(new MappingScheme());

                    var users = new List<User>
                                    {
                                        new User("test1", "test1@test.com", new EncryptedPassword("password")),
                                        new User("test2-match", "test2@test.com", new EncryptedPassword("password")),
                                        new User("test3", "test2@test.com", new EncryptedPassword("password"))
                                    };

                    _userToFind = users.First(x => x.Name == "test2-match");

                    users.ForEach(x => session.Save(x));
                    session.Flush();

                    _readOnlyRepository = new ReadOnlyRepository(session);
                };

        Because of =
            () => _result = _readOnlyRepository.FirstOrDefault<User>(x => x.Name == "test2-match");

        It should_return_matching_user =
            () => _result.ShouldBeLike(_userToFind);
    }
}