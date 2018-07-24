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
        private static IDictionary<Peg, PegNode> _pegNodesCreated = new Dictionary<Peg, PegNode>();

        private PegNode(Peg peg)
        {
            _peg = peg;
            _linkedPegs = new HashSet<PegNode>();
        }

        public static PegNode Create(Peg peg)
        {
            if (!_pegNodesCreated.ContainsKey(peg))
            {
                _pegNodesCreated[peg] = new PegNode(peg);
            }

            return _pegNodesCreated[peg];
        }        

        public PegNode AddLink(Peg peg)
        {
            var linkedPeg = Create(peg);
            _linkedPegs.Add(linkedPeg);
            return linkedPeg;
        }
    }
}