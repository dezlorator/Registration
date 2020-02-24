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
        public virtual ICollection<Answer> Answers { get; set; }
        public bool IsCorrect { get; set; }
        public double TimeSpend { get; set; }
        public string ImageUrl { get; set; }
        public UserAnswer UserAnswer { get; set; }
    }
}
