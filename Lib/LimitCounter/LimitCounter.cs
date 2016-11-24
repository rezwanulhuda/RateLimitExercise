using System;
namespace Lib
{
	public class LimitCounter : ILimitCounterStateFactory
	{
		private readonly TimeSpan allowedTime;
		private readonly int nrOfRequests;
		private readonly TimeSpan suspendedFor;

		ILimitCounterState state;

		public int CurrentCount { 
			get 
			{
				return this.state.CurrentCount;
			} 
		}

		public LimitCounter(int nrOfRequests, TimeSpan allowedTime, TimeSpan suspendFor)
		{
			this.nrOfRequests = nrOfRequests;
			this.allowedTime = allowedTime;
			this.suspendedFor = suspendFor;

			this.state = this.GetNewValidState();
		}



		public void Increase()
		{
			this.state = this.state.Next();
			this.state.PerformStateOperation();
            
		}

		public ILimitCounterState GetNewValidState()
		{
			return new ValidState(this, this.nrOfRequests, this.allowedTime);
		}

		public ILimitCounterState GetNewSuspendedState()
		{
			return new SuspendedState(this, this.nrOfRequests, this.allowedTime.Add(this.suspendedFor));
		}
	}
}
