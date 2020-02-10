using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Validator
{
    public class UserNameValidators : IUserValidator
    {
        public bool Validate(User value, List<string> errorMessages)
        {
            if(string.IsNullOrEmpty(value.UserName))
            {
                errorMessages.Add("User name field is empty");

                return false;
            }

            return true;
        }
    }
}
