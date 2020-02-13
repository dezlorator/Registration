using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IQuestionValidator
    {
        bool Validate(Question objectToValidate, out string error);
    }
}
