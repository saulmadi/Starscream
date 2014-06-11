using System;
using System.Collections.Generic;
using IvoryTower.Domain;
using IvoryTower.Domain.Entities;
using IvoryTower.Domain.Services;
using Nancy.Security;

namespace IvoryTower.Web.Api.Infrastructure.Authentication
{
    public class IvoryTowerUserIdentity : IUserIdentity
    {
        public IUserSession UserSession { get; private set; }

        public IvoryTowerUserIdentity(IUserSession userSession)
        {
            UserSession = userSession;
        }

        public string UserName
        {
            get
            {
                if (UserSession is UserLoginSession)
                {
                    var executor = ((UserLoginSession)UserSession).User;
                    if (executor == null)
                    {
                        throw new Exception("The user should not be null on the user session.");
                    }
                    return executor.Email;
                }
                return null;
            }
        }

        public IEnumerable<string> Claims
        {
            get { return new string[] { }; }
        }
    }
}