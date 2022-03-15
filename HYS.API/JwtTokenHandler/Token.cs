using System;

namespace HYS.API.JwtTokenHandler
{
    public class Token
    {
        public string token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefleshToken { get; set; }
    }
}
