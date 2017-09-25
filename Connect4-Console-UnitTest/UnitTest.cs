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
        public void IsADraw()
        {
            Program p = new Program();

            for (var row = 1; row <= 5; row++)
                for (var column = 1; column <= 5; column++)
                    p.InsertDiscInColumn(column);

            Assert.IsTrue(p.IsFinished());
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
        [ExpectedException(typeof (ApplicationException), "Invalid input, Please enter a number between 1 and 5")]
        public void ValidateInputWithinColumnRange()
        {
            Program p = new Program();
            p.InsertDiscInColumn(0);

            p.InsertDiscInColumn(6);
        }

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
            int[] inputs = { 1, 1, 2, 2, 3, 3 };
            Program p = new Program();
            foreach (var i in inputs)
            {
                p.InsertDiscInColumn(i);
            }

            Assert.AreEqual(string.Empty, p.GetWinner());
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
            
            Assert.AreEqual(string.Empty, p.GetWinner());
            p.InsertDiscInColumn(1);
            Assert.AreEqual("R", p.GetWinner());
        }

        [TestMethod]
        public void VerifyWinnerOnDiscsAreConnectedDiagonallyLHS()
        {
            int[] inputs = {1, 2, 2, 3, 4, 3, 3, 4, 4, 5, 4};
            Program p = new Program();
            foreach (var i in inputs)
            {
                p.InsertDiscInColumn(i);
            }
            Assert.AreEqual("R", p.GetWinner());
            /*
             *  |OOOOO| 
             *  |OOORO| 
             *  |OORRO| 
             *  |ORYYO| 
             *  |RYYRY| 
             */
        }

        [TestMethod]
        public void VerifyWinnerOnDiscsAreConnectedDiagonallyRHS()
        {
            int[] inputs = { 3, 4, 2, 3, 2, 2, 1, 1, 1, 1 };
            Program p = new Program();
            foreach (var i in inputs)
            {
                p.InsertDiscInColumn(i);
            }
            Assert.AreEqual("Y", p.GetWinner());
            /*
             *  |OOOOO| 
             *  |YOOOO| 
             *  |RYOOO| 
             *  |YRYOO| 
             *  |RRRYO| 
             */
        }
    }
}