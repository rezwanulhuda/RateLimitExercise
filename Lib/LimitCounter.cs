using System;
namespace Lib
{
	public class LimitCounter
	{
		TimeSpan allowedTime;
		int nrOfRequests;
		TimeSpan suspendedFor;
		DateTime firstRequest;
		public LimitCounter()
		{
			this.CurrentCount = 0;

		}

		public int CurrentCount { get; private set; }

		public LimitCounter(int nrOfRequests, TimeSpan allowedTime, TimeSpan suspendFor)
		{
			this.nrOfRequests = nrOfRequests;
			this.allowedTime = allowedTime;
			this.suspendedFor = suspendFor;
		}



		public void Increase()
		{
			if (this.CurrentCount == 0)
			{
				firstRequest = DateTime.Now;
			}

			var diff = DateTime.Now.Subtract(firstRequest).TotalSeconds;

			if (diff >= allowedTime.TotalSeconds)
			{
				if (this.CurrentCount >= this.nrOfRequests && diff < allowedTime.Add(this.suspendedFor).TotalSeconds)
				{
					throw new Exception(String.Format("Limit of {0} requests exceeded.", this.nrOfRequests));
				}
				else 
				{
					this.CurrentCount = 0;
					firstRequest = DateTime.Now;
				}

			}
			else
			{
				if (this.CurrentCount >= this.nrOfRequests)
				{
					throw new Exception(String.Format("Limit of {0} requests exceeded.", this.nrOfRequests));
				}
			}

			this.CurrentCount++;
		}



	}
}
