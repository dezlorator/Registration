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
using Registration.Validator;

namespace Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        #region fields
        private UserManager<UserIdentityChanged> userManager;
        private SignInManager<UserIdentityChanged> singInManager;
        private readonly List<IUserValidator> userValidators;
        private readonly IInitializer<User> userInitializer;
        #endregion
        #region ctor
        public ApplicationUserController(UserManager<UserIdentityChanged> UserManager,
            SignInManager<UserIdentityChanged> SingInManager,
            IInitializer<User> UserInitializer)
        {
            userManager = UserManager;
            singInManager = SingInManager;;
            userInitializer = UserInitializer;
            userValidators = new List<IUserValidator>
            {
                new UserEmailValidator(), new UserFullNameValidator(), 
                new UserPasswordValidator(), new UserNameValidators()
            };
        }
        #endregion

        [EnableCors("CorsPolicyForRegistration")]
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(User UserToRegister)
        {
            List<string> errorMessages = new List<string>();

            foreach(var validator in userValidators)
            {
                validator.Validate(UserToRegister, errorMessages);
            }

            if(errorMessages.Count != 0)
            {
                //TODO logging

                return BadRequest(errorMessages);
            }

            var user = userInitializer.Initialize(UserToRegister);

            try
            {
                var result = await userManager.CreateAsync(user, UserToRegister.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Code == "DuplicateUserName")
                        {
                            return Conflict();
                        }
                    }
                    //TODO: Logging

                    return BadRequest();
                }

                await singInManager.SignInAsync(user, false);

                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(user.Email, "Confirm your account",
                    $"Подтвердите регистрацию, перейдя по ссылке: <a href='https://localhost:44316/api/applicationUser/ConfirmEmail/{user.Id}'>link</a>");

                return Ok();
            }
            catch(Exception e)
            {
                //TODO: Logging

                return BadRequest(e.Message);
            }
        }

        [EnableCors("CorsPolicyForRegistration")]
        [HttpGet("ConfirmEmail/{id}")]
        public async Task<IActionResult> ConfirmEmail(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);

            return Ok();
        }

        [EnableCors("CorsPolicyForRegistration")]
        [HttpPost("SingIn")]
        public async Task<IActionResult> CheckLoggingIn(SingInModel singIn)
        {
            //var user = await userManager.FindByNameAsync(singIn.UserName);

            //if(user == null)
            //{
            //    return NoContent();
            //}

            var result = await singInManager.PasswordSignInAsync(singIn.UserName, singIn.Password, false, false);

            if (!result.Succeeded)
            {
                return Forbid();
            }

            HttpContextAccessor accessor = new HttpContextAccessor();
            var a = accessor.HttpContext.User.Identity.Name;

            var userName = User.Identity.Name;

            return Ok();
        }

        [EnableCors("CorsPolicyForRegistration")]
        [HttpGet("SingOut")]
        public async Task<IActionResult> SingOut()
        {
            var a = User.Claims;

            var userName = User.Identity.Name;
            
            await singInManager.SignOutAsync();
            //валидация?
            return Ok();
        }
    }
}