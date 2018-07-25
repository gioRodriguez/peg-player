using System.Collections.Generic;
using System.Linq;
using PegePlayer.Common.Utils;

namespace PegePlayer.Common
{
    public class PoorManMemoizingPegsSolution : IPegBoardSolutionStrategy
    {
        private readonly PegBoard _pegBoard;
        private readonly IDictionary<int, double> _probabilityByColumn = new Dictionary<int, double>();
        private readonly PegNodeStack _pegNodeStack = PegNodeStack.Create();
        private readonly PegNode.Factory _pegNodesFactory = new PegNode.Factory();

        private PoorManMemoizingPegsSolution(PegBoard pegBoard)
        {
            _pegBoard = pegBoard;
            for (var columnIdx = 0; columnIdx < _pegBoard.Columns; columnIdx++)
            {
                _probabilityByColumn[columnIdx] = 0;
            }
        }

        #region IPegBoardSolutionStrategy Implementation

        public void Resolve()
        {
            if (_pegBoard.IsFreefallSolution())
            {
                _probabilityByColumn[_pegBoard.GoalPeg.Column] = 1;
                return;
            }

            TraversePegTree(_pegNodesFactory.GetByPeg(_pegBoard.GoalPeg), 1.0);
        }

        public IEnumerable<Peg> GetBestPositions()
        {
            return _probabilityByColumn
                .Select(keyValue => Peg.Create(1, keyValue.Key, keyValue.Value))
                .OrderByDescending(peg => peg.Probability);
        }

        #endregion

        private void TraversePegTree(PegNode pegNode, double cumulativeProbability)
        {
            _pegNodeStack.Push(pegNode);
            if (pegNode.HasMemoization())
            {
                ResolveWithMemoization(pegNode);
                return;
            }

            var pegNeighbours = _pegBoard.GetPegUpNeighboursFrom(pegNode.Peg); 
            if (!pegNeighbours.Any() || pegNode.Peg.IsInitialPeg)
            {
                _probabilityByColumn[pegNode.Peg.Column] += cumulativeProbability;
                SetupMemoization(pegNode);
                return;
            }

            foreach (var neighbour in pegNeighbours)
            {
                var linkedNode = pegNode.AddLink(_pegNodesFactory.GetByPeg(neighbour));
                TraversePegTree(linkedNode, cumulativeProbability * linkedNode.Peg.Probability);
            }
        }

        private void ResolveWithMemoization(PegNode pegNode)
        {
            foreach (var memoization in pegNode.GetMemoization().ToList())
            {
                _probabilityByColumn[memoization.Column] += UpdateMemoizedProbability(pegNode.Peg, memoization);
            }
            _pegNodeStack.Clear();
        }

        private double UpdateMemoizedProbability(Peg peg, Memoization memoization)
        {
            var updatedMemoizedProbability = memoization.Probability;
            if (_pegNodeStack.PeekPeg().Equals(peg))
            {
                _pegNodeStack.Pop();
            }

            foreach (var pegNode in _pegNodeStack.AsList())
            {
                updatedMemoizedProbability = updatedMemoizedProbability * pegNode.Peg.Probability;
                pegNode.SetMemoization(memoization.Column, updatedMemoizedProbability);
            }
            
            return updatedMemoizedProbability;
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

        public static PoorManMemoizingPegsSolution Create(PegBoard pegBoard)
        {
            return new PoorManMemoizingPegsSolution(pegBoard);
        }
    }
}
