using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Interfaces
{
    public interface IDownloadImageService
    {
        Task DownloadFromURL(string url, string destination, string fileName);
    }
}
