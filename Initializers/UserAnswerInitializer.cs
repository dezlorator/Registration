using Registration.Interfaces;
using Registration.Models;
using Registration.Models.ReceivedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Initializers
{
    public class UserAnswerInitializer : IInitializer<ReceivedUserAnswer, UserAnswer>
    {
        public UserAnswer Initialize(ReceivedUserAnswer obj)
        {
            return new UserAnswer
            {
                QuestionId = obj.QuestionId,
                IsCorrect = obj.IsCorrect
            };
        }
    }
}
