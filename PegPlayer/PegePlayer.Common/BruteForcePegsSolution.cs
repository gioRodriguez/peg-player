using System.Collections.Generic;
using System.Linq;

namespace PegePlayer.Common
{
    public class BruteForcePegsSolution : IPegBoardSolutionStrategy
    {
        private readonly PegBoard _pegBoard;
        private readonly Peg _currentPeg;
        private readonly IDictionary<int, double> _state = new Dictionary<int, double>();

        public BruteForcePegsSolution(PegBoard pegBoard)
        {
            _pegBoard = pegBoard;
            _currentPeg = _pegBoard.GoalPeg;
        }

        public void Resolve()
        {
            var root = PegNode.Create(_currentPeg);
            TraversePegTree(root, _currentPeg, 1.0);
        }

        private void TraversePegTree(PegNode pegNode, Peg peg, double cumulativeProbability)
        {
            var neighbours = _pegBoard.GetPegUpNeighboursFrom(peg);
            if (!neighbours.Any() || peg.IsInitialPeg)
            {
                if (!_state.ContainsKey(peg.Column))
                {
                    _state[peg.Column] = 0;
                }

                _state[peg.Column] += cumulativeProbability;
                return;
            }

            foreach (var neighbour in neighbours)
            {
                pegNode.AddLink(neighbour);                
            }

            foreach (var linkedNode in pegNode.LinkedNodes)
            {
                TraversePegTree(linkedNode, linkedNode.Peg, cumulativeProbability * linkedNode.Peg.Probability);
            }
        }        

        public IEnumerable<Peg> GetBestPositions()
        {
            return _state
                .Select(keyValue => Peg.Create(1, keyValue.Key, keyValue.Value))
                .OrderByDescending(peg => peg.Probability);
        }
    }
}
