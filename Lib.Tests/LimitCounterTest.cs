using NUnit.Framework;
using System;
namespace Lib.Tests
{
	[TestFixture()]
	public class LimitCounterTest
	{
		[Test()]
		public void CurrentCount_Returns0_WhenCalledAfterInitializing()
		{
			int nrOfRequests = 0;
			int allowedTime = 0;
			int secondsSuspended = 0;

			//nrOfRequests, seconds, minutesSuspended
			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));


			Assert.AreEqual(0, counter.CurrentCount);


		}

		[Test()]
		public void Increase_IncreasesCounterBy1()
		{
			int nrOfRequests = 1;
			int allowedTime = 1;
			int secondsSuspended = 5;

			//nrOfRequests, seconds, minutesSuspended
			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			counter.Increase();
			Assert.AreEqual(1, counter.CurrentCount);


		}

		[Test()]
		public void Increase_WhenLimitExceeds_ThrowsException()
		{
			int nrOfRequests = 1;
			int allowedTime = 1;
			int secondsSuspended = 5;

			//nrOfRequests, seconds, minutesSuspended
			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			counter.Increase();
			Assert.AreEqual(1, counter.CurrentCount);
			try
			{
				counter.Increase();
				Assert.Fail("Did not throw exception");
			}
			catch (NUnit.Framework.AssertionException e)
			{
				throw e;
			}
			catch (Exception ex)
			{
				
				Assert.AreEqual(String.Format("Limit of {0} requests exceeded.", 1), ex.Message);
			}
		}

		[Test()]
		public void Increase_WhenLimitExceeds_AllowsRequestAfterElapsedTime()
		{
			int nrOfRequests = 1;
			int allowedTime = 1;
			int secondsSuspended = 1;

			//nrOfRequests, seconds, minutesSuspended
			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			counter.Increase();
			System.Threading.Thread.Sleep(secondsSuspended * 2 * 1000);
			counter.Increase();
		}

		[Test()]
		public void Increase_WhenTimeElapsedAfter_ResetsCounter()
		{
			int nrOfRequests = 2;
			int allowedTime = 2;
			int secondsSuspended = 1;

			//nrOfRequests, seconds, minutesSuspended
			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			counter.Increase();
			System.Threading.Thread.Sleep(2 * 1000);
			counter.Increase();
			Assert.AreEqual(1, counter.CurrentCount);



		}
	}
}
