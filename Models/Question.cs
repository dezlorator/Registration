using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionString { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public bool IsCorrect { get; set; }
    }
}
