using System;
namespace Lib
{
	internal class ValidState : BaseCounterState
	{
		private readonly int limit;
		private int currentCount;
		public ValidState(ILimitCounterStateFactory statefactory, int limit, TimeSpan allowedTime)
			: base(statefactory, allowedTime)
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

		public override void PerformStateOperation()
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
}
