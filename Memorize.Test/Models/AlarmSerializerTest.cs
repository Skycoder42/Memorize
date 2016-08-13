using System;
using System.Collections.Generic;
using Memorize.Core;
using Memorize.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Memorize.Test.Models
{
    [TestClass]
    public class AlarmSerializerTest
    {
        private struct TestAlarm : IAlarm
        {
            [JsonConstructor]
            public TestAlarm(int testValue, string testName)
            {
                this.TestValue = testValue;
                this.TestName = testName;
            }

            public int TestValue { get; }
            public string TestName { get; }

            public int CompareTo(IAlarm other)
            {
                throw new NotImplementedException();
            }

            public int CompareTo(object obj)
            {
                throw new NotImplementedException();
            }

            public DateTime? CalcNextTrigger(DateTime lastTriggerTime, int triggerCount)
            {
                throw new NotImplementedException();
            }
        }

        private class TestClass
        {
            public int Id { get; set; }
            [JsonConverter(typeof(AlarmSerializer))]
            public IAlarm Alarm { get; set; }
        }

        [TestMethod]
        public void TestTestAlarmSerialisation()
        {
            var tAl = new TestAlarm(42, "baum");
            var ser = JsonConvert.SerializeObject(tAl);
            var deser = JsonConvert.DeserializeObject<TestAlarm>(ser);
            Assert.AreEqual(tAl.TestValue, deser.TestValue);
            Assert.AreEqual(tAl.TestName, deser.TestName);
        }

        [TestMethod]
        public void TestSerializer()
        {
            var tAl = new TestAlarm(42, "baum");
            var tCls = new TestClass {
                Id = 42,
                Alarm = tAl
            };

            var ser = JsonConvert.SerializeObject(tCls);
            var deser = JsonConvert.DeserializeObject<TestClass>(ser);

            Assert.AreEqual(tCls.Id, deser.Id);
            var rAl = (TestAlarm) deser.Alarm;
            Assert.AreEqual(tAl.TestValue, rAl.TestValue);
            Assert.AreEqual(tAl.TestName, rAl.TestName);
        }
    }
}
