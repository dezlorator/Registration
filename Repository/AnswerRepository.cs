using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        #region fields
        private readonly ApplicationContext context;
        #endregion

        public AnswerRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task DeleteByQuestionString(string questionString)
        {
            var answersToDelete = context.Answers.Where(p => p.QuestionString == questionString);
            context.Answers.RemoveRange(answersToDelete);
            await context.SaveChangesAsync();
        }

        public IEnumerable<Answer> GetByQuestionString(string questionString)
        {
            var answersToGet = context.Answers.Where(p => p.QuestionString == questionString);
            return answersToGet;
        }

        public async Task Update(Answer answer)
        {
            context.Answers.Update(answer);
            await context.SaveChangesAsync();
        }
    }
}
