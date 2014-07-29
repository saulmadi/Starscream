using System.Collections.Generic;
using System.Linq;
using AcklenAvenue.Data.NHibernate.Testing;
using Starscream.Domain;
using Starscream.Domain.Entities;
using Starscream.Domain.Services;
using Starscream.Domain.ValueObjects;
using Machine.Specifications;
using NHibernate;

namespace Starscream.Data.Specs.WriteableRepositorySpecs
{
    public class when_updating_a_user
    {
        static IWriteableRepository _writeableRepository;
        static ISession _session;
        static User _userToUpdate;

        Establish context =
            () =>
                {
                    _session = InMemorySession.New(new MappingScheme());
                    _writeableRepository = new WriteableRepository(_session);

                    var users = new List<User>
                                    {
                                        new User("test1", "test1@test.com", new EncryptedPassword("password")),
                                        new User("test2-match", "test2@test.com", new EncryptedPassword("password")),
                                        new User("test3", "test2@test.com", new EncryptedPassword("password"))
                                    };
                    users.ForEach(x => _session.Save(x));
                    _session.Flush();

                    _userToUpdate = users.First(x => x.Name == "test2-match");
                    _userToUpdate.ChangeEmailAddress("another@test.com");
                };

        Because of =
            () => _writeableRepository.Update(_userToUpdate);

        It should_make_the_change_in_the_session =
            () => _session.Get<User>(_userToUpdate.Id).Email.ShouldEqual("another@test.com");
    }
}