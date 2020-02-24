using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Registration.Interfaces;
using Registration.Models;
using Registration.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Registration.Services
{
    public class WorkWithUserService : IWorkWithUserService
    {
        #region fields
        private readonly IWorkWithUserRepository workWithUserRepository;
        private readonly List<IUserValidator> userValidators;
        private readonly IInitializer<User, UserIdentityChanged> userInitializer;
        #endregion

        #region ctor
        public WorkWithUserService(IWorkWithUserRepository workWithUserRepository, 
            IInitializer<User, UserIdentityChanged> userInitializer, List<IUserValidator> userValidators)
        {
            this.workWithUserRepository = workWithUserRepository;
            this.userInitializer = userInitializer;
            this.userValidators = userValidators;
        }
        #endregion

        public async Task<UserIdentityChanged> GetUserByName(string UserName)
        {
            return await workWithUserRepository.FindByNameAsync(UserName);
        }

        public async Task<bool> ConfirmEmail(string id)
        {
            if (id == null)
            {
                return false;
            }

            var user = await workWithUserRepository.FindByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            user.EmailConfirmed = true;

            await workWithUserRepository.UpdateUserAsync(user);

            return true;
        }

        public async Task<List<string>> Register(User UserToRegister)
        {
            List<string> errorMessages = new List<string>();

            foreach (var validator in userValidators)
            {
                validator.Validate(UserToRegister, errorMessages);
            }

            if (errorMessages.Count != 0)
            {
                return errorMessages;
            }

            var user = userInitializer.Initialize(UserToRegister);

            var result = await workWithUserRepository.CreateUserAsync(user, UserToRegister.Password);

            if (!result.Succeeded)
            {
                errorMessages.AddRange(result.Errors.Select(p => p.Description));

                return errorMessages;
            }

            return null;
        }

        public async Task<SignInResult> PasswordSignInAsync(SingInModel singIn)
        {
            return await workWithUserRepository.PasswordSignInAsync(singIn.UserName, singIn.Password, false);
        }

        public async Task<string> GetUserTokenAsync(UserIdentityChanged user)
        {
            return await workWithUserRepository.GetUserTokenAsync(user);
        }

        public async Task SignOutAsync()
        {
            await workWithUserRepository.SignOutAsync();
        }

        public bool IsUserAuthorized()
        {
            return workWithUserRepository.IsUserAuthorized();
        }

        public IEnumerable<Claim> GetAuthorizedUserClaims()
        {
            return workWithUserRepository.GetUserClaims();
        }
    }
}
