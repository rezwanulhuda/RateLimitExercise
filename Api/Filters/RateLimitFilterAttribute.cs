using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Api
{
	[AttributeUsage(validOn:AttributeTargets.Class, AllowMultiple = false)]
	public class RateLimitFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
		{
			base.OnActionExecuting(actionContext);

			string key = actionContext.RequestContext.RouteData.Values["key"] as string;
			try
			{
				RequestLimitHelper.GlobalTracker.Track(key);
			}
			catch(Exception ex)
			{
				var resp = actionContext.Request.CreateResponse((System.Net.HttpStatusCode)429, ex.Message);
				actionContext.Response = resp;
			}

		}
	}
}
