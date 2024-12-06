namespace FlasksPuzzleSolver
{
    internal class Program
    {
        static void Main(string[] args)
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

            if (puzzle.IsSolved)
            {
                Console.WriteLine(puzzle.VisualizeSolution());
            }
            else
            {
                Console.WriteLine($"No solution is found within {puzzle.TriedStates} possible states.");
            }
        }
    }
}
