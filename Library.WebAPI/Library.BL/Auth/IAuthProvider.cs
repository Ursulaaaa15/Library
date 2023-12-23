using Library.BL.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BL.Auth
{
    public interface IAuthProvider
    {
        Task<TokensResponse> AuthorizeUser(string login, string password);
        Task RegisterUser(RegisterUserModel model);
    }
}
