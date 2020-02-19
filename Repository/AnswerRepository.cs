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

        public async Task DeleteByQuestionIdAsync(int questionId)
        {
            var answersToDelete = context.Answers.Where(p => p.QuestionId == questionId);
            context.Answers.RemoveRange(answersToDelete);
            await context.SaveChangesAsync();
        }

        public IEnumerable<Answer> GetByQuestionId(int questionId)
        {
            var answersToGet = context.Answers.Where(p => p.QuestionId == questionId);

            return answersToGet;
        }

        public void UpdateRange(ICollection<Answer> answer)
        {
            context.Answers.UpdateRange(answer);
        }
    }
}
