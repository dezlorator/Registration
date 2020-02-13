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
        #endregion

        public GuessWhatGoogleGameController(IQuestionGameService QuestionGameService)
        {
            questionGameService = QuestionGameService;
        }

        //[HttpGet("Initial")]
        //public void FillDataBase()
        //{
        //    seedData.Initial();
        //}

        [HttpPost()]
        [EnableCors("CorsPolicyForGame")]
        public async Task<IActionResult> Create(Question question)
        {
            string result = await questionGameService.Create(question);

            if (result != "")
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

            if (result != "")
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

            if (result != "")
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("Question/{id}")]
        public IActionResult GetQuestionWithPhoto(int id)
        {
            string imageUrl;

            var question = questionGameService.GetWithImage(id, out imageUrl);

            if (question == null)
            {
                return NotFound();
            }

            if(imageUrl == "")
            {
                return NoContent();
            }

            return Ok(new { question, imageUrl });
        }

    }
}