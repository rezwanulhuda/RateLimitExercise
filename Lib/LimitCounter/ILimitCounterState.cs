using System;
namespace Lib
{
	public interface ILimitCounterState
	{
		ILimitCounterState Next();
		int CurrentCount { get; }
		void PerformStateOperation();
	}
}
