using System.Collections;
using System.Collections.Generic;

namespace PegePlayer.Common
{
    public class PegNode
    {
        public IEnumerable<PegNode> LinkedNodes => _linkedPegs;
        public Peg Peg => _peg;

        private readonly Peg _peg;
        private readonly List<PegNode> _linkedPegs;

        private PegNode(Peg peg)
        {
            _peg = peg;
            _linkedPegs = new List<PegNode>();
        }

        public static PegNode Create(Peg peg)
        {
            return new PegNode(peg);
        }        

        public PegNode AddLink(Peg peg)
        {
            var linkedPeg = Create(peg);
            _linkedPegs.Add(linkedPeg);
            return linkedPeg;
        }
    }
}