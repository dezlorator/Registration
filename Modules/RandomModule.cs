using Ninject.Modules;
using Registration.Interfaces;
using Registration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Models
{
    public class RandomModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Random>().ToSelf();
        }
    }
}
