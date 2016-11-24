using System;
namespace Lib
{
	internal interface ILimitCounterState
	{
		ILimitCounterState Next();		
		void PerformStateOperation();
	}
}
