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
            Assert.IsFalse(p.IsFinished());
        }

        [TestMethod]
        public void IsNotANewGame()
        {
            Program p = new Program();
            p.InsertDiscInColumn(1);
            Assert.AreEqual(1, p.CountDiscsOnBoard());
        }

        [TestMethod]
        public void IsADrawGame()
        {
            Program p = new Program();

            for (var row = 1; row <= 5; row++)
                for (var column = 1; column <= 5; column++)
                    p.InsertDiscInColumn(column);
            
            Assert.IsTrue(p.IsFinished());
        }

        [TestMethod]
        public void VerifyConfigurationAmendment()
        {
            var key = "KEY1";
            var value = "VALUE1";
            Helper.AddUpdateAppSettings(key, value);
            var result =  Helper.ReadSetting(key);
            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void VerifyConfigurationNumericInput()
        {
            var num = Helper.ConvertToInt("a");
            Assert.AreEqual(-1, num);

            num = Helper.ConvertToInt("5");
            Assert.AreEqual(5, num);
        }

        [TestMethod]
        public void VerifyDisplayBoard()
        {
            Program p = new Program();
            p.InsertDiscInColumn(1);
            var output = p.GetBoard();
            Assert.IsTrue(output.Contains("|R----|"));
        }

        [TestMethod]
        [ExpectedException(typeof (Exception), "Invalid input, Please enter a number between 1 and 5")]
        public void ValidateInputWithinColumnRange()
        {
            Program p = new Program();
            p.InsertDiscInColumn(0);

            p.InsertDiscInColumn(6);
        }

        [TestMethod]
        public void VerifyRowPositionOnInsertion()
        {
            Program p = new Program();
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(i, p.InsertDiscInColumn(1));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No more room in this column.")]
        public void ValidateInsertWhenColumnisFull()
        {
            Program p = new Program();
            for (int i = 0; i < 9; i++)
                p.InsertDiscInColumn(1);

            Assert.Fail("Column is full");
        }

        [TestMethod]
        public void GameStartedwithRed()
        {
            Program p = new Program();
            Assert.AreEqual("R", p.GetCurrentGamer());
        }

        [TestMethod]
        public void GameStartedwithRedThenYellowsTurn()
        {
            Program p = new Program();
            p.InsertDiscInColumn(1);

            Assert.AreEqual("Y", p.GetCurrentGamer());
        }

        [TestMethod]
        public void VerifyWinnerOnDiscsAreConnectedHorizontally()
        {
            int[] inputs = { 1, 1, 2, 2, 3, 3 };
            Program p = new Program();
            foreach (var i in inputs)
            {
                p.InsertDiscInColumn(i);
            }

            Assert.IsTrue(string.IsNullOrEmpty(p.GetWinner()));
            p.InsertDiscInColumn(4);
            Assert.AreEqual("R", p.GetWinner());
        }

        [TestMethod]
        public void VerifyWinnerOnDiscsAreConnectedVertically()
        {
            int[] inputs = {1, 2, 1, 2, 1, 2};
            Program p = new Program();
            foreach (var i in inputs)
            {
                p.InsertDiscInColumn(i);
            }

            Assert.IsTrue(string.IsNullOrEmpty(p.GetWinner()));
            p.InsertDiscInColumn(1);
            Assert.AreEqual("R", p.GetWinner());
        }

        [TestMethod]
        public void VerifyWinnerOnDiscsAreConnectedDiagonallyLHS()
        {
            int[] inputs = { 5, 1, 2, 2, 3, 4, 3, 4, 4, 3, 2, 4 };
            Program p = new Program();
            foreach (var i in inputs)
            {
                p.InsertDiscInColumn(i);
            }
            Assert.AreEqual("Y", p.GetWinner());
        }

        [TestMethod]
        public void VerifyWinnerOnDiscsAreConnectedDiagonallyRHS()
        {
            int[] inputs = { 5, 4, 4, 3, 3, 2, 3, 2, 1, 2, 2 };
            Program p = new Program();
            foreach (var i in inputs)
            {
                p.InsertDiscInColumn(i);
            }
            Assert.AreEqual("R", p.GetWinner());
        }
    }
}