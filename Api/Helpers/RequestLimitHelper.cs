using System;
using Lib;
using Api.Configuration;

namespace Api
{
	public static class RequestLimitHelper
	{
		public static KeyLimitTracker GlobalTracker { get; private set; }
		static RequestLimitHelper()
		{
			GlobalTracker = new KeyLimitTracker(ConfigHelper.DefaultNrOfRequests, ConfigHelper.DefaultAllowedTime, ConfigHelper.DefaultSuspendTime);
            var k = ConfigHelper.ApiKeys;
            foreach (ApiKeyElement item in k.Instances)
            {
                GlobalTracker.AddKey(item.Key, item.RequestLimits);
            }
        }
	}
}
