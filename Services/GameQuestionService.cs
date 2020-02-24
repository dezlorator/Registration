using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Ninject;
using Registration.Interfaces;
using Registration.Models;
using Registration.Models.ResponceModels;
using Registration.Validator.GameEntityValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Registration.Services
{
    public class GameQuestionService : IQuestionGameService
    {
        #region fields
        private readonly IQuestionRepository questionRepository;
        private readonly List<IQuestionValidator> validators;
        private readonly IInitializer<Question, ResponceQuestion> questionInitializer;
        private readonly IAnswerRepository answerRepository;
        private readonly IGetPhotoFromGoogleService getPhoto;
        private const string questionNotFound = "Question not found";
        private readonly ICacheService cachingService;
        #endregion

        public GameQuestionService(IQuestionRepository questionRepository, IAnswerRepository answerRepository,
            IGetPhotoFromGoogleService getPhoto,  ICacheService cachingService,
            IInitializer<Question, ResponceQuestion> questionInitializer, List<IQuestionValidator> validators)
        {
            this.questionRepository = questionRepository;
            this.validators = validators;
            this.answerRepository = answerRepository;
            this.getPhoto = getPhoto;
            this.cachingService = cachingService;
            this.questionInitializer = questionInitializer;
        }

        public async Task<string> Create(Question question)
        {
            string error = "";

            foreach(var a in validators)
            {
                if(!a.Validate(question, out error))
                {
                    return error;
                }
            }

            foreach(var answer in question.Answers)
            {
                answer.QuestionString = question.QuestionString;
            }

            await questionRepository.CreateQuestionAsync(question);

            return null;
        }

        public async Task<string> Delete(int id)
        {
            Question question = questionRepository.GetQuestion(id);

            if(question == null)
            {
                return questionNotFound;
            }

            await answerRepository.DeleteByQuestionIdAsync(id);
            await questionRepository.DeleteQuestion(id);

            return null;
        }

        public async Task<string> Edit(Question question, int id)
        {
            string error = "";

            foreach (var a in validators)
            {
                if (!a.Validate(question, out error))
                {
                    return error;
                }
            }

            var questionFromDb = questionRepository.GetQuestion(id);

            if(questionFromDb == null)
            {
                return questionNotFound;
            }

            questionFromDb.QuestionString = question.QuestionString;
            questionFromDb.TimeSpend = question.TimeSpend;

            answerRepository.UpdateRange(question.Answers);
            await questionRepository.Update(question);

            return null;
        }

        public int GetSize()
        {
            return questionRepository.GetSize();
        }

        public Question Get(int id)
        {
            var question = questionRepository.GetQuestion(id);
            if(question == null)
            {
                return null;
            }

            question.Answers = answerRepository.GetByQuestionId(question.Id).ToList();

            return question;
        }

        public Question GetByIndex(int index)
        {
            var question = questionRepository.GetByIndex(index);

            if (question == null)
            {
                return null;
            }

            question.Answers = answerRepository.GetByQuestionId(question.Id).ToList();

            return question;
        }

        public async Task<ResponceQuestion> GetWithImageByIndex(int index)
        {
            var question = GetByIndex(index);

            if(await cachingService.IsExist(question.QuestionString))
            {
                question.ImageUrl = await cachingService.GetItem(question.QuestionString);
            }
            else
            {
                question.ImageUrl = getPhoto.GetPhotoFromGoogle(question.QuestionString);
                await cachingService.SetItem(question.QuestionString, question.ImageUrl);
            }

            return questionInitializer.Initialize(question);
        }

        public Question GetById(int id)
        {
            return questionRepository.GetById(id);
        }
    }
}
