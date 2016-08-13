using System;
using Memorize.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memorize.Test.Models
{
    [TestClass]
    public class TimepointAlarmTest
    {
        [TestMethod]
        public void TestNextDateCalcing()
        {
            var targetDate = new DateTime(2222, 2, 2, 2, 22, 22);
            var tAlarm = new TimepointAlarm(targetDate);

            Assert.AreEqual(targetDate, tAlarm.TimePoint);

            var calc1 = tAlarm.CalcNextTrigger(new DateTime(2000, 1, 1), 0);
            Assert.IsNotNull(calc1);
            Assert.AreEqual(targetDate, calc1.Value);

            var calc2 = tAlarm.CalcNextTrigger(new DateTime(2002, 4, 8), 42);
            Assert.IsNotNull(calc2);
            Assert.AreEqual(targetDate, calc2.Value);

            var calc3 = tAlarm.CalcNextTrigger(new DateTime(3000, 1, 1), 0);
            Assert.IsNull(calc3);
        }
    }
}
