using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Models
{
    public class SeedData
    {
        private readonly ApplicationContext context;
        public SeedData(ApplicationContext Context)
        {
            context = Context;
        }

        public void Initial()
        {
            if (context.Questions.FirstOrDefault(v => v.Id == v.Id) == null)
            {
                List<Question> questions = new List<Question>();

                questions.Add(new Question
                {
                    QuestionString = "Game",
                    IsCorrect = false,
                    TimeSpend = 8.32,
                    Answers = new List<Answer>
                    {
                        new Answer{ AnswerString = "Photo" },
                        new Answer{ AnswerString = "Video" },
                        new Answer{ AnswerString = "Game" },
                        new Answer{ AnswerString = "Picture" }
                    }
                });

                questions.Add(new Question
                {
                    QuestionString = "Furniture",
                    IsCorrect = false,
                    TimeSpend = 2.36,
                    Answers = new List<Answer>
                    {
                        new Answer{ AnswerString = "Furniture" },
                        new Answer{ AnswerString = "Chair" },
                        new Answer{ AnswerString = "Home" },
                        new Answer{ AnswerString = "Notebook" }
                    }
                });

                questions.Add(new Question
                {
                    QuestionString = "Clock",
                    IsCorrect = false,
                    TimeSpend = 4.19,
                    Answers = new List<Answer>
                    {
                        new Answer{ AnswerString = "Time" },
                        new Answer{ AnswerString = "Clock" },
                        new Answer{ AnswerString = "Button" },
                        new Answer{ AnswerString = "Mouse" }
                    }
                });

                context.Questions.AddRange(questions);
                context.SaveChanges();
            }
        }
    }
}
