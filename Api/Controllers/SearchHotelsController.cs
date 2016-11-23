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
		[HttpGet]
		[Route("search/{city}/{sort}")]
		public IHttpActionResult SearchHotels(string city, string sort)
		{            
			return Ok(String.Format("Hello Rate Limited world...{0}, {1}", city, sort));
		}
    }
}
