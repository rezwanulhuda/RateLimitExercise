using System;
namespace Lib
{
	public class LimitCounter : ILimitCounter, ILimitCounterStateFactory
    {
		private readonly TimeSpan allowedTime;
		private readonly int nrOfRequests;
		private readonly TimeSpan suspendedFor;

		private ILimitCounterState state;
		private static object locker = new object();

		
		public LimitCounter(int nrOfRequests, TimeSpan allowedTime, TimeSpan suspendFor)
		{
			this.nrOfRequests = nrOfRequests;
			this.allowedTime = allowedTime;
			this.suspendedFor = suspendFor;

			this.state = (this as ILimitCounterStateFactory).GetNewValidState();
		}



		public void Increase()
		{
			lock(locker)
			{
				this.state = this.state.Next();
				this.state.PerformStateOperation();
			}

            
		}
        

        ILimitCounterState ILimitCounterStateFactory.GetNewValidState()
        {
            return new ValidState(this, this.nrOfRequests, this.allowedTime);
        }

        ILimitCounterState ILimitCounterStateFactory.GetNewSuspendedState()
        {
            return new SuspendedState(this, this.nrOfRequests, this.suspendedFor);
        }
    }
}
