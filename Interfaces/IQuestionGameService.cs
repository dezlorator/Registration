using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IQuestionGameService
    {
        Question GetWithImage(int id, out string url);
        Task<string> Create(Question question);
        Task<string> Edit(Question question, int id);
        Question Get(int id);
        Task<string> Delete(int id);
    }
}
