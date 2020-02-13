using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Validator.GameEntityValidators
{
    public class QuestionStringValidator : IQuestionValidator
    {
        #region fields
        private const string stringIsEmpty = "Question string is empty";
        private const string noCorrectAnswer = "No correct answer";
        #endregion
        public bool Validate(Question objectToValidate, out string errors)
        {
            if(string.IsNullOrEmpty(objectToValidate.QuestionString))
            {
                errors = stringIsEmpty;

                return false;
            }

            if(!objectToValidate.Answers.Select(p=> p.AnswerString)
                .Contains(objectToValidate.QuestionString))
            {
                errors = noCorrectAnswer;

                return false;
            }
            errors = "";

            return true;
        }
    }
}
