using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Authentication
{
    public class JwtOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "da8hccz7nc8fafs76da<dak9zada8hccz7nc8fafs76da<dak9zada8hccz7nc8fafs76da<dak9za";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new(Encoding.UTF8.GetBytes(KEY));
    }
}
