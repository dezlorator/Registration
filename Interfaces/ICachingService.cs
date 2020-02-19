using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface ICacheService
    {
        Task SetItem(string key, string value);
        Task<string> GetItem(string key);
        Task<bool> IsExist(string key);
    }
}
