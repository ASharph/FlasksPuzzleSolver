using FlasksPuzzleSolver;

namespace FlasksPuzzleSolverTests
{
    [TestClass]
    public class FlaskTests
    {
        [TestMethod]
        public void InitProperties()
        {
            var flask1 = new Flask(["Blue", "Blue", "Blue", "Blue"]);
            var flask2 = new Flask(["", "", "", ""]);

            // flask2
            Assert.AreEqual(true, flask2.IsEmpty);

            // flask1
            Assert.AreEqual(true, flask1.IsComplete);
            Assert.AreEqual(4, flask1.TopCount);
        }

        [TestMethod]
        public void Take_MoveMultiple()
        {
            var flask1 = new Flask(["Blue", "Blue", "Blue", "Red"]);
            var flask2 = new Flask(["", "", "", ""]);

            flask2.Take(flask1);

            Assert.AreEqual("Blue", flask2.TopColor);
            Assert.AreEqual("Red", flask1.TopColor);

            Assert.AreEqual(3, flask2.TopCount);
            Assert.AreEqual(1, flask1.TopCount);

            Assert.AreEqual(1, flask2.FreeSpace);
            Assert.AreEqual(3, flask1.FreeSpace);
        }

        [TestMethod]
        public void Take_MoveMultiple2()
        {
            var flask1 = new Flask(["", "Blue", "Blue", "Red"]);
            var flask2 = new Flask(["", "", "", "Blue"]);

            flask2.Take(flask1);

            Assert.AreEqual("Blue", flask2.TopColor);
            Assert.AreEqual("Red", flask1.TopColor);

            Assert.AreEqual(3, flask2.TopCount);
            Assert.AreEqual(1, flask1.TopCount);

            Assert.AreEqual(1, flask2.FreeSpace);
            Assert.AreEqual(3, flask1.FreeSpace);
        }

        [TestMethod]
        public void Take_MoveMultiple3()
        {
            var flask1 = new Flask(["", "Blue", "Blue", "Blue"]);
            var flask2 = new Flask(["", "", "", "Blue"]);

            flask2.Take(flask1);

            Assert.AreEqual("Blue", flask2.TopColor);
            Assert.AreEqual("", flask1.TopColor);

            Assert.AreEqual(4, flask2.TopCount);
            Assert.AreEqual(0, flask1.TopCount);

            Assert.AreEqual(0, flask2.FreeSpace);
            Assert.AreEqual(4, flask1.FreeSpace);
        }

        [TestMethod]
        public void CanTake_AtLeastOneFlaskWillBeEmpty()
        {
            var flask1 = new Flask(["", "", "", "Blue"]);
            var flask2 = new Flask(["", "", "", ""]);

            var move = flask2.CanTake(flask1)!;
            Assert.AreEqual(false, move);
        }

        [TestMethod]
        public void CanTake_Empty()
        {
            var flask1 = new Flask(["Blue", "Blue", "Blue", "Red"]);
            var flask2 = new Flask(["", "", "", ""]);

            var move = flask2.CanTake(flask1)!;
            Assert.AreEqual(true, move);
        }

        [TestMethod]
        public void CanTake_SizeBig()
        {
            var flask1 = new Flask(["Blue", "Blue", "Yellow", "Red"]);
            var flask2 = new Flask(["", "Blue", "Yellow", "Yellow"]);

            Assert.AreEqual(false, flask2.CanTake(flask1));
            Assert.AreEqual(false, flask1.CanTake(flask2));
        }

        [TestMethod]
        public void CanTake_ColorMismatch()
        {
            var flask1 = new Flask(["", "Blue", "Yellow", "Red"]);
            var flask2 = new Flask(["", "Red", "Yellow", "Yellow"]);

            Assert.AreEqual(false, flask2.CanTake(flask1));
            Assert.AreEqual(false, flask1.CanTake(flask2));
        }

        [TestMethod]
        public void CanTake_TakeIsPossible()
        {
            var flask1 = new Flask(["", "", "Yellow", "Red"]);
            var flask2 = new Flask(["", "Yellow", "Yellow", "Red"]);

            Assert.AreEqual(true, flask2.CanTake(flask1));
            Assert.AreEqual(true, flask1.CanTake(flask2));
        }

        [TestMethod]
        public void CanTake_HasJustSingleColor()
        {
            var flask1 = new Flask(["", "", "", "Red"]);
            var flask2 = new Flask(["", "", "", ""]);

            var canTake1 = flask2.CanTake(flask1);
            var canTake2 = flask1.CanTake(flask2);

            Assert.AreEqual(false, canTake1);
            Assert.AreEqual(false, canTake2);
        }
    }
}