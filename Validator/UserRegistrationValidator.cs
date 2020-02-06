using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Registration.Interfaces;
using Registration.Models;

namespace Registration.Validator
{
    public class UserRegistrationValidator : IValidator<User>
    {
        public bool Validate(User value, out string errorMessage)
        {
            if(string.IsNullOrEmpty(value.Email))
            {
                errorMessage = "Email field is empty";

                return false;
            }

            if(string.IsNullOrEmpty(value.FullName))
            {
                errorMessage = "FullName field is empty";

                return false;
            }

            if(string.IsNullOrEmpty(value.Password))
            {
                errorMessage = "Password field is empty";

                return false;
            }

            if (string.IsNullOrEmpty(value.UserName))
            {
                errorMessage = "UserName field is empty";

                return false;
            }

            if(value.Password != value.ConfirmPassword)
            {
                errorMessage = "Password and confirm password fields is not equal";

                return false;
            }
            errorMessage = "";

            return true;
        }
    }
}
