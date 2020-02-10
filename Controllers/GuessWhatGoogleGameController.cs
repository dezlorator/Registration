using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ApplicationContext context;
        private readonly SeedData seedData;
        #endregion

        public GuessWhatGoogleGameController(ApplicationContext Context)
        {
            context = Context;
            seedData = new SeedData(context);
        }

        [HttpGet("Initial")]
        public void FillDataBase()
        {
            seedData.Initial();
        }

        //[HttpPost()]
        //public async Task<IActionResult> Create(Question question)
        //{

        //}
    }
}