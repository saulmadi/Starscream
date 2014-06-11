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
    public class when_getting_a_user_by_id
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

                    _userToFind = users.First(x => x.Name.Contains("match"));

                    users.ForEach(x => session.Save(x));

                    _readOnlyRepository = new ReadOnlyRepository(session);
                };

        Because of =
            () => _result = _readOnlyRepository.GetById<User>(_userToFind.Id);

        It should_return_matching_user =
            () => _result.ShouldBeLike(_userToFind);
    }
}