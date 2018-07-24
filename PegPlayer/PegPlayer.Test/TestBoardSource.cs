using System.Collections.Generic;
using PegePlayer.Common;
using PegePlayer.Common.Utils;

namespace PegPlayer.Test
{
    internal class TestBoardSource : IPegBoardSource
    {
        public int Columns { get; }
        public int Rows { get; }
        public int Goal { get; }
        public IEnumerable<Peg> MissingPegs { get; }

        public TestBoardSource(int columns, int rows, int goal, IEnumerable<Peg> missingPegs)
        {
            Columns = columns;
            Rows = rows;
            Goal = goal;
            MissingPegs = missingPegs;
        }

        public static TestBoardSource CreateDefault()
        {
            return new TestBoardSource(5, 5, 0, new[]
            {
                Peg.CreateMissingPeg(1, 1),
                Peg.CreateMissingPeg(2, 1),
                Peg.CreateMissingPeg(3, 2),
            });
        }
    }
}
