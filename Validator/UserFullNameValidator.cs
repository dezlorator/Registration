using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Validator
{
    public class UserFullNameValidator : IUserValidator
    {
        public bool Validate(User value, List<string> errorMessages)
        {
            if(string.IsNullOrEmpty(value.FullName))
            {
                errorMessages.Add("Full name is empty");

                return false;
            }

            return true;
        }
    }
}
