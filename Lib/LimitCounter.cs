using System;
namespace Lib
{
	public interface ILimitCounterState
	{
		ILimitCounterState Next();
		int CurrentCount { get; }
		void CheckState();
	}

	public abstract class BaseCounterState : ILimitCounterState
	{
		protected DateTime stateStart;
		protected ILimitCounterStateFactory stateFactory;
		protected TimeSpan lifeSpan;

		public BaseCounterState(ILimitCounterStateFactory stateFactory, TimeSpan lifeSpan)
		{
			this.stateFactory = stateFactory;
			this.stateStart = DateTime.Now;
			this.lifeSpan = lifeSpan;
		}

		public abstract int CurrentCount { get; }


		public abstract void CheckState();

		public abstract ILimitCounterState Next();

		protected bool Expired()
		{
			var timeNow = DateTime.Now;
			var diff = timeNow.Subtract(this.stateStart);
			return diff.TotalSeconds > lifeSpan.TotalSeconds;

		}
	}



	public interface ILimitCounterStateFactory
	{
		ILimitCounterState GetNewValidState();
		ILimitCounterState GetNewSuspendedState();
	}

	public class ValidState : BaseCounterState
	{
		private readonly int limit;
		private int currentCount;
		public ValidState(ILimitCounterStateFactory statefactory, int limit, TimeSpan allowedTime)
			:base(statefactory, allowedTime)
		{
			this.limit = limit;
		}

		public override int CurrentCount
		{
			get
			{
				return this.currentCount;
			}
		}

		public override void CheckState()
		{
			this.currentCount++;
		}

		public override ILimitCounterState Next()
		{			
			if (!this.Expired())
			{
				if (this.CurrentCount >= this.limit)
				{
					return this.stateFactory.GetNewSuspendedState();
				}
				else 
				{
					return this;
				}

			}
			else 
			{
				return this.stateFactory.GetNewValidState();
			}
		}
	}

	public class SuspendedState : BaseCounterState
	{
		private readonly int limit;

		private readonly ILimitCounterStateFactory statefactory;
		public SuspendedState(ILimitCounterStateFactory statefactory, int limit, TimeSpan suspendTime)
			: base(statefactory, suspendTime)
		{
			this.limit = limit;
			this.statefactory = statefactory;
		}

		public override int CurrentCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override void CheckState()
		{
			throw new RateLimitExceededException(this.limit, this.lifeSpan.Subtract(DateTime.Now.Subtract(stateStart)));
		}

		public override ILimitCounterState Next()
		{			
			if (this.Expired())
			{				
				return this.statefactory.GetNewValidState();
			}
			else 
			{
				return this;
			}
		}
	}



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
			this.state.CheckState();
            
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
