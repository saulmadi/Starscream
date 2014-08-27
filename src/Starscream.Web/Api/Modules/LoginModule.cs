using System;
using Starscream.Data;
using Starscream.Domain;
using Starscream.Domain.Entities;
using Starscream.Domain.Services;
using Starscream.Domain.ValueObjects;
using Starscream.Web.Api.Infrastructure.Exceptions;
using Starscream.Web.Api.Requests;
using Starscream.Web.Api.Responses;
using Nancy;
using Nancy.ModelBinding;

namespace Starscream.Web.Api.Modules
{
    public class LoginModule : NancyModule
    {
        public LoginModule(IPasswordEncryptor passwordEncryptor, IReadOnlyRepository readOnlyRepository,
                           IUserSessionFactory userSessionFactory)
        {
            Post["/login"] =
                _ =>
                    {
                        var loginInfo = this.Bind<LoginRequest>();
                        if (loginInfo.Email == null) throw new UserInputPropertyMissingException("Email");
                        if (loginInfo.Password == null) throw new UserInputPropertyMissingException("Password");

                        EncryptedPassword encryptedPassword = passwordEncryptor.Encrypt(loginInfo.Password);

                        try
                        {
                            var user =
                                readOnlyRepository.First<User>(
                                    x => x.Email == loginInfo.Email && x.EncryptedPassword == encryptedPassword.Password);

                            UserLoginSession userLoginSession = userSessionFactory.Create(user);

                            return new SuccessfulLoginResponse<Guid>(userLoginSession.Id, user.Name, userLoginSession.Expires);
                        }
                        catch (ItemNotFoundException<User>)
                        {
                            throw new UnauthorizedAccessException();
                        }
                    };
        }
    }
}