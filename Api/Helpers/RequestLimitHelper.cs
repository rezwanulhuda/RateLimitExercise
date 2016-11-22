using System;
using Lib;

namespace Api
{
	public static class RequestLimitHelper
	{
		public static KeyLimitTracker GlobalTracker = null;
		static RequestLimitHelper()
		{
			GlobalTracker = new KeyLimitTracker(ConfigHelper.DefaultNrOfRequests, ConfigHelper.DefaultAllowedTime, ConfigHelper.DefaultSuspendTime);
		}
	}
}
