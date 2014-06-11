using DomainDrivenDatabaseDeployer;
using IvoryTower.Domain;
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
            _session.Save(new User("Test User", "test@test.com", encryptor.Encrypt("password")));
        }

        #endregion
    }
}