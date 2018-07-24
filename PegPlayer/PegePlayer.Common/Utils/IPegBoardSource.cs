using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegePlayer.Common.Utils
{
    public interface IPegBoardSource
    {
        int Columns { get; }
        int Rows { get; }
        int Goal { get; }
        IEnumerable<Peg> MissingPegs { get; }
    }
}
