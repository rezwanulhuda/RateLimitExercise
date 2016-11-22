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

			if (this.CurrentCount >= this.nrOfRequests)
			{
				var diff = DateTime.Now.Subtract(firstRequest).Seconds;
				var next = allowedTime.Add(this.suspendedFor).Seconds;
				if (diff >= next)
				{
					this.CurrentCount = 0;
					firstRequest = DateTime.Now;
				}
				else 
				{
					throw new Exception(String.Format("Limit of {0} requests exceeded.", this.nrOfRequests));
				}


			}
				


			this.CurrentCount++;
		}



	}
}
