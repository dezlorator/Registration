using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerString { get; set; }
        public string QuestionString { get; set; }
        public virtual Question Question { get; set; }
        public int? QuestionId { get; set; }
    }
}
