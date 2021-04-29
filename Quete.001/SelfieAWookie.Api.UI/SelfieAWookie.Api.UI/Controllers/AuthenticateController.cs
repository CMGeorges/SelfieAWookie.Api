using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookie.Api.UI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfieAWookie.Api.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        #region Fields
        private UserManager<IdentityUser> _userManager = null;
        #endregion


        #region Ctor

        public AuthenticateController(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }
        #endregion

        #region Public methods

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]AuthentificateUserDto userDto)
        {
            IActionResult result = this.BadRequest();

            var user = await this._userManager.FindByEmailAsync(userDto.Login);
            if (user != null)
            {
                var verif = await this._userManager.CheckPasswordAsync(user, userDto.Password);
                if (verif)
                {
                    result = this.Ok(new AuthentificateUserDto()
                    {
                        Login = user.Email,
                        Name = user.UserName
                        //Token = //TODO Identificated.
                    });
                    result = this.Ok();
                }
            }


            return result;
        }
        #endregion
    }
}
