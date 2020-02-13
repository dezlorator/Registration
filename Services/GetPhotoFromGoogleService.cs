using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using Registration.Interfaces;
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
        #endregion

        public GetPhotoFromGoogleService()
        {

        }

        public string GetPhotoFromGoogle(string question)
        {
            //customSearchService into field?
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
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
