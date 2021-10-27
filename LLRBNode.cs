using System;

namespace Algorithms
{
    partial class LLRB<KeyType, ValueType> where KeyType : IComparable
    {
        private class LLRBNode
        {
            public KeyType Key { get => key; }
            private readonly KeyType key;

            public bool IsRed = true;

            public ValueType Value;
            public LLRBNode LChild = null; //left child
            public LLRBNode RChild = null; //right child
            public LLRBNode(KeyType key, ValueType value)
            {
                this.key = key;
                this.Value = value;
            }
            public int CompareTo(LLRBNode that)
            {
                return this.Key.CompareTo(that.Key);
            }
        }
    }
}
