using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlasksPuzzleSolver
{
    public class FlaskPuzzle
    {
        private const string CollumnSeparator = "   ";

        private readonly HashSet<string> _states = new();
        private readonly Dictionary<string, int> colorsSummary = new();
        private readonly Stack<(List<Flask> Flasks, List<(int From, int To)> Moves)> _flasksToSolve = new();
        private readonly int _padding;
        private readonly List<Flask> _initialState;

        private List<(int From, int To)>? _solutionMoves;


        public FlaskPuzzle(string[][] data)
        {
            var flasks = new List<Flask>();
            foreach (var row in data)
            {
                flasks.Add(new Flask(row));
                foreach (var col in row)
                {
                    colorsSummary[col] = colorsSummary.GetValueOrDefault(col, 0) + 1;
                }
            }

            foreach(var color in colorsSummary.Keys)
            {
                _padding = Math.Max(color.Length, _padding);
            }

            var initialState = GetState(flasks);
            _flasksToSolve.Push((flasks, new ()));
            _initialState = flasks;
            _states.Add(initialState);
        }

        public bool IsSolved => _solutionMoves != null;
        public int TriedStates => _states.Count;

        public bool Solve()
        {
            while (_flasksToSolve.Count > 0)
            {
                var (flasks, pastMoves) = _flasksToSolve.Pop();

                if (flasks.All(f => f.IsComplete || f.IsEmpty))
                {
                    _solutionMoves = pastMoves;
                    return true;
                }

                var moves = GetMoves(flasks);
                foreach (var move in moves)
                {
                    var flasksCopy = ApplyMove(move, flasks);
                    var state = GetState(flasksCopy);

                    if (_states.Add(state))
                    {
                        var newMoves = new List<(int From, int To)>(pastMoves)
                        {
                            move
                        };

                        _flasksToSolve.Push((flasksCopy, newMoves));
                    }
                }

            }

            return false;
        }

        public string VisualizeSolution()
        {
            if (!IsSolved)
                return string.Empty;

            var sb = new StringBuilder();
            byte offset = 1;
            sb.Append(GetState(_initialState));
            var numerations = GetNumerations(_initialState, offset);
            sb.Append(numerations);
            var flasks = _initialState;

            foreach (var move in _solutionMoves!)
            {
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine($"From {move.From + offset} to {move.To + offset}:");
                flasks = ApplyMove(move, flasks);
                sb.Append(GetState(flasks));
                sb.Append(numerations);
            }

            return sb.ToString();
        }

        private string? GetNumerations(List<Flask> flasks, byte offset)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < flasks.Count; i++)
            {
                sb.Append($" {(i + offset).ToString().PadRight(_padding)} {CollumnSeparator}");
            }
            sb.AppendLine();

            return sb.ToString();
        }

        private static List<Flask> ApplyMove((int From, int To) move, List<Flask> flasks)
        {
            var flasksCopy = new List<Flask>(flasks);
            flasksCopy[move.From] = flasksCopy[move.From].Copy();
            flasksCopy[move.To] = flasksCopy[move.To].Copy();
            flasksCopy[move.To].Take(flasksCopy[move.From]);

            return flasksCopy;
        }

        private string GetState(List<Flask> flasks)
        {
            var rows = flasks.First().Contents.Count;
            var sb = new StringBuilder();
            for (var j = 0; j < rows; j++)
            {
                for (var i = 0; i < flasks.Count; i++)
                {
                    sb.Append($"|{flasks[i].Contents[j].PadRight(_padding)}|{CollumnSeparator}");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        private List<(int From, int To)> GetMoves(List<Flask> flasks)
        {
            var result = new List<(int From, int To)>();

            for (var i = 0; i < flasks.Count; i++)
            {
                for(var j = 0; j < flasks.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (flasks[i].CanTake(flasks[j]))
                    {
                        result.Add((j, i));
                    }
                }
            }

            return result;
        }
    }
}
