using System.Collections;
using System.Collections.Generic;

namespace PegePlayer.Common
{
    public class PegsBoardBruteForceIterator : IEnumerable<Peg>
    {
        private readonly PegBoard _pegBoard;
        private readonly Peg _currentPeg;

        public PegsBoardBruteForceIterator(PegBoard pegBoard)
        {
            _pegBoard = pegBoard;
            _currentPeg = _pegBoard.GoalPeg;
        }

        public IEnumerator<Peg> GetEnumerator()
        {
            var visited = new HashSet<Peg>();
            var stack = new Stack<Peg>();
            stack.Push(_currentPeg);

            while (stack.Count != 0)
            {
                var current = stack.Pop();
                //if (!visited.Add(current))
                //{
                //    continue;
                //}

                yield return current;
                foreach (var pegNeighbour in _pegBoard.GetPegNeighboursFrom(current))
                {
                    stack.Push(pegNeighbour);
                }
            }
        }        

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
