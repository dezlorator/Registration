using Registration.Interfaces;
using Registration.Models;
using Registration.Validator.GameEntityValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Services
{
    public class GameQuestionService : IQuestionGameService
    {
        #region fields
        private readonly IQuestionRepository questionRepository;
        private readonly List<IGameEntityValidator> validators;
        #endregion

        public GameQuestionService(IQuestionRepository QuestionRepository)
        {
            questionRepository = QuestionRepository;
            validators = new List<IGameEntityValidator> { new AnswerValidator(), new QuestionStringValidator()};
        }

        public async Task<bool> Create(Question question)
        {
            if(!validators.Any(x => x.Validate(question)))
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<Question> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
