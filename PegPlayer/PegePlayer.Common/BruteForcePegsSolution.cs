using System.Collections.Generic;
using System.Linq;

namespace PegePlayer.Common
{
    public class BruteForcePegsSolution : IPegBoardSolutionStrategy
    {
        private readonly PegBoard _pegBoard;
        private readonly Peg _currentPeg;
        private readonly IDictionary<int, List<double>> _state = new Dictionary<int, List<double>>();
        private Stack<PegNode> _pegNodesMemoizasion = new Stack<PegNode>();
        private PegNode.Factory _factory = new PegNode.Factory();

        public BruteForcePegsSolution(PegBoard pegBoard)
        {
            _pegBoard = pegBoard;
            _currentPeg = _pegBoard.GoalPeg;
        }

        public void Resolve()
        {
            if (_pegBoard.IsFreefallSolution())
            {
                _state[_currentPeg.Column] = new List<double> {1};
                return;
            }

            var root = _factory.Create(_currentPeg);
            TraversePegTree(root, _currentPeg, 1.0);
        }

        private void TraversePegTree(PegNode pegNode, Peg peg, double cumulativeProbability)
        {
            _pegNodesMemoizasion.Push(pegNode);
            if (pegNode.HasMemoizasion())
            {
                foreach (var memoization in pegNode.GetMemoizaion())
                {
                    foreach (var probability in memoization.Value)
                    {
                        if (!_state.ContainsKey(memoization.Key))
                        {
                            _state[memoization.Key] = new List<double>(0);
                        }

                        var previous = probability;
                        while (_pegNodesMemoizasion.Count > 0)
                        {
                            if (_pegNodesMemoizasion.Peek().Peg.Equals(peg))
                            {
                                _pegNodesMemoizasion.Pop();
                                continue;
                            }

                            previous = previous * _pegNodesMemoizasion.Pop().Peg.Probability;
                        }
                        _state[memoization.Key].Add(previous);
                    }
                }
                
                return;
            }

            var pegNeighbours = _pegBoard.GetPegUpNeighboursFrom(peg); 
            if (!pegNeighbours.Any() || peg.IsInitialPeg)
            {
                if (!_state.ContainsKey(peg.Column))
                {
                    _state[peg.Column] = new List<double>();
                }

                _state[peg.Column].Add(cumulativeProbability);

                var memoizationProbability = _pegNodesMemoizasion.Pop().Peg.Probability;
                while (_pegNodesMemoizasion.Count > 0)
                {                    
                    var previousNode = _pegNodesMemoizasion.Pop();
                    if (previousNode.Peg.Row == _pegBoard.Rows)
                    {
                        continue;
                    }

                    var previousProbability = memoizationProbability * previousNode.Peg.Probability;
                    memoizationProbability = previousProbability;
                    previousNode.AddMemoizacion(peg.Column, previousProbability);
                }
                return;
            }

            foreach (var neighbour in pegNeighbours)
            {
                pegNode.AddLink(_factory.Create(neighbour));
            }

            foreach (var linkedNode in pegNode.LinkedNodes)
            {
                TraversePegTree(linkedNode, linkedNode.Peg, cumulativeProbability * linkedNode.Peg.Probability);
            }
        }        

        public IEnumerable<Peg> GetBestPositions()
        {
            return _state
                .Select(keyValue => Peg.Create(1, keyValue.Key, keyValue.Value.Sum()))
                .OrderByDescending(peg => peg.Probability);
        }
    }
}
