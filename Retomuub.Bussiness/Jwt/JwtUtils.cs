using Retomuub.Data.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Retomuub.Common;

namespace Retomuub.Bussiness.Jwt
{
    public class JwtUtils: IJwtUtils
    {
        private readonly AppSettings _appSettings;

        public JwtUtils(IOptions<AppSettings>  appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(UserDTO userLogin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Key));
            var creadentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create claims
            var claims = new []{
                new Claim("id", userLogin.Id.ToString()),
                new Claim(ClaimTypes.Name, userLogin.Username),
                new Claim(ClaimTypes.Role, userLogin.Rol)
            };

            //Create token
            var token = new JwtSecurityToken(_appSettings.Issuer, _appSettings.Audience, claims, expires: DateTime.Now.AddDays(7),signingCredentials: creadentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtSecurityToken ValidateToken(string token) {
            if (token == null) 
                return null!;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            try{
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _appSettings.Audience,
                    ValidIssuer = _appSettings.Issuer,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                // return user id from JWT token if validation successful
                return jwtToken;
            } catch {
                // return null if validation fails
                return null!;
            }
        }
    }
}