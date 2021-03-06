﻿using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IUserInitializer
    {
        UserIdentityChanged Initialize(User user);
    }
}
