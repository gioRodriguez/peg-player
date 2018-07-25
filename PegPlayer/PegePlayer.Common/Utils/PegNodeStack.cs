using System.Collections.Generic;

namespace PegePlayer.Common.Utils
{
    public class PegNodeStack
    {
        private readonly Stack<PegNode> _stack = new Stack<PegNode>();

        private PegNodeStack()
        { }

        public int Count => _stack.Count;

        public static PegNodeStack Create()
        {
            return new PegNodeStack();
        }

        public void Push(PegNode pegNode)
        {
            _stack.Push(pegNode);
        }

        public PegNode Pop()
        {
            return _stack.Count > 0 ? _stack.Pop() : null;
        }

        public Peg PopPeg()
        {
            return _stack.Count > 0 ? _stack.Pop().Peg : null;
        }

        public Peg PeekPeg()
        {
            return _stack.Peek().Peg;
        }

        public void Clear()
        {
            _stack.Clear();
        }
    }
}
