using NUnit.Framework;
using System;
using Lib;

namespace Lib.Tests
{
	[TestFixture]
	public class KeyLimitTrackerTest
	{
		[TestCase]
		public void IsTracking_WhenNoKeysPresent_ReturnsFalse()
		{
			int nrOfRequests = 1;
			int waitTime = 1;
			int allowedTime = 1;

			KeyLimitTracker tracker = new KeyLimitTracker(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(waitTime));
            			
			Assert.IsFalse(tracker.IsTracking("key1"));
		}

		[TestCase]
		public void Track_WhenKeyIsMissing_AddsTheKeyWithDefaultValues()
		{
			int nrOfRequests = 1;
			int waitTime = 1;
			int allowedTime = 1;

			KeyLimitTracker tracker = new KeyLimitTracker(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(waitTime));
			tracker.Track("key1");
			Assert.IsTrue(tracker.IsTracking("key1"));
		}

		[TestCase]
		public void IsTracking_WhenKeyIsPresent_ReturnsTrue()
		{
			int nrOfRequests = 1;
			int waitTime = 1;
			int allowedTime = 1;

			KeyLimitTracker tracker = new KeyLimitTracker(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(waitTime));
			tracker.Track("key1");
			Assert.IsTrue(tracker.IsTracking("key1"));
		}

		[TestCase]
		public void Track_WhenLimitForKeyHasExceeded_ThrowsException()
		{
			int nrOfRequests = 1;
			int waitTime = 1;
			int allowedTime = 1;

			KeyLimitTracker tracker = new KeyLimitTracker(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(waitTime));
			tracker.Track("key1");
			try
			{
				tracker.Track("key1");
				Assert.Fail("did not throw exception");
			}			
			catch (RateLimitExceededException ex)
			{
				Assert.IsTrue(ex.Message.Contains("requests exceeded."));
			}
		}

		[TestCase]
		public void Track_MultipleKeys_TracksAsNeeded()
		{
			int nrOfRequests = 2;
			int waitTime = 1;
			int allowedTime = 1;

			KeyLimitTracker tracker = new KeyLimitTracker(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(waitTime));
			tracker.Track("key1");
			tracker.Track("key1"); //limit exceeded
			try
			{
				tracker.Track("key1");
				Assert.Fail("did not throw exception for key1");
			}
            catch (RateLimitExceededException ex)
            {
                Assert.IsTrue(ex.Message.Contains("requests exceeded."));
            }

			tracker.Track("key2");
			tracker.Track("key2");


		}

		[TestCase]
		public void AddKey_AddsKey()
		{
			int nrOfRequests = 2;
			int waitTime = 1;
			int allowedTime = 1;

			KeyLimitTracker tracker = new KeyLimitTracker(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(waitTime));
			tracker.AddKey("key1", 2);
			Assert.IsTrue(tracker.IsTracking("key1"));


		}
	}
}
