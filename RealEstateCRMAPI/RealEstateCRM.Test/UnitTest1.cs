using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RealEstateCRM.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int expected = 4;
            int actual = 2 * 2;
            Assert.AreEqual(expected, actual);
        }
    }
}
