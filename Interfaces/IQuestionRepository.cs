using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IQuestionRepository
    {
        Question GetByIndex(int index);
        int GetSize();
        Task CreateQuestionAsync(Question question);
        Task DeleteQuestion(int id);
        Task Update(Question question);
        Question GetQuestion(int id);
        IEnumerable<Question> GetAllQuestions();
    }
}
