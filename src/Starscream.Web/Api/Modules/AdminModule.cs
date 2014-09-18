using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Nancy;
using Nancy.ModelBinding;
using Starscream.Domain.Entities;
using Starscream.Domain.Services;
using Starscream.Web.Api.Requests.Admin;
using Starscream.Web.Api.Responses.Admin;

namespace Starscream.Web.Api.Modules
{
    public class AdminModule : NancyModule
    {
        public AdminModule(IPasswordEncryptor passwordEncryptor, IReadOnlyRepository readOnlyRepository,
                           IUserSessionFactory userSessionFactory, IMappingEngine mappingEngine)
        {
            Get["/users"] =
                _ =>
                    {
                        var request = this.Bind<AdminUsersRequest>();
                        var admin = new ProfileAdministrator();
                        var param = Expression.Parameter(typeof(User), "User");
                        var mySortExpression = Expression.Lambda<Func<User, object>>(Expression.Property(param, request.Field), param);
                        IQueryable<User> users =
                            readOnlyRepository.Query<User>(x => x.Profile != admin.Name).AsQueryable();

                        var orderedUsers = users.OrderBy(mySortExpression);

                        IQueryable<User> pagedUsers = orderedUsers.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize);

                        List<AdminUserResponse> mappedItems = mappingEngine
                            .Map<IQueryable<User>, IEnumerable<AdminUserResponse>>(pagedUsers).ToList();

                        return new AdminUsersListResponse(mappedItems);
                    };
        }
    }
}