using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IUserValidator
    {
        bool Validate(User value, List<string> errorMessages);
    }
}
