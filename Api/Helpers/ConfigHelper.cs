using Api.Configuration;
using System;
using System.Configuration;

namespace Api
{
	public static class ConfigHelper
	{
		public static int DefaultNrOfRequests
		{
			get 
			{
				return int.Parse(ConfigurationManager.AppSettings["DefaultNrOfRequests"]);
			}

		}

		public static TimeSpan DefaultAllowedTime
		{
			get
			{
				var value = int.Parse(ConfigurationManager.AppSettings["DefaultAllowedTimeInSeconds"]);
				return TimeSpan.FromSeconds(value);
			}
		}

		public static TimeSpan DefaultSuspendTime
		{
			get
			{
				var value = int.Parse(ConfigurationManager.AppSettings["DefaultSuspendTimeInMinutes"]);
				return TimeSpan.FromMinutes(value);
			}

		}

        public static ApiKeysSection ApiKeys
        {
            get
            {
                return ConfigurationManager.GetSection("ApiKeys") as ApiKeysSection;
            }
        }

    }
}
