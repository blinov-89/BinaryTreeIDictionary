using System;
using System.Collections.Generic;

namespace BinaryTreeIDictionary
{
    [Serializable]
    class Node<Tkey, Tvalue> where Tkey :IComparable
    {
        public KeyValuePair<Tkey,Tvalue> KeyValuePair { get; set; }

        public Node<Tkey, Tvalue> Left { get; set; }
        public Node<Tkey, Tvalue> Right { get; set; }
        public Node<Tkey, Tvalue> Parent { get; set; }

    }
}
