using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Retomuub.Bussiness.Interfaces;
using Retomuub.Data.DTO;

namespace Retomuub.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserCollection _user;
        public UserController(IUserCollection user)
        {
            _user = user;
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
            var result = ModelState.IsValid;
            if( result ){
                result = await _user.LoginUser( userDTO );
            }
            return Ok(new { success = result });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}