using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IQuestionGameService
    {
        public Task<bool> Create(Question question);
        public Task<bool> Edit(Question question);
        public Task<Question> Get(int id);
        public Task<bool> Delete(int id);
    }
}
