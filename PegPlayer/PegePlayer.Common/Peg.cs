using System.Collections.Generic;
using System.Linq;

namespace PegePlayer.Common
{
    public class Peg
    {
        public const int PegAtBorderProbality = 1;
        public const double PegProbality = 0.5;

        public static Peg OutOfBoard = Create(-1, -1, 0, false);

        public int Row { get; private set; }
        public int Column { get; private set; }
        public bool IsValid { get; private set; }
        public bool IsMissingPeg { get; private set; }
        public double Probability { get; private set; }
        public bool IsInitialPeg => Row == 1;

        private Peg()
        {}

        #region Equals and GetHashCode Implementation

        public override bool Equals(object obj)
        {
            return obj is Peg other && Equals(other);
        }

        protected bool Equals(Peg other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Column;
            }
        }

        #endregion

        public override string ToString()
        {
            if (Row == -PegAtBorderProbality && Column == -PegAtBorderProbality)
            {
                return "(OutOfBoard)";
            }

            if (Row == -2 && Column == -2)
            {
                return "(MissingPeg)";
            }

            return  $"({Row}, {Column})";
        }

        public static Peg Create(int row, int column, double probability = 0.5, bool isValid = true, bool isMissingPeg = false)
        {
            return new Peg
            {
                Row = row,
                Column = column,
                IsValid = isValid,
                IsMissingPeg = isMissingPeg,
                Probability = probability
            };
        }        

        public static Peg CreateMissingPeg(int row, int column)
        {
            return new Peg
            {
                Row = row,
                Column = row % 2 == 0 ? column * 2 : column * 2 + 1,
                IsValid = false,
                IsMissingPeg = true,
                Probability = PegAtBorderProbality
            };
        }

        public class Factory
        {            
            private readonly int _rows;
            private readonly int _columns;
            public IDictionary<Peg, Peg> MissingPegs = new Dictionary<Peg, Peg>();
            private static readonly IDictionary<int, Peg> CreatedPegs = new Dictionary<int, Peg>();

            public Factory(int rows, int columns, IEnumerable<Peg> missingPegs = null)
            {
                _rows = rows;
                _columns = columns;
                foreach (var missingPeg in missingPegs ?? Enumerable.Empty<Peg>())
                {
                    MissingPegs[missingPeg] = missingPeg;
                }
            }

            public Peg CreatePeg(int row, int column)
            {
                return IsOutOfBoard(row, column) ? OutOfBoard : NewPeg(row, column);
            }

            private Peg NewPeg(int row, int column)
            {
                var key = (row * 397) ^ column;
                if (!CreatedPegs.ContainsKey(key))
                {
                    CreatedPegs[key] = Create(row, column, PegProbability(column), row != 0);
                }

                var peg = CreatedPegs[key];
                return MissingPegs.ContainsKey(peg) ? MissingPegs[peg] : peg;
            }

            private double PegProbability(int column)
            {
                return column == 0 || column == _columns ? PegAtBorderProbality : PegProbality;
            }

            private bool IsOutOfBoard(int row, int column)
            {
                return row < 0 || row > _rows || column < 0 || column >= _columns;
            }

            public Peg CreatePegUpFrom(Peg peg)
            {
                return CreatePeg(peg.Row - 2, peg.Column);
            }

            public Peg CreateUpAndLeftFrom(Peg peg)
            {
                return CreatePeg(peg.Row - PegAtBorderProbality, peg.Column - PegAtBorderProbality);
            }

            public Peg CreateUpAndRigthFrom(Peg peg)
            {
                return CreatePeg(peg.Row - PegAtBorderProbality, peg.Column + PegAtBorderProbality);
            }

            public Peg CreateFreeFallPegFrom(Peg peg)
            {
                return CreatePeg(1, peg.Column);
            }
        }        
    }
    
}
