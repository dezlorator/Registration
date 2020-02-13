using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IAnswerRepository
    {
        Task Update(Answer answer);
        Task DeleteByQuestionString(string questionString);
        IEnumerable<Answer> GetByQuestionString(string questionString);

    }
}
