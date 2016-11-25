using System;
using System.Collections.Generic;
using Lib;

namespace test
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//TestLimitCounter();
			TestKeyLimitTracker();

		}

		static void TestKeyLimitTracker()
		{
			int nrOfRequests = 900;
			int allowedTime = 100;
			int secondsSuspended = 1;

			KeyLimitTracker counter = new KeyLimitTracker(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			List<System.Threading.Thread> threads = new List<System.Threading.Thread>();


			for (int x = 0; x < 100; ++x)
			{
				var thread = new System.Threading.Thread(() =>
				{
					var id = System.Threading.Thread.CurrentThread.ManagedThreadId;
					for (int y = 0; y < 1000; ++y)
					{
						try
						{
							counter.Track(id.ToString());
							Console.WriteLine(String.Format("T-{0}-{1}", id, y));
						}
						catch (RateLimitExceededException e)
						{
							Console.WriteLine(String.Format("E{0}-{1}-{2}", id, y, e.Message));
							System.Threading.Thread.Sleep(1000);
						}


						System.Threading.Thread.Yield();
					}
				});

				threads.Add(thread);
			}
			Console.WriteLine("Starting...");
			foreach (var t in threads)
			{
				t.Start();
			}

			while (threads.TrueForAll(p => p.IsAlive == false))
			{
				System.Threading.Thread.Yield();
				System.Threading.Thread.Sleep(1000);

			}
		}

		static void TestLimitCounter()
		{
			int nrOfRequests = 90000;
			int allowedTime = 100;
			int secondsSuspended = 1;

			LimitCounter counter = new LimitCounter(nrOfRequests, TimeSpan.FromSeconds(allowedTime), TimeSpan.FromSeconds(secondsSuspended));

			List<System.Threading.Thread> threads = new List<System.Threading.Thread>();


			for (int x = 0; x < 100; ++x)
			{
				var thread = new System.Threading.Thread(() =>
				{
					var id = System.Threading.Thread.CurrentThread.ManagedThreadId;
					for (int y = 0; y < 1000; ++y)
					{
						try
						{
							counter.Increase();
							Console.WriteLine(String.Format("T-{0}-{1}", id, y));
						}
						catch (RateLimitExceededException e)
						{
							Console.WriteLine(String.Format("E{0}-{1}-{2}", id, y, e.Message));
							System.Threading.Thread.Sleep(1000);
						}


						System.Threading.Thread.Yield();
					}
				});

				threads.Add(thread);
			}
			Console.WriteLine("Starting...");
			foreach (var t in threads)
			{
				t.Start();
			}

			while (threads.TrueForAll(p => p.IsAlive == false))
			{
				System.Threading.Thread.Yield();
				System.Threading.Thread.Sleep(1000);

			}
		}
	}
}
