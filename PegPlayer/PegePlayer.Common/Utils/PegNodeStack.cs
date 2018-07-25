using System.Collections.Generic;

namespace PegePlayer.Common.Utils
{
    public class PegNodeStack
    {
        private readonly IList<PegNode> _list = new List<PegNode>();
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
            _list.Insert(0, pegNode);
        }

        public PegNode Pop()
        {
            if (_stack.Count <= 0)
            {
                return null;
            }

            _list.RemoveAt(0);
            return _stack.Pop();

        }

        public Peg PopPeg()
        {
            var pegNode = Pop();
            return pegNode?.Peg;
        }

        public Peg PeekPeg()
        {
            return _stack.Peek().Peg;
        }

        public void Clear()
        {
            _stack.Clear();
            _list.Clear();
        }

        public IEnumerable<PegNode> AsList()
        {
            return _list;
        }
    }
}
