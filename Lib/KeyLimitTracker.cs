using System;
using System.Collections.Generic;

namespace Lib
{
	public class KeyLimitTracker
	{
		private readonly int nrOfRequests;
		private readonly TimeSpan defaultAllowedTime;
		private readonly TimeSpan defaultSuspendTime;

		Dictionary<string, ILimitCounter> items = new Dictionary<string, ILimitCounter>();

		private static object locker = new object();

		public void Track(string key)
		{
			
			if (!items.ContainsKey(key))
			{
				this.AddKey(key, this.nrOfRequests, this.defaultAllowedTime, this.defaultSuspendTime);
			}
			items[key].Increase();


		}

        public void AddKey(string key, int nrOfRequests)
        {
            this.AddKey(key, nrOfRequests, this.defaultAllowedTime, this.defaultSuspendTime);
            
        }

        private void AddKey(string key, int nrOfRequests, TimeSpan allowedTime, TimeSpan suspendTime)
		{
			
			if (!items.ContainsKey(key))
			{
				lock(locker)
				{
                    if (!items.ContainsKey(key))
                    {
                        var counter = new LimitCounter(nrOfRequests, allowedTime, suspendTime);
                        items.Add(key, counter);
                    }                        
				}
			}
		}

		public KeyLimitTracker(int nrOfRequests, TimeSpan defaultAllowedTime, TimeSpan defaultSuspendTime)
		{
			this.nrOfRequests = nrOfRequests;
			this.defaultAllowedTime = defaultAllowedTime;
			this.defaultSuspendTime = defaultSuspendTime;
		}

		public bool IsTracking(string key)
		{
			return this.items.ContainsKey(key);
		}

}
}
