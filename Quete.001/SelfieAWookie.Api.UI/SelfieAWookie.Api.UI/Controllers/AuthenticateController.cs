﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookie.Api.UI.Application.DTOs;
using SelfieAWookie.Api.UI.ExtensionMethods;
using SelfieAWookie.Core.Selfies.Infrastructures.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookie.Api.UI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]  
    public class AuthenticateController : ControllerBase
    {

        #region Fields
        private readonly UserManager<IdentityUser> _userManager = null;
        private readonly IConfiguration _configuration = null;
        private readonly IOptions<SecurityOption> options;
        private readonly SecurityOption _option = null;
        private readonly ILogger<AuthenticateController> _logger;

        #endregion


        #region Ctor

        public AuthenticateController(ILogger<AuthenticateController> logger,UserManager<IdentityUser> userManager, IConfiguration configuration,IOptions<SecurityOption> options)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._configuration = configuration;
            this._option = options.Value;
        }
        #endregion

        #region Public methods
        [Route("register")]
        [HttpPost]      
        public async Task<IActionResult> Register([FromBody] AuthentificateUserDto userDto)
        {
            IActionResult result = this.BadRequest();

            try
            {
                var user = new IdentityUser(userDto.Login);
                user.Email = userDto.Login;
                user.UserName = userDto.Name;


                var success = await this._userManager.CreateAsync(user, userDto.Password);


                if (success.Succeeded)
                {
                    userDto.Token = this.GenerateJwtToken(user);
                    result = this.Ok(userDto);
                }

            }
            catch (Exception ex)
            {
                this._logger.LogError("Register", ex, userDto);
                this.Problem("Cannot Register");
            }

            return result;

        }



        [HttpPost]        
        public async Task<IActionResult> Login([FromBody]AuthentificateUserDto userDto)
        {
            IActionResult result = this.BadRequest();
            try
            {
                var user = await this._userManager.FindByEmailAsync(userDto.Login);
                if (user != null)
                {
                    var verif = await this._userManager.CheckPasswordAsync(user, userDto.Password);
                    if (verif)
                    {
                        result = this.Ok(new AuthentificateUserDto()
                        {
                            Login = user.Email,
                            Name = user.UserName,
                            Token = this.GenerateJwtToken(user)
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Login", ex, userDto);
                result = this.Problem("Cannot log");

            }


            return result;

        }
        #endregion

        #region Internal methods

        private string GenerateJwtToken(IdentityUser user)
    {
        // Now its ime to define the jwt token which will be responsible of creating our tokens
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        // We get our secret from the appsettings
        var key = Encoding.UTF8.GetBytes(this._option.Key);

        // we define our token descriptor
            // We need to utilise claims which are properties in our token which gives information about the token
            // which belong to the specific user who it belongs to
            // so it could contain their id, name, email the good part is that these information
            // are generated by our server and identity framework which is valid and trusted
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                // the JTI is used for our refresh token which we will be convering in the next video
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            // the life span of the token needs to be shorter and utilise refresh token to keep the user signedin
            // but since this is a demo app we can extend it to fit our current need
            Expires = DateTime.UtcNow.AddHours(6),
            // here we are adding the encryption alogorithim information which will be used to decrypt our token
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }
	    #endregion
    }
}
