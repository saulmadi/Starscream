using DomainDrivenDatabaseDeployer;
using IvoryTower.Domain;
using IvoryTower.Domain.Entities;
using IvoryTower.Domain.Services;
using NHibernate;

namespace DatabaseDeployer
{
    public class UserSeeder : IDataSeeder
    {
        readonly ISession _session;

        public UserSeeder(ISession session)
        {
            _session = session;
        }

        #region IDataSeeder Members

        public void Seed()
        {
            var encryptor = new HashPasswordEncryptor();
            _session.Save(new User("Test User", "test@test.com", encryptor.Encrypt("password"), "615-555-1212"));
        }

        #endregion
    }
}