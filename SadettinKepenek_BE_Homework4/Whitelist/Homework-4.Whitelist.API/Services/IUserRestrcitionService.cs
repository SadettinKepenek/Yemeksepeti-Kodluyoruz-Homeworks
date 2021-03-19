using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Homework_4.Whitelist.API.Entities;

namespace Homework_4.Whitelist.API.Services
{
    public interface IUserRestrictionService
    {
        void AddUserIp(UserIp  userIp);
        void AddUserRestriction(UserRestriction userRestriction);

        UserIp GetUserIp(Expression<Func<UserIp,bool>> predicate);
        List<UserRestriction> GetUserRestrictions(Expression<Func<UserRestriction,bool>> predicate);
        
    }
}