using Microsoft.AspNetCore.Identity;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IWorkWithUserRepository
    {
        Task<IdentityResult> CreateUserAsync(UserIdentityChanged user, string password);
        Task<UserIdentityChanged> FindByIdAsync(string id);
        Task UpdateUserAsync(UserIdentityChanged user);
        Task<UserIdentityChanged> FindByNameAsync(string name);
        Task<SignInResult> PasswordSignInAsync(string UserName, string Password, bool isPersistant);
        Task SignInAsync(UserIdentityChanged user, bool isPersistant);
        Task SignOutAsync();
        bool IsUserAuthorized();
        IEnumerable<Claim> GetUserClaims();
        Task<string> GetUserTokenAsync(UserIdentityChanged user);
    }
}
