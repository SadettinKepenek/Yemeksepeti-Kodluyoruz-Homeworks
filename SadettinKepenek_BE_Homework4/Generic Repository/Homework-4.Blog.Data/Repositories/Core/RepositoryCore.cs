using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Homework_4.Blog.Data.Context;
using Homework_4.Blog.Domain.CustomExceptions;
using Homework_4.Blog.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Blog.Data.Repositories.Core
{
    public abstract class RepositoryCore<T> where T : class ,IEntity 
    {
        protected readonly DbContext _dbContext = null;
        protected readonly DbSet<T> _dbSet = null;

        protected RepositoryCore(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        protected virtual List<T> ExecSqlQuery<T>(string query, Func<DbDataReader, T> map)
        {
            using var command = _dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = System.Data.CommandType.Text;

            if (command.Connection.State != System.Data.ConnectionState.Open)
                command.Connection.Open();

            var entities = new List<T>();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                entities.Add(map(reader));
            }

            return entities;
        }

        public virtual async Task<int> SaveChanges()
        {
            var saveChangeResult = 0;
            try
            {
                saveChangeResult = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                HandleDbException(ex);
            }

            return saveChangeResult;
        }

        protected virtual void HandleDbException(Exception ex)
        {
            switch (ex)
            {
                case DbUpdateConcurrencyException concurrencyException:
                    throw new ConcurrencyException();
                case DbUpdateException updateEx:
                {
                    if (updateEx.InnerException?.InnerException == null)
                        throw new DatabaseAccessException(updateEx.Message, updateEx.InnerException);
                    if (!(updateEx.InnerException is SqlException sqlException))
                        throw new DatabaseAccessException(updateEx.Message, updateEx.InnerException);
                    switch (sqlException.Number)
                    {
                        case 2627:  // unique KeyException
                        case 547: // check constraints
                        case 2601: // duplicate
                            throw new ConcurrencyException();
                        default:
                            throw new DatabaseAccessException(updateEx.Message, updateEx.InnerException);
                    }
                }
            }
        }
    }
}