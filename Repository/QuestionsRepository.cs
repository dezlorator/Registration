using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Repository
{
    public class QuestionsRepository : IQuestionRepository
    {
        #region fields 
        private readonly ApplicationContext context;
        #endregion

        public QuestionsRepository(ApplicationContext Context)
        {
            context = Context;
        }

        public async Task CreateQuestionAsync(Question question)
        {
            await context.Questions.AddAsync(question);
        }

        public void DeleteAnswers(int questionId)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuestion(int id)
        {
            var question = context.Questions.FirstOrDefault(p => p.Id == id);
            context.Questions.Remove(question);
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return context.Questions;
        }

        public Question GetQuestion(int id)
        {
            return context.Questions.FirstOrDefault(p => p.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(Question question)
        {
            context.Questions.Update(question);
        }
    }
}
