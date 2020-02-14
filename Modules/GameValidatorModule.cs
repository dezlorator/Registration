using Ninject.Modules;
using Registration.Interfaces;
using Registration.Validator.GameEntityValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Models
{
    public class GameValidatorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IQuestionValidator>().To<QuestionStringValidator>();
            Bind<IQuestionValidator>().To<AnswerValidator>();
        }
    }
}
