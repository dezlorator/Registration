using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Registration.Repository
{
    public class WorkWithUserRepository : IWorkWithUserRepository
    {
        #region field
        private readonly UserManager<UserIdentityChanged> userManager;
        private readonly SignInManager<UserIdentityChanged> singInManager;
        private readonly HttpContextAccessor httpContextAccessor;
        #endregion

        public WorkWithUserRepository(UserManager<UserIdentityChanged> userManager, SignInManager<UserIdentityChanged> singInManager,
            HttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.singInManager = singInManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityResult> CreateUserAsync(UserIdentityChanged userIdentityChanged, string password)
        {
            return await userManager.CreateAsync(userIdentityChanged, password);
        }

        public async Task<UserIdentityChanged> FindByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task UpdateUserAsync(UserIdentityChanged user)
        {
            await userManager.UpdateAsync(user);
        }

        public async Task<UserIdentityChanged> FindByNameAsync(string name)
        {
            return await userManager.FindByNameAsync(name);
        }

        public async Task<SignInResult> PasswordSignInAsync(string UserName, string Password, bool isPersistant)
        {
           return await singInManager.PasswordSignInAsync(UserName, Password, isPersistant, false);
        }

        public async Task SignInAsync(UserIdentityChanged user, bool isPersistant)
        {
            await singInManager.SignInAsync(user, isPersistant);
        }

        public async Task<string> GetUserTokenAsync(UserIdentityChanged user)
        {
            return await userManager.GetAuthenticationTokenAsync(user, "Windows", "Auth-Token");
        }

        public async Task SignOutAsync()
        {
            await singInManager.SignOutAsync();
        }

        public bool IsUserAuthorized()
        {
            return httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetUserClaims()
        {
            return httpContextAccessor.HttpContext.User.Claims;
        }
    }
}
