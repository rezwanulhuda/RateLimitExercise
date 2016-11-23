using System;
using System.Collections.Generic;

namespace Lib
{
	public class KeyLimitTracker
	{
		int nrOfRequests;
		TimeSpan defaultAllowedTime;
		TimeSpan defaultSuspendUntil;

		Dictionary<string, LimitCounter> items = new Dictionary<string, LimitCounter>();

		private static object locker = new object();

		public void Track(string key)
		{
			
			if (!items.ContainsKey(key))
			{
				this.AddKey(key, this.nrOfRequests, this.defaultAllowedTime, this.defaultSuspendUntil);
			}
			items[key].Increase();


		}

		public void AddKey(string key, int nrOfRequests, TimeSpan allowedTime, TimeSpan suspendUntil)
		{
			
			if (!items.ContainsKey(key))
			{
				lock(locker)
				{
                    if (!items.ContainsKey(key))
                    {
                        var counter = new LimitCounter(nrOfRequests, allowedTime, suspendUntil);
                        items.Add(key, counter);
                    }                        
				}
			}
		}

		public KeyLimitTracker(int nrOfRequests, TimeSpan defaultAllowedTime, TimeSpan defaultSuspendUntil)
		{
			this.nrOfRequests = nrOfRequests;
			this.defaultAllowedTime = defaultAllowedTime;
			this.defaultSuspendUntil = defaultSuspendUntil;
		}

		public bool IsTracking(string key)
		{
			return this.items.ContainsKey(key);
		}

}
}
