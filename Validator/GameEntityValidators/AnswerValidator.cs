using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Validator.GameEntityValidators
{
    public class AnswerValidator : IGameEntityValidator
    {
        public bool Validate(Question objectToValidate)
        {
            if(objectToValidate.Answers.Count() <= 1)
            {
                return false;
            }

            if(objectToValidate.Answers.Select(p=> p.AnswerString).GroupBy(v => v)
                .Where(g => g.Count() > 1).Select(g => g.Key).Count()!=0)
            {
                return false;
            }

            return true;
        }
    }
}
