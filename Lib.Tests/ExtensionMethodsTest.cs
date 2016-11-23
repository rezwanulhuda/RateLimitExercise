using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Lib.Tests
{
    [TestFixture]
    public class ExtensionMethodsTest
    {
        [TestCase]
        public void Format_WhenOnlySeconds_ShowsOnlySeconds()
        {
            var ts = TimeSpan.FromSeconds(5);
            var expected = "5 seconds";
            var actual = ts.Format();

            Assert.AreEqual(expected, actual);

        }

        [TestCase]
        public void Format_WhenOnlyMinutes_ShowsOnlyMinutes()
        {
            var ts = TimeSpan.FromMinutes(5);
            var expected = "5 minutes";
            var actual = ts.Format();

            Assert.AreEqual(expected, actual);

        }

        [TestCase]
        public void Format_WhenBothMinutesAndSeconds_ShowsMinutesAndSeconds()
        {
            var ts = TimeSpan.FromMinutes(5);
            ts = ts.Add(TimeSpan.FromSeconds(5));
            var expected = "5 minutes, 5 seconds";
            var actual = ts.Format();

            Assert.AreEqual(expected, actual);

        }

        [TestCase]
        public void Format_WhenHours_ShowsHours()
        {
            var ts = TimeSpan.FromHours(5);            
            var expected = "5 hours";
            var actual = ts.Format();

            Assert.AreEqual(expected, actual);

        }

        [TestCase]
        public void Format_WhenHoursAndSeconds_ShowsHoursAndSeconds()
        {
            var ts = TimeSpan.FromHours(5);
            ts = ts.Add(TimeSpan.FromSeconds(5));
            var expected = "5 hours, 5 seconds";
            
            var actual = ts.Format();

            Assert.AreEqual(expected, actual);

        }
    }
}
