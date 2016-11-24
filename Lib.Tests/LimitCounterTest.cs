using NUnit.Framework;
using System;
namespace Lib.Tests
{
	[TestFixture()]
	public class LimitCounterTest
	{
		
		[Test()]
		public void Increase_WhenLimitExceeds_ThrowsException()
		{
			int nrOfRequests = 1;
			int allowedTime = 1;
			int secondsSuspended = 5;
			
			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			counter.Increase();
            			
			try
			{
				counter.Increase();
				Assert.Fail("Did not throw exception");
			}			
			catch (RateLimitExceededException)
			{				
			}
		}
        
        [Test()]
		public void Increase_WhenLimitExceeds_ThrowsExceptionDuringWaitTime()
		{
			int nrOfRequests = 1;
			int allowedTime = 1;
			int secondsSuspended = 3;
			
			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			counter.Increase();

            try
            {
                counter.Increase();
                Assert.Fail("Did not throw exception after exceeding limit");
            }
            catch (RateLimitExceededException)
            {

            }

            System.Threading.Thread.Sleep(1000);

            try
            {
                counter.Increase();
                Assert.Fail("Did not throw exception after 1 second of waiting");
            }
            catch (RateLimitExceededException)
            {
            }

            System.Threading.Thread.Sleep(1000);

            try
            {
                counter.Increase();
                Assert.Fail("Did not throw exception after 2 seconds of waiting");
            }
            catch (RateLimitExceededException)
            {
            }

            System.Threading.Thread.Sleep(990);

            try
            {
                counter.Increase();
                Assert.Fail("Did not throw exception after 2 seconds of waiting");
            }
            catch (RateLimitExceededException)
            {
            }
        }        

		[Test()]
		public void Increase_WhenLimitExceeds_AllowsAfterWaitTime()
		{
			int nrOfRequests = 1;
			int allowedTime = 1;
			int secondsSuspended = 2;

			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			counter.Increase();
			try
			{
				counter.Increase();
				Assert.Fail("Did not throw first time");
			}
            catch (RateLimitExceededException)
            {
            }

            System.Threading.Thread.Sleep(2000);
            			
			counter.Increase();
			

		}



		[Test()]
		public void Increase_WhenLimitDidNotExceed_AllowsAfterAllowedTime()
		{
			int nrOfRequests = 2;
			int allowedTime = 1;
			int secondsSuspended = 5;

			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			counter.Increase();
			counter.Increase();
			
            System.Threading.Thread.Sleep(1000);

            counter.Increase();
        }


	}
}
