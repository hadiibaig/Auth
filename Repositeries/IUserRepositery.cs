using Auth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Repositeries
{
    public interface IUserRepositery
    {
        int AddUser(User user);
        IEnumerable<User> GetUsers(int? id);

        int AddrefreshToken(RefreshToken token);
        RefreshToken getUserByRefreshToken(string token);

        RefreshToken refreshTokenvalidate(string token);

        User UserByRefreshTokenID(int id);
        int RemoveOldRefreshTokens(RefreshToken token);
        RefreshToken getRefreshTokenByID(int id);

        int UpdateRefreshToken(RefreshToken token);

        User GetbyName(string username);
    }
}
