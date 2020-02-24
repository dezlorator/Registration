using Microsoft.AspNetCore.Identity;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IWorkWithUserService
    {
        Task<List<string>> Register(User UserToRegister);
        Task<UserIdentityChanged> GetUserByName(string UserName);
        Task<bool> ConfirmEmail(string id);
        Task<SignInResult> PasswordSignInAsync(SingInModel singIn);
        Task SignOutAsync();
        bool IsUserAuthorized();
        IEnumerable<Claim> GetAuthorizedUserClaims();
        Task<string> GetUserTokenAsync(UserIdentityChanged user);
    }
}
