using System;
using Lib;

namespace Api
{
	public static class RequestLimitHelper
	{
		public static KeyLimitTracker GlobalTracker = null;
		static RequestLimitHelper()
		{
			GlobalTracker = new KeyLimitTracker(1, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
		}
	}
}
