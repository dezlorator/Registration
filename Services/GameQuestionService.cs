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
            validators = new StandardKernel(new GameValidatorModule()).Get<IEnumerable<IQuestionValidator>>();
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

            return null;
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
            questionFromDb.Answers = question.Answers;

            questionRepository.Update(question);
            await questionRepository.SaveChangesAsync();

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

            question.Answers = answerRepository.GetByQuestionString(question.QuestionString).ToList();

            return question;
        }

        public Question GetByIndex(int index)
        {
            var question = questionRepository.GetByIndex(index);

            if (question == null)
            {
                return null;
            }

            question.Answers = answerRepository.GetByQuestionString(question.QuestionString).ToList();

            return question;
        }

        public Question GetWithImageByIndex(int index)
        {
            var question = GetByIndex(index);

            question.ImageUrl = getPhoto.GetPhotoFromGoogle(question.QuestionString);

            return question;
        }
    }
}
