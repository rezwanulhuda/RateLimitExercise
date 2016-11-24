using System;
namespace Lib
{
	public interface ILimitCounterState
	{
		ILimitCounterState Next();		
		void PerformStateOperation();
	}
}
