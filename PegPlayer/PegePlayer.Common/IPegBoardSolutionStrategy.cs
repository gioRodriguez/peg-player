using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegePlayer.Common
{
    public interface IPegBoardSolutionStrategy
    {
        void Resolve();

        IEnumerable<int> GetBestPositions();
    }
}
