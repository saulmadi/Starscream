using System;
using System.Collections.Generic;
using System.Linq;
using AcklenAvenue.Data.NHibernate.Testing;
using IvoryTower.Domain;
using IvoryTower.Domain.Entities;
using IvoryTower.Domain.Services;
using IvoryTower.Domain.ValueObjects;
using Machine.Specifications;
using NHibernate;

namespace IvoryTower.Data.Specs.WriteableRepositorySpecs
{
    public class when_creating_a_multiple_users
    {
        static IWriteableRepository _writeableRepository;
        static IEnumerable<User> _result;
        static ISession _session;
        static List<User> _users;

        Establish context =
            () =>
                {
                    _session = InMemorySession.New(new MappingScheme());
                    _writeableRepository = new WriteableRepository(_session);

                    _users = new List<User>
                                 {
                                     new User("test1", "test1@test.com", new EncryptedPassword("password")),
                                     new User("test2-match", "test2@test.com", new EncryptedPassword("password")),
                                     new User("test3", "test2@test.com", new EncryptedPassword("password"))
                                 };
                };

        Because of =
            () =>
            _result =
            _writeableRepository.CreateAll(_users);

        It should_all_be_retrievable =
            () => _result.ToList().ForEach(x => _session.Get<User>(x.Id).Name.ShouldEqual(x.Name));

        It should_all_return_the_created_user_with_an_id =
            () => _result.ToList().ForEach(x => x.Id.ShouldNotEqual(Guid.Empty));
    }
}