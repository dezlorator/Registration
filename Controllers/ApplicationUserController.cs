using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Registration.Interfaces;
using Registration.Models;
using Registration.Services;
using Registration.Validator;

namespace Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        #region fields
        private readonly IWorkWithUserService workWithUserService;
        private readonly IEmailService emailService;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
        #endregion
        #region ctor
        public ApplicationUserController(IWorkWithUserService workWithUserService, IEmailService emailService,
            JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            this.workWithUserService = workWithUserService;
            this.emailService = emailService;
            this.jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }
        #endregion

        [EnableCors("CorsPolicyForRegistration")]
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(User UserToRegister)
        {
            List<string> errors = await workWithUserService.Register(UserToRegister);

            if(errors != null)
            {
                return BadRequest(errors);
            }

            var userId = (await workWithUserService.GetUserByName(UserToRegister.UserName)).Id;

            await emailService.SendEmailAsync(UserToRegister.Email, "Confirm your account",
            $"Подтвердите регистрацию, перейдя по ссылке: <a href='https://localhost:44316/api/applicationUser/ConfirmEmail/{userId}'>link</a>");

            return Ok();
        }

        [EnableCors("CorsPolicyForRegistration")]
        [HttpGet("ConfirmEmail/{id}")]
        public async Task<IActionResult> ConfirmEmail(string id)
        {
            var isSuccess = await workWithUserService.ConfirmEmail(id);

            if(!isSuccess)
            {
                return NotFound();
            }

            return Ok();
        }

        [EnableCors("CorsPolicyForRegistration")]
        [HttpPost("SingIn")]
        public async Task<IActionResult> SignIn(SingInModel singIn)
        {
            var user = await workWithUserService.GetUserByName(singIn.UserName);

            if(user == null)
            {
                return NotFound();
            }

            var authResult = await workWithUserService.PasswordSignInAsync(singIn);

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            var now = DateTime.Now.AddHours(2); 
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: new List<Claim> { new Claim("UserId", user.Id), new Claim("UserName", user.UserName) },
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = jwtSecurityTokenHandler.WriteToken(jwt);

            return Ok(new { encodedJwt } );
        }

        [EnableCors("CorsPolicyForRegistration")]
        [HttpGet("SingOut")]
        public async Task<IActionResult> SingOut()
        {
            await workWithUserService.SignOutAsync();

            return Ok();
        }
    }
}