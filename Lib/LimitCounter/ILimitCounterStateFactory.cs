using System;
namespace Lib
{
    internal interface ILimitCounterStateFactory
	{
		ILimitCounterState GetNewValidState();
		ILimitCounterState GetNewSuspendedState();
	}
}
