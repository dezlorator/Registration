using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Services
{
    public class UserInitializer : IInitializer<User, UserIdentityChanged>
    {
        public UserIdentityChanged Initialize(User user)
        {
            return new UserIdentityChanged()
            {
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName
            };
        }
    }
}
