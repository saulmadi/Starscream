using System;
using Starscream.Domain;
using StarScream.Domain.Email;

namespace Starscream.Web.emails
{
    public class PasswordResetEmailTemplate : IEmailBodyTemplate, IEmailSubjectTemplate
    {
        public Type ForType
        {
            get { return typeof (PasswordResetEmail); }
        }

        public string BodyTemplate
        {
            get { return "Please reset your password. Here's the token: @Model.Token."; }
        }

        public string SubjectTemplate
        {
            get { return "Password Reset"; }
        }
    }
       
}