using System;
using Memorize.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memorize.Test.Models
{
    [TestClass]
    public class TimeSpanAlarmTest
    {
        [TestMethod]
        public void TestNextDateCalcingWithoutRepeat()
        {
            var testSpan = TimeSpan.FromHours(5);
            var baseDate = new DateTime(2000, 1, 1, 10, 0, 0);

            var tAlarm = new TimeSpanAlarm(testSpan, false);
            Assert.AreEqual(testSpan, tAlarm.TimeSpan);
            Assert.IsFalse(tAlarm.Repeating);

            var tDate1 = new DateTime(2000, 1, 1, 15, 0, 0);
            var calc1 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc1);
            Assert.AreEqual(tDate1, calc1.Value);

            var calc2 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNull(calc2);
        }

        [TestMethod]
        public void TestNextDateCalcingWithRepeat()
        {
            var testSpan = TimeSpan.FromHours(5);
            var baseDate = new DateTime(2000, 1, 1, 10, 0, 0);

            var tAlarm = new TimeSpanAlarm(testSpan, true);
            Assert.AreEqual(testSpan, tAlarm.TimeSpan);
            Assert.IsTrue(tAlarm.Repeating);

            var tDate1 = new DateTime(2000, 1, 1, 15, 0, 0);
            var calc1 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc1);
            Assert.AreEqual(tDate1, calc1.Value);
            
            var calc2 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNotNull(calc2);
            Assert.AreEqual(tDate1, calc2.Value);
        }
    }
}
