using Registration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Registration.Services
{
    public class DownloadImageService : IDownloadImageService
    {
        public async Task DownloadFromURL(string url, string destination, string fileName)
        {
            //Try catch?web client into field? return value?await?
            WebClient webClient = new WebClient();
            Uri uri = new Uri(url);
            //webClient.DownloadFile(url, destination + "/" + fileName);
            webClient.DownloadFile(url, @"C:\Users\danil\source\repos\Registration\Registration\Resources\Images\" + fileName);
        }
    }
}
