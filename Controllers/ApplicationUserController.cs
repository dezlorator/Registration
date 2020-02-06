using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Registration.Interfaces;
using Registration.Models;
using Registration.Services;

namespace Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        #region fields
        private UserManager<UserIdentityChanged> _userManager;
        private SignInManager<UserIdentityChanged> _singInManager;
        private IValidator<User> _userValidator;
        #endregion

        #region ctor
        public ApplicationUserController(UserManager<UserIdentityChanged> UserManager,
            SignInManager<UserIdentityChanged> SingInManager,
            IValidator<User> UserValidator)
        {
            _userManager = UserManager;
            _singInManager = SingInManager;
            _userValidator = UserValidator;
        }
        #endregion

        [EnableCors("CorsPolicyForRegistration")]
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostApplicationUser(User UserToRegister)
        {
            string errorMessage;

            if (!_userValidator.Validate(UserToRegister, out errorMessage))
            {
                //TODO: Logging

                return BadRequest(errorMessage);
            }

            var user = new UserIdentityChanged()
            {
                Email = UserToRegister.Email,
                FullName = UserToRegister.FullName,
                UserName = UserToRegister.UserName
            };

            try
            {
                var result = await _userManager.CreateAsync(user, UserToRegister.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Code == "DuplicateUserName")
                        {
                            //return Forbid();??
                            return Conflict();
                        }
                    }
                    //TODO: Logging

                    return BadRequest();
                }

                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Action(
                //    "ConfirmEmail",
                //    "Account",
                //    new { userId = user.Id, code = code },
                //    protocol: HttpContext.Request.Scheme);
                //EmailService emailService = new EmailService();
                //await emailService.SendEmailAsync(user.Email, "Confirm your account",
                //    $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                return Ok();
            }
            catch(Exception e)
            {
                //TODO: Logging

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}