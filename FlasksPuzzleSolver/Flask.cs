using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlasksPuzzleSolver
{
    public class Flask
    {
        private readonly string[] _contents;
        private string _topColor = string.Empty;
        private int _topCount = 0;
        private int _freeSpace = 0;
        

        public Flask(string[] contents)
        {
            if (contents.Length < 1)
            {
                throw new ArgumentException("Invalid contents.");
            }

            _contents = [.. contents];
            SetSummary();
        }

        public IReadOnlyList<string> Contents => _contents;
        public bool IsComplete => _topCount == _contents.Length;
        public bool IsEmpty => _contents.Length == _freeSpace;

        public int TopCount => _topCount;
        public int FreeSpace => _freeSpace;
        public string TopColor => _topColor;


        public Flask Copy()
        {
            return new Flask(_contents);
        }

        public bool CanTake(Flask source)
        {
            if (_freeSpace == 0)
                 return false;

            if (_freeSpace < source._topCount)
                return false;

            if (source._topColor == string.Empty)
                return false;

            if (!IsEmpty && source._topColor != _topColor)
                return false;

            if (IsEmpty && source._topCount + source._freeSpace == source._contents.Length)
                return false;

            return true;
        }

        public void Take(Flask source)
        {
            if (!CanTake(source))
            {
                throw new ArgumentException("Cannot mix in.");
            }

            var count = source._topCount;
            var color = source._topColor;

            // 4 1
            // 4 - 3 = 1 - 1 => 0
            var startIndex = _contents.Length - (_contents.Length - _freeSpace) - 1;
            while (count > 0)
            {
                _contents[startIndex] = color;
                startIndex--;
                count--;
            }
            SetSummary();

            
            startIndex = source._contents.Length - (source._contents.Length - source._freeSpace);
            count = source._topCount;
            while (count > 0)
            {
                source._contents[startIndex] = string.Empty;
                startIndex++;
                count--;
            }
            source.SetSummary();
        }

        private void SetSummary()
        {
            _topColor = string.Empty;
            _topCount = 0;
            _freeSpace = 0;
            var index = 0;
            while (index < _contents.Length)
            {
                if (_contents[index] != string.Empty)
                {
                    _topColor = _contents[index];
                    _topCount++;
                    break;
                }

                _freeSpace++;
                index++;
            }

            while (++index < _contents.Length)
            {
                if (_contents[index] != _topColor)
                {
                    break;
                }

                _topCount++;
            }
        }
    }
}
