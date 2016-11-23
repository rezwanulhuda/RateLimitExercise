using System;
namespace Lib
{
	public interface ILimitCounterStateFactory
	{
		ILimitCounterState GetNewValidState();
		ILimitCounterState GetNewSuspendedState();
	}
}
