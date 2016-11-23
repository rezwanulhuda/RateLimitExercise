using System;
namespace Lib
{
	public class LimitCounter
	{
		TimeSpan allowedTime;
		int nrOfRequests;
		TimeSpan suspendedFor;
		DateTime firstRequest;
        bool limitExceeded;
        DateTime lastRequest;
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
            try
            {
                if (this.CurrentCount == 0)
                {
                    firstRequest = DateTime.Now;
                    limitExceeded = false;
                }

                var timeNow = DateTime.Now;
                var diff = timeNow.Subtract(firstRequest).TotalSeconds;

                if (diff > allowedTime.TotalSeconds)
                {
                    if (limitExceeded && timeNow.Subtract(lastRequest).TotalSeconds < suspendedFor.TotalSeconds)
                    {
                        throw new Exception(String.Format("Limit of {0} requests exceeded.", this.nrOfRequests));
                    }
                    else
                    {
                        this.CurrentCount = 0;
                        firstRequest = DateTime.Now;
                        limitExceeded = false;
                    }
                }
                else
                {
                    if (this.CurrentCount >= this.nrOfRequests)
                    {
                        limitExceeded = true;
                        lastRequest = DateTime.Now;
                        throw new Exception(String.Format("Limit of {0} requests exceeded.", this.nrOfRequests));                        
                    }
                }
            }
            finally
            {
                this.CurrentCount++;
            }
            

			
		}



	}
}
