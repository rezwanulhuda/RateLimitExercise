using System;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			
			config.MapHttpAttributeRoutes();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
            .Add(new RequestHeaderMapping("Accept",
                              "text/html",
                              StringComparison.InvariantCultureIgnoreCase,
                              true,
                              "application/json"));            
        }
	}
}
