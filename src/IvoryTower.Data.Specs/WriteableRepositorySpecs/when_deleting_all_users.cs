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
    public class when_deleting_all_users
    {
        static IWriteableRepository _writeableRepository;
        static ISession _session;        

        Establish context =
            () =>
                {
                    _session = InMemorySession.New(new MappingScheme());
                    _writeableRepository = new WriteableRepository(_session);

                    var users = new List<User>
                                    {
                                        new User("test1", "test1@test.com", new EncryptedPassword("password")),
                                        new User("test2", "test2@test.com", new EncryptedPassword("password")),
                                        new User("test3", "test2@test.com", new EncryptedPassword("password"))
                                    };
                    users.ForEach(x => _session.Save(x));
                    _session.Flush();                    
                };

        Because of =
            () => _writeableRepository.DeleteAll<User>();

        It should_make_the_change_in_the_session =
            () =>
                {
                    _session.Flush();
                    _session.QueryOver<User>().List().ShouldBeEmpty();
                };
    }
}