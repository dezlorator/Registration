using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Ninject;
using Registration.Interfaces;
using Registration.Models;
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
        private readonly IEnumerable<IQuestionValidator> validators;
        private readonly IAnswerRepository answerRepository;
        private readonly IGetPhotoFromGoogleService getPhoto;
        private readonly IDownloadImageService downloadImage;
        private const string questionNotFound = "Question not found";
        private readonly IOptionsSnapshot<ServerURLSettings> settings;
        private readonly ICacheService cachingService;
        #endregion

        public GameQuestionService(IQuestionRepository questionRepository,
            IAnswerRepository answerRepository,
            IGetPhotoFromGoogleService getPhoto,
            IDownloadImageService downloadImage, 
            IOptionsSnapshot<ServerURLSettings> settings,
            ICacheService cachingService)
        {
            this.questionRepository = questionRepository;
            validators = new StandardKernel(new GameValidatorModule()).Get<IEnumerable<IQuestionValidator>>();
            this.answerRepository = answerRepository;
            this.getPhoto = getPhoto;
            this.downloadImage = downloadImage;
            this.settings = settings;
            this.cachingService = cachingService;
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

        public async Task<Question> GetWithImageByIndex(int index)
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

            return question;
        }
    }
}
