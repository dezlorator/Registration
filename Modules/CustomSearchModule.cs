using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Modules
{
    public class CustomSearchModule : NinjectModule
    {
        public override void Load()
        {
            Bind<CustomsearchService>().ToSelf();
        }
    }
}
