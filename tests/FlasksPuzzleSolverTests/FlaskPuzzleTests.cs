using FlasksPuzzleSolver;

namespace FlasksPuzzleSolverTests
{
    [TestClass]
    public class FlaskPuzzleTests
    {
        [TestMethod]
        public void Solve_NoSolution()
        {
            string[][] data = [
                ["+", "Rhombus", "Flash", "+"],
                ["Square", "Star", "Triangle", "-"],
                ["-", "Flash", "Heart", "Splash"],
                ["Rhombus", "Circle", "Flash", "Triangle"],
                ["Star", "-", "Circle", "Triangle"],
                ["Star", "Heart", "Splash", "Rhombus"],

                ["Circle", "Rhombus", "Splash", "Square"],
                ["Square", "Star", "Splash", "Heart"],
                ["Flash", "-", "Heart", "+"],
                ["+", "Circle", "Triangle", "Square"],
                ["","","",""],
                ["","","",""],
            ];
            var puzzle = new FlaskPuzzle(data);

            puzzle.Solve();

            Assert.AreEqual(puzzle.IsSolved, false);
        }

        [TestMethod]
        public void Solve_SolvableSet()
        {
            string[][] data = [
                ["Pentagon", "Splash", "Square", "="],
                ["Square", "+", "Star", "Pentagon"],
                ["Splash", "Rhombus", "-", "Star"],
                ["Circle", "Rhombus", "Flash", "Circle"],
                ["Triangle", "Star", "Flash", "Triangle"],
                ["+", "Splash", "Heart", "Star"],

                ["Heart", "=", "-", "Rhombus"],
                ["Circle", "Triangle", "+","="],
                ["Circle", "Square", "Flash", "Pentagon"],
                ["Pentagon", "-", "Heart", "Splash"],
                ["=","Square","Heart","Rhombus"],
                ["-","Triangle","+","Flash"],

                ["","","",""],
                ["","","",""],
            ];
            var puzzle = new FlaskPuzzle(data);

            puzzle.Solve();

            Assert.AreEqual(puzzle.IsSolved, true);
        }
    }
}
