using System.Collections;
using System.Collections.Generic;

namespace PegePlayer.Common
{
    public class PegNode
    {
        public IEnumerable<PegNode> LinkedNodes => _linkedPegs;
        public Peg Peg => _peg;

        private readonly Peg _peg;
        private readonly ISet<PegNode> _linkedPegs;
        private readonly IDictionary<int, List<double>> _probabilityMemoizazion = new Dictionary<int, List<double>>();

        public PegNode(Peg peg)
        {
            _peg = peg;
            _linkedPegs = new HashSet<PegNode>();
        }

        public override string ToString()
        {
            return $"{Peg}";
        }

        public void AddLink(PegNode peg)
        {
            _linkedPegs.Add(peg);
        }

        public void AddMemoizacion(int initialPegColumn, double cumulativeProbability)
        {
            if (!_probabilityMemoizazion.ContainsKey(initialPegColumn))
            {
                _probabilityMemoizazion[initialPegColumn] = new List<double>();
            }

            _probabilityMemoizazion[initialPegColumn].Add(cumulativeProbability);
        }

        public bool HasMemoizasion()
        {
           return _probabilityMemoizazion.Keys.Count > 0;
        }

        public IDictionary<int, List<double>> GetMemoizaion()
        {
            return _probabilityMemoizazion;
        }

        public class Factory
        {
            private readonly IDictionary<Peg, PegNode> _pegNodesCreated = new Dictionary<Peg, PegNode>();

            public PegNode Create(Peg peg)
            {
                if (!_pegNodesCreated.ContainsKey(peg))
                {
                    _pegNodesCreated[peg] = new PegNode(peg);
                }

                return _pegNodesCreated[peg];
            }
        }
    }
}