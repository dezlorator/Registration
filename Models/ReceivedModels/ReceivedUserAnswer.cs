using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Models.ReceivedModels
{
    public class ReceivedUserAnswer
    {
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
