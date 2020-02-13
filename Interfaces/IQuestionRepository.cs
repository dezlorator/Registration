using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IQuestionRepository
    {
        Task CreateQuestionAsync(Question question);
        void DeleteAnswers(int questionId);
        void DeleteQuestion(int id);
        void Update(Question question);
        Question GetQuestion(int id);
        IEnumerable<Question> GetAllQuestions();
        Task SaveChangesAsync();
    }
}
