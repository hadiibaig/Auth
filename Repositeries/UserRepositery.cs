using Auth.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Repositeries
{
    public class UserRepositery : IUserRepositery
    {
              private IConfiguration _config;
        public UserRepositery(IConfiguration config)
        {
            _config = config;
        }

        public int AddrefreshToken(RefreshToken token)
        {
            var sp = "usp_AddrefreshToken";
            var param = new DynamicParameters();
            param.Add("token", token.Token);
            param.Add("isActive", token.IsActive, DbType.Boolean);
            param.Add("created", token.Created, DbType.DateTime);
            param.Add("createdbyIP", token.CreatedByIp, DbType.String);
            param.Add("isExpired", token.IsExpired, DbType.Boolean);
            param.Add("isRevoked", token.IsRevoked, DbType.Boolean);
            param.Add("ResonRevoked", token.ReasonRevoked, DbType.String);
            param.Add("ReplacedByToken", token.ReplacedByToken, DbType.String);
            param.Add("Revoked", token.Revoked, DbType.String);
            param.Add("RevokedByIP", token.RevokedByIp, DbType.String);
            param.Add("Expires", token.Expires, DbType.DateTime);
            param.Add("userID", token.userID, DbType.Int32);
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                var result = connection.QueryFirst<int>(sp, param,
                    commandType: CommandType.StoredProcedure);
                return result;


            }
        }

        public int AddUser(User user)
        {
    
            var storeProc = "usp_addDapperuser";
            var param = new DynamicParameters();
            param.Add("firstname", user.FirstName);
            param.Add("lastname", user.LastName);
            param.Add("username", user.Username);
            param.Add("passwordhash", user.PasswordHash);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                var result = connection.QueryFirst<int>(storeProc, param,
                    commandType: CommandType.StoredProcedure);

                return result;


            }
        }

        public IEnumerable<User> GetUsers(int? id)
        {
            var sp = "usp_getAllUsers";
            var param = new DynamicParameters();
            param.Add("id", id, DbType.Int32);
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                var result = connection.Query<User>(sp, param,
                    commandType: CommandType.StoredProcedure);
                return result;


            }
        
        }
    
        public RefreshToken getUserByRefreshToken(string token)
        {

            var storeProc = "usp_getUserByRefreshToken";
            var param = new DynamicParameters();
            param.Add("token", token);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                var result = connection.QueryFirst<RefreshToken>(storeProc, param,
                    commandType: CommandType.StoredProcedure);

                return result;


            }
        }
       public RefreshToken refreshTokenvalidate(string token)
        {

            var storeProc = "usp_refreshTokenvalidate";
            var param = new DynamicParameters();
            param.Add("token", token);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                var result = connection.QueryFirst<RefreshToken>(storeProc, param,
                    commandType: CommandType.StoredProcedure);

                return result;


            }
        }
       public User UserByRefreshTokenID(int id)
        {

            var storeProc = "usp_UserByRefreshTokens";
            var param = new DynamicParameters();
            param.Add("id", id , DbType.Int32);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                var result = connection.QueryFirst<User>(storeProc, param,
                    commandType: CommandType.StoredProcedure);

                return result;


            }
        }
       public int RemoveOldRefreshTokens(RefreshToken token)
        {
           
            var sp = "usp_DeleteRefreshTokens";
            var param = new DynamicParameters();
            param.Add("id", token.Id, DbType.Int32);
            param.Add("isActive", token.IsActive, DbType.Boolean);
            param.Add("Created", token.Created, DbType.DateTime);
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                var result = connection.Execute(sp, param,
                    commandType: CommandType.StoredProcedure);
                return result;


            }
        }
       public RefreshToken getRefreshTokenByID(int id)
        {

            var storeProc = "usp_usp_getRefreshTokenByID";
            var param = new DynamicParameters();
            param.Add("id", id, DbType.Int32);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                var result = connection.QueryFirstOrDefault<RefreshToken>(storeProc, param,
                    commandType: CommandType.StoredProcedure);

                return result;


            }
        }
        public int UpdateRefreshToken(RefreshToken token)
        {

            var storeProc = "usp_UpdateRefreshToken";
            var param = new DynamicParameters();
            param.Add("id", token.Id, DbType.Int32);
            param.Add("revokedbyip", token.RevokedByIp);
            param.Add("isrevoked", token.IsRevoked, DbType.Boolean);
            param.Add("replacedbytoken", token.ReplacedByToken);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                var result = connection.Execute(storeProc, param,
                    commandType: CommandType.StoredProcedure);

                return result;


            }
        }
        public User GetbyName(string username)
        {

            var sp = "usp_GetUserByName";
            var param = new DynamicParameters();
            param.Add("username", username);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {


                var result = connection.Query<User>(sp, param,
                    commandType: CommandType.StoredProcedure).First();
                return result;


            }
        }
    }
}
