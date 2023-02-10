using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Retomuub.Data.DTO;

namespace Retomuub.Bussiness.Jwt
{
    public interface IJwtUtils
    {
        string GenerateToken(UserDTO user);
        JwtSecurityToken ValidateToken(string token);
    }
}