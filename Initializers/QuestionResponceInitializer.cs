using Registration.Interfaces;
using Registration.Models;
using Registration.Models.ResponceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Initializers
{
    public class QuestionResponceInitializer : IInitializer<Question, ResponceQuestion>
    {
        public ResponceQuestion Initialize(Question obj)
        {
            return new ResponceQuestion
            {
                QuestionId = obj.Id,
                QuestionString = obj.QuestionString,
                ImageUrl = obj.ImageUrl,
                Answers = obj.Answers.Select(p => p.AnswerString).ToList()
            };
        }
    }
}
