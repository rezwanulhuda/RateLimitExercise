﻿using System;
namespace Lib
{
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
}
