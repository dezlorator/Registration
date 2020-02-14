using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using Ninject;
using Ninject.Parameters;
using Registration.Interfaces;
using Registration.Models;
using Registration.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Services
{
    public class GetPhotoFromGoogleService : IGetPhotoFromGoogleService
    {
        #region fields
        //private const string apiKey = "AIzaSyC4c673yxhTA8fYizzZmrdjg8XtWpgpK_4";
        //private const string searchEngineId = "000826139542435041803:iuh7pds6crp";
        private const string apiKey = "AIzaSyAdIkUnMWWPtet-61OpGWV14GZ2SitCcoI";
        private const string searchEngineId = "000826139542435041803:eafvr9n6zay";
        private readonly CustomsearchService customSearchService;
        #endregion

        public GetPhotoFromGoogleService()
        {
            customSearchService = new StandardKernel(new CustomSearchModule())
                .Get<CustomsearchService>( new IParameter[] { new ConstructorArgument("initializer", 
                new BaseClientService.Initializer { ApiKey = apiKey }) } );
        }

        public string GetPhotoFromGoogle(string question)
        {
            var listRequest = customSearchService.Cse.List(question);
            listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            listRequest.Cx = searchEngineId;
            listRequest.Start = 0;
            IList<Result> paging = listRequest.Execute().Items;

            if (paging == null)
            {
                return "";
            }

            foreach (var item in paging)
            {
                if(item.Image != null)
                {
                    return item.Image.ThumbnailLink;
                }
            }

            return "";
        }
    }
}
