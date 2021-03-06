﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IValidator<T> where T: class
    {
        bool Validate(T value, out string errorMessage);
    }
}
