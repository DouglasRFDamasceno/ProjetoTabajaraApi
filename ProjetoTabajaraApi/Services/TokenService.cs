using Microsoft.IdentityModel.Tokens;
using ProjetoTabajaraApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoTabajaraApi.Services
{
    public class TokenService
    {
        public IConfiguration _configuration { get; private set; }
        // string symmetricSecurityKey;

        public TokenService(IConfiguration configuration)
        {
            // DotNetEnv.Env.Load();
            // symmetricSecurityKey = Environment.GetEnvironmentVariable("SYMMETRIC_SECURITY_KEY");
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("userName", user.UserName),
                new Claim("id", user.Id),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var symmetricSecurityKey = _configuration.GetConnectionString("SYMMETRIC_SECURITY_KEY");
            var key = new SymmetricSecurityKey
            (
                Encoding.UTF8.GetBytes(symmetricSecurityKey)
            );

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                expires: DateTime.UtcNow.AddMinutes(60),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}