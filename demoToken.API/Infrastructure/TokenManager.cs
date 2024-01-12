using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace demoToken.API.Infrastructure
{
    public class TokenManager
    {
        public readonly IConfiguration _configuration;
        public readonly string secret;
        public readonly string issuer;

        public readonly string audience;

        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
            secret = _configuration["jwt:key"];
            issuer = _configuration["jwt:issuer"];
            audience = _configuration["jwt:audience"];
        }

        public string GenerateJwt(dynamic user, int expirationDate = 1)
        {

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            DateTime now = DateTime.Now;
            Claim[] myClaims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, user.id.ToString()),
                new Claim(ClaimTypes.GivenName, $"{user.Nom} {user.Prenom}"),
                new Claim(ClaimTypes.Expiration, now.Add(TimeSpan.FromDays(expirationDate)).ToString(), ClaimValueTypes.DateTime)

            };

            JwtSecurityToken token = new JwtSecurityToken(
                    claims: myClaims,
                    expires: now.Add(TimeSpan.FromDays(expirationDate)),
                    signingCredentials: credentials,
                    issuer: issuer,
                    audience: audience
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }

    }
}
