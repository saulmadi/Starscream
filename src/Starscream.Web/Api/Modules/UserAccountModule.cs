using Starscream.Domain.Commands;
using Starscream.Domain.Services;
using Nancy;
using Nancy.ModelBinding;
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

            Post["/reset-password"] =
                _ =>
                {
                    var req = this.Bind<ResetPasswordRequest>();
                    commandDispatcher.Dispatch(this.UserSession(),
                                               new CreatePasswordResetToken(req.Email) );
                    return null;
                };
        }
    }
}