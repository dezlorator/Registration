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

        #region ctor
        public QuestionsRepository(ApplicationContext Context)
        {
            context = Context;
        }
        #endregion

        public async Task CreateQuestionAsync(Question question)
        {
            await context.Questions.AddAsync(question);
            await context.SaveChangesAsync();
        }

        public async Task DeleteQuestion(int id)
        {
            var question = context.Questions.FirstOrDefault(p => p.Id == id);
            context.Questions.Remove(question);
            await context.SaveChangesAsync();
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return context.Questions;
        }

        public Question GetQuestion(int id)
        {
            return context.Questions.FirstOrDefault(p => p.Id == id);
        }

        public Question GetByIndex(int index)
        {
            return context.Questions.ToList().ElementAt(index);
        }

        public int GetSize()
        {
            return context.Questions.Count();
        }

        public async Task Update(Question question)
        {
            context.Questions.Update(question);
            await context.SaveChangesAsync();
        }
    }
}
