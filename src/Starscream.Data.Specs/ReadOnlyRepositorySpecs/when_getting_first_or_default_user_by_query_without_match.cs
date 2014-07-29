using System.Collections.Generic;
using AcklenAvenue.Data.NHibernate.Testing;
using Starscream.Domain;
using Starscream.Domain.Entities;
using Starscream.Domain.ValueObjects;
using Machine.Specifications;
using NHibernate;

namespace Starscream.Data.Specs.ReadOnlyRepositorySpecs
{
    public class when_getting_first_or_default_user_by_query_without_match
    {
        static ReadOnlyRepository _readOnlyRepository;
        static User _result;
        
        Establish context =
            () =>
                {
                    ISession session = InMemorySession.New(new MappingScheme());

                    var users = new List<User>
                                    {
                                        new User("test1", "test1@test.com", new EncryptedPassword("password")),
                                        new User("test2", "test2@test.com", new EncryptedPassword("password")),
                                        new User("test3", "test2@test.com", new EncryptedPassword("password"))
                                    };

                    users.ForEach(x => session.Save(x));
                    session.Flush();

                    _readOnlyRepository = new ReadOnlyRepository(session);
                };

        Because of =
            () => _result = _readOnlyRepository.FirstOrDefault<User>(x => x.Name == "test2-match");

        It should_return_matching_user =
            () => ShouldExtensionMethods.ShouldBeNull(_result);
    }
}