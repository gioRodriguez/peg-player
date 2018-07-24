using System.Collections.Generic;

namespace PegePlayer.Common
{
    public interface IPegBoardSolutionStrategy
    {
        void Resolve();

        IEnumerable<Peg> GetBestPositions();
    }
}
