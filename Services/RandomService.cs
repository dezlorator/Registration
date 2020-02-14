using Ninject;
using Registration.Interfaces;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Services
{
    public class RandomService : IRandomService
    {
        #region fields
        private readonly Random random;
        #endregion

        public RandomService()
        {
            random = new StandardKernel(new RandomModule()).Get<Random>();
        }
        public int GetRandomNumber(int begin, int end)
        {
            return random.Next(begin, end);
        }
    }
}
