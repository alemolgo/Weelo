using co_weelo_testproject_common.Request;
using co_weelo_testproject_common.Response;
using co_weelo_testproject_dal.ModelData;
using co_weelo_testproject_service.Configuration;
using co_weelo_testproject_service.Interfaces;
using co_weelo_testproject_service.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace co_weelo_testproject_service.Implements
{
    public class AuthService : IAuthService
    {
        protected WEELOContext _context;
        private readonly JwtConfig _jwtConfig;

        public AuthService(WEELOContext context, IOptions<JwtConfig> jwtConfig)
        {
            _context = context;
            _jwtConfig = jwtConfig.Value;
        }

        public AuthResponse Authenticate(AuthRequest authRequest)
        {
            AuthResponse authResponse = new();
            string password = Encrypt.GetSHA256(authRequest.Password);
            var user = _context.Users.Where(x => x.Email == authRequest.Email && x.Password == password).FirstOrDefault();

            if (user == null) return null;

            authResponse.Email = user.Email;
            authResponse.Token = GenerateJwtToken(user);
            return authResponse;
        }

        public string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                        }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}


