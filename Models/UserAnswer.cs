using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Models
{
    [Serializable]
    public class UserAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public string UserId { get; set; }
    }
}
