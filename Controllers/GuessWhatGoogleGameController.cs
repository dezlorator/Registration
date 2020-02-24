using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Registration.Interfaces;
using Registration.Models;
using Registration.Models.ReceivedModels;
using Registration.Services;
using Registration.Validator;

namespace Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuessWhatGoogleGameController : ControllerBase
    {
        #region fields
        private readonly IQuestionGameService questionGameService;
        private readonly IRandomService randomService;
        private readonly IWorkWithUserService workWithUserService;
        private readonly IConsumeRabbitMQHostedService<UserAnswer> rabbitMessager;
        private readonly IInitializer<ReceivedUserAnswer, UserAnswer> userAnswerInitializer;
        private const int userIdIndex = 0;
        #endregion

        #region ctor
        public GuessWhatGoogleGameController(IQuestionGameService questionGameService, IRandomService randomService, 
            IWorkWithUserService workWithUserService, IConsumeRabbitMQHostedService<UserAnswer> rabbitMessager,
            IInitializer<ReceivedUserAnswer, UserAnswer> userAnswerInitializer)
        {
            this.questionGameService = questionGameService;
            this.randomService = randomService;
            this.workWithUserService = workWithUserService;
            this.rabbitMessager = rabbitMessager;
            this.userAnswerInitializer = userAnswerInitializer;
        }
        #endregion

        [HttpPost()]
        [EnableCors("CorsPolicyForGame")]
        public async Task<IActionResult> Create(Question question)
        {
            string createdQuestion = await questionGameService.Create(question);

            if (createdQuestion != null)
            {
                return ValidationProblem(createdQuestion);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        [EnableCors("CorsPolicyForGame")]
        public async Task<IActionResult> Edit(Question question, int id)
        {
            string error = await questionGameService.Edit(question, id);

            if (error != null)
            {
                return ValidationProblem(error);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        [EnableCors("CorsPolicyForGame")]
        public IActionResult GetQuestion(int id)
        {
            var question = questionGameService.Get(id);

            if(question == null)
            {
                return NotFound();
            }
            //Problem with Json
            return Ok(question);
        }

        [HttpDelete()]
        [EnableCors("CorsPolicyForGame")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            string result = await questionGameService.Delete(id);

            if (result != null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("Question")]
        [EnableCors("CorsPolicyForGame")]
        public async Task<IActionResult> GetQuestionWithPhoto()
        {
            int index = randomService.GetRandomNumber(0, questionGameService.GetSize());

            var question = await questionGameService.GetWithImageByIndex(index);
            
            if (question == null)
            {
                return NotFound();
            }

            if(question.ImageUrl == null)
            {
                return NoContent();
            }

            return Ok(question);
        }

        [HttpPost("GetAnswer")]
        [EnableCors("CorsPolicyForGame")]
        public async Task<IActionResult> GetUserAnswer(ReceivedUserAnswer answer)
        {
            if (!workWithUserService.IsUserAuthorized())
            {
                return Unauthorized();
            }

            var question = questionGameService.GetById(answer.QuestionId);

            if(question == null)
            {
                return NotFound();
            }

            var userAnswer = userAnswerInitializer.Initialize(answer);

            userAnswer.UserId = workWithUserService.GetAuthorizedUserClaims().ToList()[userIdIndex].Value;

            rabbitMessager.SendMessage(userAnswer);

            return Ok();
        }
    }
}