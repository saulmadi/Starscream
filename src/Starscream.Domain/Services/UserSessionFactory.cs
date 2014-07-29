using System;
using Starscream.Domain.Entities;

namespace Starscream.Domain.Services
{
    public class UserSessionFactory : IUserSessionFactory
    {
        readonly ITimeProvider _timeProvider;
        readonly ITokenExpirationProvider _tokenExpirationProvider;
        readonly ITokenGenerator<Guid> _tokenGenerator;
        readonly IWriteableRepository _writeableRepository;

        public UserSessionFactory(IWriteableRepository writeableRepository,
                                  ITimeProvider timeProvider,
                                  ITokenGenerator<Guid> tokenGenerator,
                                  ITokenExpirationProvider tokenExpirationProvider)
        {
            _writeableRepository = writeableRepository;
            _timeProvider = timeProvider;
            _tokenGenerator = tokenGenerator;
            _tokenExpirationProvider = tokenExpirationProvider;
        }

        #region IUserSessionFactory Members

        public UserLoginSession Create(User executor)
        {
            DateTime dateTime = _tokenExpirationProvider.GetExpiration(_timeProvider.Now());
            Guid token = _tokenGenerator.Generate();

            var userSession = new UserLoginSession(token, executor, dateTime);

            _writeableRepository.Create(userSession);

            return userSession;
        }

        #endregion
    }
}