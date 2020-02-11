using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Validator
{
    public class UserPasswordValidator : IUserValidator
    {
        #region fields
        private const int firstAsciiNumberOfNumbersCharacters = 48;
        private const int lastAsciiNumberOfNumbersCharacters = 57;
        private const int firstAsciiNumberOfUpperLetters = 65;
        private const int lastAsciiNumberOfUpperLetters = 90;
        private const int firstAsciiNumberOfLowerLetters = 97;
        private const int lastAsciiNumberOfLowerLetters = 122;
        #endregion

        public bool Validate(User value, List<string> errorMessages)
        {
            int errorContainerSizeWhenEnted = errorMessages.Count;

            if (string.IsNullOrEmpty(value.Password) || string.IsNullOrEmpty(value.ConfirmPassword))
            {
                errorMessages.Add("Password or confirm password field is empty");
            }

            if(!IsPasswordCorrect(value.Password, firstAsciiNumberOfNumbersCharacters, lastAsciiNumberOfNumbersCharacters) ||
               !IsPasswordCorrect(value.Password, firstAsciiNumberOfUpperLetters, lastAsciiNumberOfUpperLetters) ||
               !IsPasswordCorrect(value.Password, firstAsciiNumberOfLowerLetters, lastAsciiNumberOfLowerLetters))
            {
                errorMessages.Add("Password is not correct");
            }

            if(value.Password != value.ConfirmPassword)
            {
                errorMessages.Add("Password and confirm password fields is not equal");
            }

            return errorMessages.Count == errorContainerSizeWhenEnted;
        }

        private bool IsPasswordCorrect(string password, int firstAsciiNumber, int lastAsciiNumber)
        {
            foreach(var letter in password)
            {
                if((int)letter >= firstAsciiNumber && (int)letter <= lastAsciiNumber)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
