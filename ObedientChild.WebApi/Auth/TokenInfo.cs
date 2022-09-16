using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObedientChild.WebApi.Dto
{
    public class TokenInfo
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
    }
}
