﻿using System;
namespace Lib
{
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
}
