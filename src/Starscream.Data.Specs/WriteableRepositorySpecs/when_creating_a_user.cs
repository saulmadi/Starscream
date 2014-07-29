using System;
using AcklenAvenue.Data.NHibernate.Testing;
using Starscream.Domain;
using Starscream.Domain.Entities;
using Starscream.Domain.Services;
using Starscream.Domain.ValueObjects;
using Machine.Specifications;
using NHibernate;

namespace Starscream.Data.Specs.WriteableRepositorySpecs
{
    public class when_creating_a_user
    {
        static IWriteableRepository _writeableRepository;
        static User _result;
        static ISession _session;

        Establish context =
            () =>
                {
                    _session = InMemorySession.New(new MappingScheme());
                    _writeableRepository = new WriteableRepository(_session);
                };

        Because of =
            () =>
            _result =
            _writeableRepository.Create(new User("test", "test@test.com", new EncryptedPassword("password")));

        It should_be_retrievable =
            () => _session.Get<User>(_result.Id).Name.ShouldEqual("test");

        It should_return_the_created_user_with_an_id =
            () => _result.Id.ShouldNotEqual(Guid.Empty);
    }
}