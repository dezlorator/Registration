using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Validator
{
    public class UserEmailValidator : IUserValidator
    {
        public bool Validate(User value, List<string> errorMessages)
        {
            int errorContainersSizeWhenEnted = errorMessages.Count;

            if (string.IsNullOrEmpty(value.Email))
            {
                errorMessages.Add("Email field is empty");
            }
            if(!IsEmailCorrect(value.Email))
            {
                errorMessages.Add("Email is not correct");
            }

            return errorMessages.Count == errorContainersSizeWhenEnted;
        }

        private bool IsEmailCorrect(string email)
        {
            return (email.Contains('@')) && (email[email.Length - 1] != '@') && (email[0] != '@');
        }
    }
}
