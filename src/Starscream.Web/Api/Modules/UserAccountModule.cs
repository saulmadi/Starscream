using System;
using Starscream.Domain.Application.Commands;
using Starscream.Domain.Services;
using Nancy;
using Nancy.ModelBinding;
using Starscream.Notifications;
using Starscream.Web.Api.Infrastructure;
using Starscream.Web.Api.Requests;
using Starscream.Web.Api.Requests.Facebook;
using Starscream.Web.Api.Requests.Google;

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
                                                   new CreateEmailLoginUser(req.Email, passwordEncryptor.Encrypt(req.Password), req.Name, req.PhoneNumber));
                        return null;
                    };


            Post["/register/facebook"] =
                _ =>
                    {
                        var req = this.Bind<FacebookRegisterRequest>();
                        commandDispatcher.Dispatch(this.UserSession(), new CreateFacebookLoginUser(req.id,req.email, req.first_name, req.last_name,req.link,req.name,req.url_image));
                        return null;
                    };

            Post["/register/google"] =
                _ =>
                    {
                        var req = this.Bind<GoogleRegisterRequest>();
                        commandDispatcher.Dispatch(this.UserSession(), new CreateGoogleLoginUser(req.id,req.email,req.name.givenName,req.name.familyName,req.url,req.displayName,req.image.url));
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
                p =>
                {
                    var newPasswordRequest = this.Bind<NewPasswordRequest>();
                    var token = Guid.Parse((string)p.token);
                    commandDispatcher.Dispatch(this.UserSession(),
                                               new ResetPassword(token, passwordEncryptor.Encrypt(newPasswordRequest.Password)));
                    return null;
                };
        }
    }
}