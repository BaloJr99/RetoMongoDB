using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Retomuub.Bussiness.Interfaces;
using Retomuub.Bussiness.Jwt;

namespace Retomuub.Client.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtUtils _jwtUtil, IUserCollection _user){

            var token = context.Session.GetString("Token");
            if(token == null)
                if(context.Request.Cookies.ContainsKey("X-Access-Token")){
                    token = context.Request.Cookies["X-Access-Token"];
                }

            JwtSecurityToken validatedToken = null!;
            if(token != null && token != ""){
                validatedToken = _jwtUtil.ValidateToken(token);
            }

            if(validatedToken == null){
                context.Session.Clear();
            }else{
                context.Request.Headers.Add("Authorization", "Bearer " + token);
                context.Items["UserData"] = await _user.GetUsuario(validatedToken.Payload["id"].ToString());
            }
            await _next(context);
        }
    }
}