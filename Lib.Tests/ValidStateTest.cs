using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;

namespace Lib.Tests
{

    [TestFixture]
    public class ValidStateTest
    {

        [TestCase]
        public void CurrentCount_WhenInvokedInitially_Returs0()
        {            
            ValidState state = new ValidState(null, 100, TimeSpan.FromSeconds(5));
            Assert.AreEqual(0, state.CurrentCount);
        }

        [TestCase]
        public void PerformStateOperation_IncreasesCounterBy1()
        {            
            ValidState state = new ValidState(null, 100, TimeSpan.FromSeconds(5));
            Assert.AreEqual(0, state.CurrentCount);
            state.PerformStateOperation();
            Assert.AreEqual(1, state.CurrentCount);
            state.PerformStateOperation();
            Assert.AreEqual(2, state.CurrentCount);
        }

        [TestCase]
        public void Next_WhenNotExpiredAndLimitNotExceeded_ReturnsSameInstance()
        {            
            ValidState state = new ValidState(null, 100, TimeSpan.FromSeconds(5));
            var next = state.Next();
            Assert.AreSame(state, next);
            state.PerformStateOperation();
            next = state.Next();
            Assert.AreSame(state, next);
        }

        [TestCase]
        public void Next_WhenExpiredAndLimitNotExceeded_ReturnsNewInstanceOfValidState()
        {
            Mock<ILimitCounterStateFactory> factoryMock = new Mock<ILimitCounterStateFactory>();
            factoryMock.Setup(p => p.GetNewValidState()).Returns(() => new ValidState(factoryMock.Object, 2, TimeSpan.FromSeconds(1)));

            var state = factoryMock.Object.GetNewValidState();
            state.PerformStateOperation();
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(1, state.CurrentCount);
            var next = state.Next();
            Assert.AreNotSame(state, next);
                        
            Assert.AreEqual(0, next.CurrentCount);
            Assert.IsInstanceOf<ValidState>(next);
            
        }

        [TestCase]
        public void Next_WhenNotExpiredAndLimitExceeded_ReturnsNewInstanceOfSuspendedState()
        {
            Mock<ILimitCounterStateFactory> factoryMock = new Mock<ILimitCounterStateFactory>();
            factoryMock.Setup(p => p.GetNewValidState()).Returns(() => new ValidState(factoryMock.Object, 2, TimeSpan.FromSeconds(1)));
            factoryMock.Setup(p => p.GetNewSuspendedState()).Returns(() => new SuspendedState(factoryMock.Object, 2, TimeSpan.FromSeconds(1)));

            var state = factoryMock.Object.GetNewValidState();

            var next = state.Next();
            next.PerformStateOperation();
            Assert.AreSame(state, next);

            next = state.Next();
            next.PerformStateOperation();            
            Assert.AreSame(state, next);

            next = state.Next();
            Assert.IsInstanceOf<SuspendedState>(next);
        }
    }
}
