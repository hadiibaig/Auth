using Auth.Entities;
using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public interface IService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        void RevokeToken(string token, string ipAddress);
        IEnumerable<User> GetAll(int? userid);
        User GetById(int id);
        AuthenticateResponse Login(AuthenticateRequest op, string ipAddress);

        User GetUserByName(string username);
    }
}
