using System.Web;
using System.Web.Mvc;

namespace Api
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(System.Web.Http.Filters.HttpFilterCollection filters)
		{
			filters.Add(new RateLimitFilterAttribute());
		}
	}
}
