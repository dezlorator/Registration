using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IAnswerRepository
    {
        void UpdateRange(ICollection<Answer> answer);
        Task DeleteByQuestionIdAsync(int questionId);
        IEnumerable<Answer> GetByQuestionId(int questionId);
    }
}
