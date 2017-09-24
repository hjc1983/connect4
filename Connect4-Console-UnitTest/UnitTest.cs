using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Connect4_Console;

namespace Connect4_Console_UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void IsANewGame()
        {
            Program p = new Program();
            Assert.AreEqual(0, p.CountDiscsOnBoard());
        }

        [TestMethod]
        public void IsNotANewGame()
        {
            Program p = new Program();
            p.InsertDiscInColumn(1);
            Assert.AreEqual(1, p.CountDiscsOnBoard());
        }


        //[TestMethod]
        //public void ValidateGameUserinput()
        //{
        //    Assert.IsFalse(Program.ValidateGameUserinput("aaa"));
        //    Assert.IsFalse(Program.ValidateGameUserinput("1 a"));
        //    Assert.IsFalse(Program.ValidateGameUserinput("a 2"));
        //    Assert.IsFalse(Program.ValidateGameUserinput("@ 2"));
        //    Assert.IsFalse(Program.ValidateGameUserinput("ab  3"));
        //    Assert.IsFalse(Program.ValidateGameUserinput("2 3 @"));
        //    Assert.IsTrue(Program.ValidateGameUserinput(" 2 3 "));
        //    Assert.IsTrue(Program.ValidateGameUserinput("5 5"));
        //}

        [TestMethod]
        [ExpectedException(typeof (ApplicationException), "No more room in this column.")]
        public void ValidateInsertDiscInColumn()
        {
            Program p = new Program();
            p.InsertDiscInColumn(1);
            Assert.AreEqual(1, p.CountDiscsOnBoard());

            p.InsertDiscInColumn(1);
            Assert.AreEqual(2, p.CountDiscsOnBoard());

            p.InsertDiscInColumn(1);
            Assert.AreEqual(3, p.CountDiscsOnBoard());
            p.InsertDiscInColumn(1);
            Assert.AreEqual(4, p.CountDiscsOnBoard());

            p.InsertDiscInColumn(1);
            Assert.AreEqual(5, p.CountDiscsOnBoard());

            p.InsertDiscInColumn(1);
            Assert.Fail("Column is full");
        }

        [TestMethod]
        public void GameStartedwithRed()
        {
            Program p = new Program();
            Assert.AreEqual(Program.Red, p.GetCurrentGamer());
        }

        [TestMethod]
        public void GameStartedwithRedThenYellowsTurn()
        {
            Program p = new Program();
            p.InsertDiscInColumn(1);

            Assert.AreEqual(Program.Yellow, p.GetCurrentGamer());
        }

        [TestMethod]
        public void VerifyWinnerOnDiscsAreConnectedHorizontally()
        {
            Program p = new Program();
            p.InsertDiscInColumn(1);
            p.InsertDiscInColumn(1);
            p.InsertDiscInColumn(2);
            p.InsertDiscInColumn(2);
            p.InsertDiscInColumn(3);
            p.InsertDiscInColumn(3);

            Assert.AreEqual(string.Empty, p.GetWinner());
            p.InsertDiscInColumn(4);
            Assert.AreEqual("R", p.GetWinner());
        }

        [TestMethod]
        public void VerifyWinnerOnDiscsAreConnectedVertically()
        {
            Program p = new Program();
            p.InsertDiscInColumn(1);
            p.InsertDiscInColumn(2);
            p.InsertDiscInColumn(1);
            p.InsertDiscInColumn(2);
            p.InsertDiscInColumn(1);
            p.InsertDiscInColumn(2);

            Assert.AreEqual(string.Empty, p.GetWinner());
            p.InsertDiscInColumn(1);
            Assert.AreEqual("R", p.GetWinner());
        }
    }
}