using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Api.Controllers
{
	[RoutePrefix("api/{key}")]
	[RateLimitFilter]
    public class SearchHotelsController : ApiController
	{
        private readonly IDataStore dataStore;
        public SearchHotelsController(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }
		[HttpGet]
		[Route("search/{city}/{sort}")]
		public IHttpActionResult SearchHotels(string city, string sort)
		{
            SortOrder sorting;
			if (!Enum.TryParse<SortOrder>(sort, out sorting))
            {
                sorting = SortOrder.Undefined;
            }
            return Ok(this.dataStore.Search(city, sorting));            
		}
    }
}
