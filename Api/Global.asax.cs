﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Reflection;
using System.IO;
using Api.Helpers;

namespace Api
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
            //AreaRegistration.RegisterAllAreas();
            
            
            
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			GlobalConfiguration.Configuration.Filters.Add(new RateLimitFilterAttribute());
            DataStoreHelper.LoadFromFile();


        }
	}
}
