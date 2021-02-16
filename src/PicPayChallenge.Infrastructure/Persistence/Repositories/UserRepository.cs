using Dapper;
using Microsoft.Extensions.Options;
using PicPayChallenge.Core.Configs;
using PicPayChallenge.Core.DTOs;
using PicPayChallenge.Core.Entities;
using PicPayChallenge.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PicPayChallenge.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;
        public UserRepository(IOptions<ConnectionStringConfig> options)
        {
            connectionString = options.Value.ConnectionString;
        }

        public IEnumerable<User> GetUsersByTerm(UserRequestDTO request)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<User>(@"                       
                       SELECT Id, Name, UserName FROM (
                                    SELECT ROW_NUMBER() OVER(ORDER BY UserTypePriorityId) AS NUMBER,
                                           Id, Name, UserName FROM Users
                       					WHERE Name LIKE @Term OR UserName LIKE @Term
                                      ) AS TBL
                       WHERE NUMBER BETWEEN ((@PageNumber - 1) * @RowsOfPage + 1) AND (@PageNumber * @RowsOfPage)", 
                       new { Term = $"%{request.Term}%", PageNumber = request.PageNumber, RowsOfPage = request.RowsOfPage}, commandTimeout: 120);
            }
        }
    }
}
