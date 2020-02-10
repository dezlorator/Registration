using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Registration.Models;

namespace Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuessWhatGoogleGameController : ControllerBase
    {
        #region fields
        private readonly ApplicationContext context;
        #endregion

        public GuessWhatGoogleGameController(ApplicationContext Context)
        {
            context = Context;
        }

        //[HttpPost]
        //public Task<IActionResult> CreateQuestion(GuessWhatGoogleGame question)
        //{

        //}
    }
}