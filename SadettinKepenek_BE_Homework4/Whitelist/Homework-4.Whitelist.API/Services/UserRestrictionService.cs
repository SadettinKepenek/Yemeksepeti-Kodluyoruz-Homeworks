using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Homework_4.Whitelist.API.Data;
using Homework_4.Whitelist.API.Entities;

namespace Homework_4.Whitelist.API.Services
{
    public class UserRestrictionService:IUserRestrictionService
    {
        private readonly DataContext _dbContext;

        public UserRestrictionService()
        {
            _dbContext = new DataContext();
        }

        public void AddUserIp(UserIp userIp)
        {
            _dbContext.UserIps.Add(userIp);
            _dbContext.SaveChanges();
        }

        public void AddUserRestriction(UserRestriction userRestriction)
        {
            _dbContext.UserRestrictions.Add(userRestriction);
            _dbContext.SaveChanges();
        }

        public UserIp GetUserIp(Expression<Func<UserIp, bool>> predicate)
        {
            return _dbContext.UserIps.FirstOrDefault(predicate);
        }

        public List<UserRestriction> GetUserRestrictions(Expression<Func<UserRestriction, bool>> predicate)
        {
            return _dbContext.UserRestrictions.Where(predicate).ToList();
        }
    }
}