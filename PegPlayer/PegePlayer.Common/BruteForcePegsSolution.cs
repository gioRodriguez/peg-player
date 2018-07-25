using System.Collections.Generic;
using System.Linq;
using PegePlayer.Common.Utils;

namespace PegePlayer.Common
{
    public class BruteForcePegsSolution : IPegBoardSolutionStrategy
    {
        private readonly PegBoard _pegBoard;
        private readonly Peg _currentPeg;
        private readonly IDictionary<int, double> _probabilityByColumn = new Dictionary<int, double>();
        private readonly PegNodeStack _pegNodeStack = PegNodeStack.Create();
        private readonly PegNode.Factory _pegsFactory = new PegNode.Factory();

        public BruteForcePegsSolution(PegBoard pegBoard)
        {
            _pegBoard = pegBoard;
            _currentPeg = _pegBoard.GoalPeg;
            for (var columnIdx = 0; columnIdx < _pegBoard.Columns; columnIdx++)
            {
                _probabilityByColumn[columnIdx] = 0;
            }
        }

        public void Resolve()
        {
            if (_pegBoard.IsFreefallSolution())
            {
                _probabilityByColumn[_currentPeg.Column] = 1;
                return;
            }

            TraversePegTree(_pegsFactory.GetPeg(_currentPeg), _currentPeg, 1.0);
        }

        private void TraversePegTree(PegNode pegNode, Peg peg, double cumulativeProbability)
        {
            _pegNodeStack.Push(pegNode);
            if (pegNode.HasMemoization())
            {
                ResolveWithMemoization(pegNode, peg, cumulativeProbability);
                //UpdateMemoization(pegNode);
                return;
            }

            var pegNeighbours = _pegBoard.GetPegUpNeighboursFrom(peg); 
            if (!pegNeighbours.Any() || peg.IsInitialPeg)
            {
                _probabilityByColumn[peg.Column] += cumulativeProbability;
                SetupMemoization(pegNode);
                return;
            }

            foreach (var neighbour in pegNeighbours)
            {
                var linkedNode = pegNode.AddLink(_pegsFactory.GetPeg(neighbour));
                TraversePegTree(linkedNode, linkedNode.Peg, cumulativeProbability * linkedNode.Peg.Probability);
            }
        }

        private void ResolveWithMemoization(PegNode pegNode, Peg peg, double cumulativeProbability)
        {
            //_pegNodeStack.Clear();
            //var newMemoization = new Dictionary<int, double>();
            foreach (var memoization in pegNode.GetMemoization().ToList())
            {
                var memoizationProbability = memoization.Probability * cumulativeProbability;
                //foreach (var probability in memoization.Value)
                //{
                //    memoizationProbability = probability * peg.Probability;
                //    //while (_pegNodeStack.Count > 0)
                //    //{
                //    //    if (_pegNodeStack.PeekPeg().Equals(peg))
                //    //    {
                //    //        _pegNodeStack.Pop();
                //    //        continue;
                //    //    }

                //    //    memoizationProbability = memoizationProbability * _pegNodeStack.PopPeg().Probability;
                //    //}
                //}
                var newMomoization = memoization.Probability;
                while (_pegNodeStack.Count > 0)
                {
                    if (_pegNodeStack.PeekPeg().Equals(peg))
                    {
                        _pegNodeStack.Pop();
                        continue;
                    }

                    var previousNode = _pegNodeStack.Pop();
                    if (previousNode.Peg.Row == _pegBoard.Rows)
                    {
                        continue;
                    }

                    var previousProbability = newMomoization * previousNode.Peg.Probability;
                    previousNode.SetMemoization(memoization.Column, previousProbability);
                    newMomoization = previousProbability;
                }
                _probabilityByColumn[memoization.Column] += newMomoization;
            }
        }

        private void SetupMemoization(PegNode peg)
        {
            var memoizationProbability = _pegNodeStack.PopPeg().Probability;
            while (_pegNodeStack.Count > 0)
            {
                var previousNode = _pegNodeStack.Pop();
                if (previousNode.Peg.Row == _pegBoard.Rows)
                {
                    continue;
                }

                var previousProbability = memoizationProbability * previousNode.Peg.Probability;
                memoizationProbability = previousProbability;
                previousNode.SetMemoization(peg.Peg.Column, previousProbability);
            }
        }

        public IEnumerable<Peg> GetBestPositions()
        {
            return _probabilityByColumn
                .Select(keyValue => Peg.Create(1, keyValue.Key, keyValue.Value))
                .OrderByDescending(peg => peg.Probability);
        }
    }
}
