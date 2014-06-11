using System;
using AcklenAvenue.Data.NHibernate.Testing;
using IvoryTower.Domain;
using IvoryTower.Domain.Entities;
using Machine.Specifications;
using NHibernate;

namespace IvoryTower.Data.Specs.ReadOnlyRepositorySpecs
{
    public class when_getting_a_user_by_id_with_no_match
    {
        static ReadOnlyRepository _readOnlyRepository;
        static Exception _exception;

        Establish context =
            () =>
                {
                    ISession session = InMemorySession.New(new MappingScheme());
                    _readOnlyRepository = new ReadOnlyRepository(session);
                };

        Because of =
            () => _exception = Catch.Exception(() => _readOnlyRepository.GetById<User>(Guid.NewGuid()));

        It should_throw_the_expected_exception =
            () => _exception.ShouldBeOfType<ItemNotFoundException<User>>();
    }
}