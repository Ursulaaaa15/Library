using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BL.Auth.Entities
{
    public class TokensResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
