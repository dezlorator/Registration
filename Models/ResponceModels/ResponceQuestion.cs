using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Models.ResponceModels
{
    public class ResponceQuestion
    {
        public int QuestionId { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Answers { get; set; }
        public string QuestionString { get; set; }
    }
}
