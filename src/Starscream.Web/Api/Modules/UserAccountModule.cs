using Starscream.Domain.Application.Commands;
using Starscream.Domain.Services;
using Nancy;
using Nancy.ModelBinding;
using Starscream.Web.Api.Infrastructure;
using Starscream.Web.Api.Requests;

namespace Starscream.Web.Api.Modules
{

    public class UserAccountModule : NancyModule
    {        
        public UserAccountModule(ICommandDispatcher commandDispatcher, IPasswordEncryptor passwordEncryptor)
        {
            Post["/register"] =
                _ =>
                    {
                        var req = this.Bind<NewUserRequest>();
                        commandDispatcher.Dispatch(this.UserSession(),
                                                   new CreateUser(req.Email, passwordEncryptor.Encrypt(req.Password), req.Name, req.PhoneNumber));
                        return null;
                    };

            Post["/password/requestReset"] =
                _ =>
                {
                    var req = this.Bind<ResetPasswordRequest>();
                    commandDispatcher.Dispatch(this.UserSession(),
                                               new CreatePasswordResetToken(req.Email) );
                    return null;
                };

            Put["/password/reset/{token}"] =
                _ =>
                {
                    return null;
                };
        }
    }
}