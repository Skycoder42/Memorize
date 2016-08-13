using System;
using System.Diagnostics.CodeAnalysis;
using Memorize.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memorize.Test.Models
{
    [TestClass]
    public class TimeScopeAlarmTest
    {
        [TestMethod]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        [SuppressMessage("ReSharper", "AccessToModifiedClosure")]
        public void TestConstructionRules()
        {
            var testSpan = 42;
            uint? testDays = null;
            TimeSpan? testTime = null;
            var testRepeat = false;

            //day scope
            var testScope = TimeScopeAlarm.SpanScope.Days;
            new TimeScopeAlarm(testScope, testSpan, null, testTime, testRepeat);
            new TimeScopeAlarm(testScope, testSpan, 0, testTime, testRepeat);
            MAssert.Throws<ArgumentException>(() => new TimeScopeAlarm(testScope, testSpan, 1, testTime, testRepeat));

            //week scope
            testScope = TimeScopeAlarm.SpanScope.Weeks;
            new TimeScopeAlarm(testScope, testSpan, null, testTime, testRepeat);
            new TimeScopeAlarm(testScope, testSpan, 0, testTime, testRepeat);
            new TimeScopeAlarm(testScope, testSpan, 7, testTime, testRepeat);
            MAssert.Throws<ArgumentException>(() => new TimeScopeAlarm(testScope, testSpan, 8, testTime, testRepeat));

            //month scope
            testScope = TimeScopeAlarm.SpanScope.Months;
            new TimeScopeAlarm(testScope, testSpan, null, testTime, testRepeat);
            new TimeScopeAlarm(testScope, testSpan, 0, testTime, testRepeat);
            new TimeScopeAlarm(testScope, testSpan, 31, testTime, testRepeat);
            MAssert.Throws<ArgumentException>(() => new TimeScopeAlarm(testScope, testSpan, 32, testTime, testRepeat));

            //year scope
            testScope = TimeScopeAlarm.SpanScope.Years;
            new TimeScopeAlarm(testScope, testSpan, null, testTime, testRepeat);
            new TimeScopeAlarm(testScope, testSpan, 0, testTime, testRepeat);
            new TimeScopeAlarm(testScope, testSpan, 366, testTime, testRepeat);
            MAssert.Throws<ArgumentException>(() => new TimeScopeAlarm(testScope, testSpan, 367, testTime, testRepeat));

            //invalid span
            MAssert.Throws<ArgumentException>(() => new TimeScopeAlarm(testScope, -1, testDays, testTime, testRepeat));

            //invalid time
            new TimeScopeAlarm(testScope, testSpan, testDays, TimeSpan.Zero, testRepeat);
            MAssert.Throws<ArgumentException>(() => new TimeScopeAlarm(testScope, testSpan, testDays, TimeSpan.FromSeconds(-1), testRepeat));
            MAssert.Throws<ArgumentException>(() => new TimeScopeAlarm(testScope, testSpan, testDays, TimeSpan.FromHours(24), testRepeat));
        }

        [TestMethod]
        public void TestNextDateCalcingForDays()
        {
            var testScope = TimeScopeAlarm.SpanScope.Days;
            var testSpan = 10;
            var testTime = TimeSpan.FromMinutes(42);
            var baseDate = new DateTime(2000, 1, 1, 10, 0, 0);

            //no repeat, no time
            var tAlarm = new TimeScopeAlarm(testScope, testSpan, null, null, false);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.IsNull(tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsFalse(tAlarm.Repeating);
            
            var tDate1 = new DateTime(2000, 1, 11, 0, 0, 0);
            var calc1 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc1);
            Assert.AreEqual(tDate1, calc1.Value);

            calc1 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNull(calc1);

            //repeat, no time
            tAlarm = new TimeScopeAlarm(testScope, testSpan, null, null, true);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.IsNull(tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsTrue(tAlarm.Repeating);

            var tDate2 = new DateTime(2000, 1, 11, 0, 0, 0);
            var calc2 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc2);
            Assert.AreEqual(tDate2, calc2.Value);

            calc2 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNotNull(calc2);
            Assert.AreEqual(tDate2, calc2.Value);

            //no repeat, time
            tAlarm = new TimeScopeAlarm(testScope, testSpan, null, testTime, false);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.IsNull(tAlarm.DaysOffset);
            Assert.AreEqual(testTime, tAlarm.DayTime);
            Assert.IsFalse(tAlarm.Repeating);

            var tDate3 = new DateTime(2000, 1, 11, 0, 42, 0);
            var calc3 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc3);
            Assert.AreEqual(tDate3, calc3.Value);

            calc3 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNull(calc3);

            //repeat, time
            tAlarm = new TimeScopeAlarm(testScope, testSpan, null, testTime, true);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.IsNull(tAlarm.DaysOffset);
            Assert.AreEqual(testTime, tAlarm.DayTime);
            Assert.IsTrue(tAlarm.Repeating);

            var tDate4 = new DateTime(2000, 1, 11, 0, 42, 0);
            var calc4 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc4);
            Assert.AreEqual(tDate4, calc4.Value);

            calc4 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNotNull(calc4);
            Assert.AreEqual(tDate4, calc4.Value);
        }

        [TestMethod]
        public void TestNextDateCalcingForWeeks()
        {
            var testScope = TimeScopeAlarm.SpanScope.Weeks;
            var testSpan = 10;
            uint testDays = 4;
            var baseDate = new DateTime(2000, 1, 1, 10, 0, 0);

            //no repeat, no days
            var tAlarm = new TimeScopeAlarm(testScope, testSpan, 0, null, false);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.IsNull(tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsFalse(tAlarm.Repeating);

            var tDate1 = new DateTime(2000, 3, 11, 0, 0, 0);
            var calc1 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc1);
            Assert.AreEqual(tDate1, calc1.Value);

            calc1 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNull(calc1);

            //repeat, no days
            tAlarm = new TimeScopeAlarm(testScope, testSpan, 0, null, true);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.IsNull(tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsTrue(tAlarm.Repeating);

            var tDate2 = new DateTime(2000, 3, 11, 0, 0, 0);
            var calc2 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc2);
            Assert.AreEqual(tDate2, calc2.Value);

            calc2 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNotNull(calc2);
            Assert.AreEqual(tDate2, calc2.Value);

            //no repeat, days
            tAlarm = new TimeScopeAlarm(testScope, testSpan, testDays, null, false);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.AreEqual(testDays, tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsFalse(tAlarm.Repeating);

            var tDate3 = new DateTime(2000, 3, 9, 0, 0, 0);
            var calc3 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc3);
            Assert.AreEqual(tDate3, calc3.Value);

            calc3 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNull(calc3);

            //repeat, days
            tAlarm = new TimeScopeAlarm(testScope, testSpan, testDays, null, true);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.AreEqual(testDays, tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsTrue(tAlarm.Repeating);

            var tDate4 = new DateTime(2000, 3, 9, 0, 0, 0);
            var calc4 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc4);
            Assert.AreEqual(tDate4, calc4.Value);

            calc4 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNotNull(calc4);
            Assert.AreEqual(tDate4, calc4.Value);
        }

        [TestMethod]
        public void TestNextDateCalcingForMonths()
        {
            var testScope = TimeScopeAlarm.SpanScope.Months;
            var testSpan = 10;
            uint testDays = 13;
            var baseDate = new DateTime(2000, 1, 3, 10, 0, 0);

            //no repeat, no days
            var tAlarm = new TimeScopeAlarm(testScope, testSpan, null, null, false);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.IsNull(tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsFalse(tAlarm.Repeating);

            var tDate1 = new DateTime(2000, 11, 3, 0, 0, 0);
            var calc1 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc1);
            Assert.AreEqual(tDate1, calc1.Value);

            calc1 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNull(calc1);

            //repeat, no days
            tAlarm = new TimeScopeAlarm(testScope, testSpan, null, null, true);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.IsNull(tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsTrue(tAlarm.Repeating);

            var tDate2 = new DateTime(2000, 11, 3, 0, 0, 0);
            var calc2 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc2);
            Assert.AreEqual(tDate2, calc2.Value);

            calc2 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNotNull(calc2);
            Assert.AreEqual(tDate2, calc2.Value);

            //no repeat, days
            tAlarm = new TimeScopeAlarm(testScope, testSpan, testDays, null, false);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.AreEqual(testDays, tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsFalse(tAlarm.Repeating);
            
            var tDate3 = new DateTime(2000, 11, 13, 0, 0, 0);
            var calc3 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc3);
            Assert.AreEqual(tDate3, calc3.Value);

            calc3 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNull(calc3);

            //repeat, days
            tAlarm = new TimeScopeAlarm(testScope, testSpan, testDays, null, true);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.AreEqual(testDays, tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsTrue(tAlarm.Repeating);

            var tDate4 = new DateTime(2000, 11, 13, 0, 0, 0);
            var calc4 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc4);
            Assert.AreEqual(tDate4, calc4.Value);

            calc4 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNotNull(calc4);
            Assert.AreEqual(tDate4, calc4.Value);
        }

        [TestMethod]
        public void TestNextDateCalcingForYears()
        {
            var testScope = TimeScopeAlarm.SpanScope.Years;
            var testSpan = 5;
            uint? testDays = 100;
            var baseDate = new DateTime(2000, 2, 10, 10, 0, 0);

            //no repeat, no days
            var tAlarm = new TimeScopeAlarm(testScope, testSpan, null, null, false);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.IsNull(tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsFalse(tAlarm.Repeating);

            var tDate1 = new DateTime(2005, 2, 10, 0, 0, 0);
            var calc1 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc1);
            Assert.AreEqual(tDate1, calc1.Value);

            calc1 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNull(calc1);

            //repeat, no days
            tAlarm = new TimeScopeAlarm(testScope, testSpan, null, null, true);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.IsNull(tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsTrue(tAlarm.Repeating);

            var tDate2 = new DateTime(2005, 2, 10, 0, 0, 0);
            var calc2 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc2);
            Assert.AreEqual(tDate2, calc2.Value);

            calc2 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNotNull(calc2);
            Assert.AreEqual(tDate2, calc2.Value);

            //no repeat, days
            tAlarm = new TimeScopeAlarm(testScope, testSpan, testDays, null, false);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.AreEqual(testDays, tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsFalse(tAlarm.Repeating);

            var tDate3 = new DateTime(2005, 4, 10, 0, 0, 0);
            var calc3 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc3);
            Assert.AreEqual(tDate3, calc3.Value);

            calc3 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNull(calc3);

            //repeat, days
            tAlarm = new TimeScopeAlarm(testScope, testSpan, testDays, null, true);
            Assert.AreEqual(testScope, tAlarm.Scope);
            Assert.AreEqual(testSpan, tAlarm.Span);
            Assert.AreEqual(testDays, tAlarm.DaysOffset);
            Assert.IsNull(tAlarm.DayTime);
            Assert.IsTrue(tAlarm.Repeating);

            var tDate4 = new DateTime(2005, 4, 10, 0, 0, 0);
            var calc4 = tAlarm.CalcNextTrigger(baseDate, 0);
            Assert.IsNotNull(calc4);
            Assert.AreEqual(tDate4, calc4.Value);

            calc4 = tAlarm.CalcNextTrigger(baseDate, 1);
            Assert.IsNotNull(calc4);
            Assert.AreEqual(tDate4, calc4.Value);
        }
    }
}
