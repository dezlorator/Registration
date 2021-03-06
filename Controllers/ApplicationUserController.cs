﻿using System;
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
        private readonly IValidator<User> userValidator;
        private readonly List<IUserValidator> userValidators;
        private readonly IUserInitializer userInitializer;
        #endregion

        #region ctor
        public ApplicationUserController(UserManager<UserIdentityChanged> UserManager,
            SignInManager<UserIdentityChanged> SingInManager,
            IValidator<User> UserValidator, IUserInitializer UserInitializer)
        {
            userManager = UserManager;
            singInManager = SingInManager;
            userValidator = UserValidator;
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

        //перенести логику в сервис
        public async Task<IActionResult> PostApplicationUser(User UserToRegister)
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
                
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(user.Email, "Confirm your account",
                    $"Подтвердите регистрацию, перейдя по ссылке: <a href='https://localhost:44316/api/applicationUser?id={user.Id}&code={code}'>link</a>");

                return Ok();
            }
            catch(Exception e)
            {
                //TODO: Logging

                return BadRequest(e.Message);
            }
        }

        [HttpGet("/{id}/{code}")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string id, [FromQuery]string code)
        {
            if (id == null || code == null)
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

        //public async Task<IActionResult> LogIn(string login, string password)
        //{

        //}
    }
}