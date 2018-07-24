using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegePlayer.Common
{
    public class PegBoardBruteForceSolutionStrategy : IPegBoardSolutionStrategy
    {
        private readonly PegBoard _pegBoard;
        private readonly IList<int> _bestPositions;

        private PegBoardBruteForceSolutionStrategy(PegBoard pegBoard)
        {
            _pegBoard = pegBoard;
            _bestPositions = new List<int>();
        }

        public void Resolve()
        {
            var initialPegProbabilities = new Dictionary<int, List<double>>();
            for (var initialPositionIdx = 0; initialPositionIdx < _pegBoard.Columns; initialPositionIdx++)
            {
                if (initialPositionIdx % 2 > 0)
                {
                    initialPegProbabilities[initialPositionIdx] = new List<double>();
                }
            }

            var probability = 1.0;
            foreach (var peg in new PegsBoardBruteForceIterator(_pegBoard))
            {
                probability = probability * peg.Probability;
                if (peg.IsInitialPeg)
                {
                    initialPegProbabilities[peg.Column].Add(probability);
                    probability = 1.0;
                }
            }

            _bestPositions.Add(0);
        }

        public IEnumerable<int> GetBestPositions()
        {
            return _bestPositions;
        }

        public static PegBoardBruteForceSolutionStrategy Create(PegBoard pegBoard)
        {
            return new PegBoardBruteForceSolutionStrategy(pegBoard);
        }
    }
}
