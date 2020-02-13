using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Validator.GameEntityValidators
{
    public class AnswerValidator : IQuestionValidator
    {
        #region fields
        private const string notEnoughtAnswers = "Not enough answers";
        private const string answerReapeats = "Answers can`t repeat";
        #endregion
        public bool Validate(Question objectToValidate, out string error)
        {
            if (objectToValidate.Answers.Count() <= 1)
            {
                error = notEnoughtAnswers;

                return false;
            }

            if(objectToValidate.Answers.Select(p=> p.AnswerString).GroupBy(v => v)
                .Where(g => g.Count() > 1).Select(g => g.Key).Count()!=0)
            {
                error = answerReapeats;

                return false;
            }

            error = "";

            return true;
        }
    }
}
