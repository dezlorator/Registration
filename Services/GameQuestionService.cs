using Microsoft.Extensions.Options;
using Ninject;
using Registration.Interfaces;
using Registration.Models;
using Registration.Validator.GameEntityValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        #endregion

        public GameQuestionService(IQuestionRepository QuestionRepository,
            IAnswerRepository answerRepository,
            IGetPhotoFromGoogleService getPhoto,
            IDownloadImageService downloadImage, 
            IOptionsSnapshot<ServerURLSettings> settings)
        {
            questionRepository = QuestionRepository;
            var kernel = new StandardKernel(new GameValidatorModule());
            validators = kernel.Get<IEnumerable<IQuestionValidator>>();
            this.answerRepository = answerRepository;
            this.getPhoto = getPhoto;
            this.downloadImage = downloadImage;
            this.settings = settings;
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
            await questionRepository.SaveChangesAsync();

            return error;
        }

        public async Task<string> Delete(int id)
        {
            Question question = questionRepository.GetQuestion(id);

            if(question == null)
            {
                return questionNotFound;
            }

            await answerRepository.DeleteByQuestionString(question.QuestionString);
            questionRepository.DeleteQuestion(id);
            await questionRepository.SaveChangesAsync();

            return "";
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
            questionFromDb.Answers = question.Answers;

            questionRepository.Update(question);
            await questionRepository.SaveChangesAsync();

            return "";
        }

        public Question Get(int id)
        {
            var question = questionRepository.GetQuestion(id);
            question.Answers = answerRepository.GetByQuestionString(question.QuestionString).ToList();

            return question;
        }

        public Question GetWithImage(int id, out string url)
        {
            var question = Get(id);

            //url = getPhoto.GetPhotoFromGoogle(question.QuestionString);

            url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMhVhaZs2DEP7M-UyQaR11eLwQQceWVC6dY8qGcFdtlQcLxHFmMnd6J_1fpQ&s";

            return question;
        }
    }
}
