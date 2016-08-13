using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Memorize.Test
{
    internal static class MAssert
    {
        public static void Throws<TException>(Action action) 
            where TException : Exception
        {
            try {
                action();
                Assert.Fail($"Exception of type {typeof(TException).Name} was never thrown");
            } catch (TException) {}
        }
    }
}
