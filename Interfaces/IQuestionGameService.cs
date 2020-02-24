using Registration.Models;
using Registration.Models.ResponceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IQuestionGameService
    {
        Question GetByIndex(int index);
        int GetSize();
        Task<ResponceQuestion> GetWithImageByIndex(int id);
        Task<string> Create(Question question);
        Task<string> Edit(Question question, int id);
        Question Get(int id);
        Task<string> Delete(int id);
        Question GetById(int id);
    }
}
