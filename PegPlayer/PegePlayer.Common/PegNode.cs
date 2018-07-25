using System.Collections.Generic;
using System.Linq;

namespace PegePlayer.Common
{
    public class PegNode
    {
        public Peg Peg { get; private set; }
        private readonly ISet<PegNode> _linkedPegs;
        private readonly IDictionary<int, double> _probabilityMemoization = new Dictionary<int, double>();

        public PegNode(Peg peg)
        {
            Peg = peg;
            _linkedPegs = new HashSet<PegNode>();
        }

        public override string ToString()
        {
            return $"{Peg}";
        }

        public PegNode AddLink(PegNode peg)
        {
            _linkedPegs.Add(peg);
            return peg;
        }

        public void SetMemoization(int column, double cumulativeProbability)
        {
            if (!_probabilityMemoization.ContainsKey(column))
            {
                _probabilityMemoization[column] = 0.0;                
            }
            
            _probabilityMemoization[column] += cumulativeProbability;
        }

        public bool HasMemoization()
        {
           return _probabilityMemoization.Keys.Count > 0;
        }

        public IEnumerable<Memoization> GetMemoization()
        {
            return _probabilityMemoization.Select(keyValue => Memoization.Create(keyValue.Key, keyValue.Value));
        }

        public class Factory
        {
            private readonly IDictionary<Peg, PegNode> _pegNodesCreated = new Dictionary<Peg, PegNode>();

            public PegNode GetPeg(Peg peg)
            {
                if (!_pegNodesCreated.ContainsKey(peg))
                {
                    _pegNodesCreated[peg] = new PegNode(peg);
                }

                return _pegNodesCreated[peg];
            }
        }
    }

    public class Memoization
    {
        public static Memoization Create(int column, double probability)
        {
            return new Memoization
            {
                Column = column,
                Probability = probability
            };
        }

        public int Column { get; set; }

        public double Probability { get; set; }
    }
}