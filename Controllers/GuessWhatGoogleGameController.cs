using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Registration.Interfaces;
using Registration.Models;
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
        #endregion

        #region ctor
        public GuessWhatGoogleGameController(IQuestionGameService QuestionGameService,
                                             IRandomService randomService)
        {
            questionGameService = QuestionGameService;
            this.randomService = randomService;
        }
        #endregion

        [HttpPost()]
        [EnableCors("CorsPolicyForGame")]
        public async Task<IActionResult> Create(Question question)
        {
            string result = await questionGameService.Create(question);

            if (result != null)
            {
                return ValidationProblem(result);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        [EnableCors("CorsPolicyForGame")]
        public async Task<IActionResult> Edit(Question question, [FromQuery]int id)
        {
            string result = await questionGameService.Edit(question, id);

            if (result != null)
            {
                return ValidationProblem(result);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        [EnableCors("CorsPolicyForGame")]
        public IActionResult GetQuestion(int id)
        {
            var result = questionGameService.Get(id);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
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
        public IActionResult GetQuestionWithPhoto()
        {
            int index = randomService.GetRandomNumber(0, questionGameService.GetSize());

            var question = questionGameService.GetWithImageByIndex(index);
            
            if (question == null)
            {
                return NotFound();
            }

            if(question.ImageUrl == "")
            {
                return NoContent();
            }
            var answers = question.Answers.Select(p => p.AnswerString);
            //надо ли делать отдельную модель?
            return Ok(new { question.QuestionString, answers, question .ImageUrl});
        }
    }
}