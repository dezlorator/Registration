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
                    Answers = new List<Answer>
                    {
                        new Answer{ AnswerString = "Photo", IsRight = false },
                        new Answer{ AnswerString = "Video", IsRight = false },
                        new Answer{ AnswerString = "Game", IsRight = true },
                        new Answer{ AnswerString = "Picture", IsRight = false }
                    }
                });

                questions.Add(new Question
                {
                    QuestionString = "Furniture",
                    IsCorrect = false,
                    Answers = new List<Answer>
                    {
                        new Answer{ AnswerString = "Furniture", IsRight = false },
                        new Answer{ AnswerString = "Chair", IsRight = false },
                        new Answer{ AnswerString = "Home", IsRight = true },
                        new Answer{ AnswerString = "Notebook", IsRight = false }
                    }
                });

                questions.Add(new Question
                {
                    QuestionString = "Clock",
                    IsCorrect = false,
                    Answers = new List<Answer>
                    {
                        new Answer{ AnswerString = "Time", IsRight = false },
                        new Answer{ AnswerString = "Clock", IsRight = true },
                        new Answer{ AnswerString = "Button", IsRight = false },
                        new Answer{ AnswerString = "Mouse", IsRight = false }
                    }
                });

                context.Questions.AddRange(questions);
                context.SaveChanges();
            }
        }
    }
}
