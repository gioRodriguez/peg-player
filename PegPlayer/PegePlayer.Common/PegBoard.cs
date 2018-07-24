using System.Collections.Generic;
using PegePlayer.Common.Utils;

namespace PegePlayer.Common
{
    public class PegBoard
    {
        private readonly Peg.Factory _pegsFactory;
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public int Goal { get; private set; }
        public IEnumerable<Peg> MissingPegs { get; private set; }
        public Peg GoalPeg { get; private set; }

        private PegBoard(IPegBoardSource pegBoardSource)
        {
            Columns = pegBoardSource.Columns * 2 - 1;
            Rows = pegBoardSource.Rows;
            Goal = pegBoardSource.Goal;
            MissingPegs = pegBoardSource.MissingPegs;
            GoalPeg = Peg.Create(pegBoardSource.Rows, pegBoardSource.Goal + 1);
            _pegsFactory = new Peg.Factory(Rows, Columns, MissingPegs);
        }

        public static PegBoard FromSource(IPegBoardSource pegBoardSource)
        {
            return new PegBoard(pegBoardSource);
        }        

        private Peg GetUpAndLeftPegFrom(Peg peg)
        {
            return _pegsFactory.CreateUpAndLeftFrom(peg);
        }

        private Peg GetUpPegFrom(Peg peg)
        {
            return _pegsFactory.CreatePegUpFrom(peg);
        }

        private Peg GetUpAndRigthPegFrom(Peg peg)
        {
            return _pegsFactory.CreateUpAndRigthFrom(peg);
        }

        public IEnumerable<Peg> GetPegNeighboursFrom(Peg peg)
        {
            var neighbours = new List<Peg>();
            AddRigthNeighbours(peg, neighbours);
            AddUpNeighbours(peg, neighbours);
            AddLeftAndUpNeighbours(peg, neighbours);
            return neighbours;
        }

        private void AddRigthNeighbours(Peg peg, List<Peg> neighbours)
        {
            if (GetUpAndRigthPegFrom(peg).IsValid)
            {
                neighbours.Add(GetUpAndRigthPegFrom(peg));
            }
        }

        private void AddUpNeighbours(Peg peg, List<Peg> neighbours)
        {
            var upPeg = GetUpPegFrom(peg);
            if (!upPeg.IsMissingPeg)
            {
                return;
            }

            if (GetUpAndLeftPegFrom(upPeg).IsValid)
            {
                neighbours.Add(GetUpAndLeftPegFrom(upPeg));
            }

            if (GetUpAndRigthPegFrom(upPeg).IsValid)
            {
                neighbours.Add(GetUpAndRigthPegFrom(upPeg));
            }
        }

        private void AddLeftAndUpNeighbours(Peg peg, List<Peg> neighbours)
        {
            if (GetUpAndLeftPegFrom(peg).IsValid)
            {
                neighbours.Add(GetUpAndLeftPegFrom(peg));
            }
        }
    }
}
