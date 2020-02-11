using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Validator.GameEntityValidators
{
    public class QuestionStringValidator : IGameEntityValidator
    {
        public bool Validate(Question objectToValidate)
        {
            if(string.IsNullOrEmpty(objectToValidate.QuestionString))
            {
                return false;
            }

            if(!objectToValidate.Answers.Select(p=> p.AnswerString)
                .Contains(objectToValidate.QuestionString))
            {
                return false;
            }

            return true;
        }
    }
}
