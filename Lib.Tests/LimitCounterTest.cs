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
			System.Threading.Thread.Sleep(2000);
			counter.Increase();
		}

		[Test()]
		public void Increase_WhenTimeElapsedButLimitStillExists_ResetsCounter()
		{
			int nrOfRequests = 2;
			int allowedTime = 1;
			int secondsSuspended = 1;

			//nrOfRequests, seconds, minutesSuspended
			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			counter.Increase();
			System.Threading.Thread.Sleep(1000);
			counter.Increase();
			Assert.AreEqual(1, counter.CurrentCount);
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
			catch
			{
			}

			System.Threading.Thread.Sleep(1000);
			try
			{
				counter.Increase();
				Assert.Fail("Did not throw second time");
			}
			catch
			{
			}

			System.Threading.Thread.Sleep(2000);

			counter.Increase();
			Assert.AreEqual(1, counter.CurrentCount);

		}



		[Test()]
		public void Increase_WhenLimitAtPar_AllowsAfterWaitTime()
		{
			int nrOfRequests = 2;
			int allowedTime = 1;
			int secondsSuspended = 5;

			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			counter.Increase();
			counter.Increase();
			try
			{
				counter.Increase();
				Assert.Fail("did not throw");
			}
			catch
			{
			}

			System.Threading.Thread.Sleep(3000);

			try
			{
				counter.Increase();
				Assert.Fail("Did not throw");
			}
			catch
			{
			}

			//System.Threading.Thread.Sleep(1000);

			//counter.Increase();
			//Assert.AreEqual(1, counter.CurrentCount);

		}


	}
}
