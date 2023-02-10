using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Retomuub.Bussiness.Interfaces;
using Retomuub.Bussiness.Jwt;
using Retomuub.Data.DTO;

namespace Retomuub.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserCollection _user;
        private readonly IJwtUtils _jwt;
        public UserController(IUserCollection user, IJwtUtils jwt)
        {
            _user = user;
            _jwt = jwt;
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody]UserDTO userDTO)
        {
            if( ModelState.IsValid ){
                await _user.InsertUser( userDTO );
            }
            return Ok(new { success = ModelState.IsValid });
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody]LoggedUserDTO userDTO)
        {
            if( ModelState.IsValid ){
                var user = await _user.LoginUser( userDTO );
                if( user != null ){
                    var token = _jwt.GenerateToken(user);
                    if(token != null){
                        HttpContext.Session.SetString("Token", token);
                        HttpContext.Response.Cookies.Append("X-Access-Token", token, new CookieOptions(){HttpOnly = true, Expires = DateTime.Now.AddDays(7),  });
                    }else{
                        return Ok(new { success  = false });
                    }
                }
            }
            return Ok(new { success = ModelState.IsValid });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}