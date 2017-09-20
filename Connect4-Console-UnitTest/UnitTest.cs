using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Connect4_Console;

namespace Connect4_Console_UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void isEmptyGame()
        {
            Program p = new Program();
            Assert.AreEqual(0, p.countDiskOnBoard());
        }
    }
}
