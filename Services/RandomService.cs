using Ninject;
using Registration.Interfaces;
using Registration.Models;
using System;

namespace Registration.Services
{
    public class RandomService : IRandomService
    {
        #region fields
        private readonly Random random;
        #endregion

        public RandomService(Random random)
        {
            //this.random = new StandardKernel(new RandomModule()).Get<Random>();
            this.random = random;
        }
        public int GetRandomNumber(int begin, int end)
        {
            return random.Next(begin, end);
        }
    }
}
