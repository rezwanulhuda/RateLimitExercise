using System;
using System.Web.Mvc;

namespace Api
{
	public class RateLimitFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			string key = filterContext.RouteData.Values["key"] as string;
			RequestLimitHelper.GlobalTracker.Track(key);
		}
	}
}
