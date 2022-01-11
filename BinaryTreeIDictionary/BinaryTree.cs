using System;
using System.Collections.Generic;

namespace BinaryTreeIDictionary
{
    [Serializable]
    class BinaryTree<Tkey, Tvalue>
        where Tkey : IComparable
    {
        private Node<Tkey, Tvalue> root;
        

        public IEnumerable<KeyValuePair<Tkey, Tvalue>> Traverse()
        {
            return TraverseNode(root);
        }

        private IEnumerable<KeyValuePair<Tkey, Tvalue>> TraverseNode(Node<Tkey, Tvalue> currentNode)
        {
            if (currentNode == null)
                yield break;

            foreach (var keyValuePair in TraverseNode(currentNode.Left))
                yield return keyValuePair;

            yield return currentNode.KeyValuePair;

            foreach (var keyValuePair in TraverseNode(currentNode.Right))
                yield return keyValuePair;
        }

        public bool Add(KeyValuePair<Tkey, Tvalue> item)
        {
            var currentNode = root;
            while (currentNode != null)
            {
                var compareResult = item.Key.CompareTo(currentNode.KeyValuePair.Key);
                if (compareResult < 0)
                {
                    if (currentNode.Left == null)
                    {
                        currentNode.Left = new Node<Tkey, Tvalue>
                        {
                            KeyValuePair = item,
                            Parent = currentNode
                        };
                        return true;
                    }

                    currentNode = currentNode.Left;
                }
                else if (compareResult > 0)
                {
                    if (currentNode.Right == null)
                    {
                        currentNode.Right = new Node<Tkey, Tvalue>
                        {
                            KeyValuePair = item,
                            Parent = currentNode
                        };
                        return true;
                    }

                    currentNode = currentNode.Right;
                }
                else
                {                   
                    return false;
                }
            }

            root = new Node<Tkey, Tvalue>
            {
                KeyValuePair = item
            };
            return true;
        }

        public KeyValuePair<Tkey,Tvalue> Find(Tkey key)
        {
            var currentNode = root;
            while (currentNode != null)
            {
                var compareResult = key.CompareTo(currentNode.KeyValuePair.Key);
                if (compareResult < 0)
                {
                    currentNode = currentNode.Left;
                }
                else if (compareResult > 0)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    return currentNode.KeyValuePair;
                }
            }

            return new KeyValuePair<Tkey, Tvalue>();
        }

        public bool Remove(Tkey key)
        {
            var currentNode = root;
            while (currentNode != null)
            {
                var compareResult = key.CompareTo(currentNode.KeyValuePair.Key);
                if (compareResult < 0)
                {
                    currentNode = currentNode.Left;
                }
                else if (compareResult > 0)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    break;
                }
            }

            if (currentNode == null)
                return false;

            if (currentNode.Left == null && currentNode.Right == null)
            {
                if (currentNode.Parent == null)
                {
                    root = null;
                }
                else if (currentNode.Parent.Left == currentNode)
                {
                    currentNode.Parent.Left = null;
                }
                else
                {
                    currentNode.Parent.Right = null;
                }

                return true;
            }

            if (currentNode.Left == null)
            {
                currentNode.Right.Parent = currentNode.Parent;

                if (currentNode.Parent == null)
                {
                    root = currentNode.Right;
                }
                else if (currentNode.Parent.Left == currentNode)
                {
                    currentNode.Parent.Left = currentNode.Right;
                }
                else
                {
                    currentNode.Parent.Right = currentNode.Right;
                }

                return true;
            }

            if (currentNode.Right == null)
            {
                currentNode.Left.Parent = currentNode.Parent;

                if (currentNode.Parent == null)
                {
                    root = currentNode.Left;
                }
                else if (currentNode.Parent.Left == currentNode)
                {
                    currentNode.Parent.Left = currentNode.Left;
                }
                else
                {
                    currentNode.Parent.Right = currentNode.Left;
                }

                return true;
            }

            var leftmostNode = currentNode.Right;
            while (leftmostNode.Left != null)
            {
                leftmostNode = leftmostNode.Left;
            }

            currentNode.KeyValuePair = leftmostNode.KeyValuePair;
            if (leftmostNode.Right == null)
            {
                leftmostNode.Parent.Left = null;
            }
            else
            {
                leftmostNode.Parent.Left = leftmostNode.Right;
                leftmostNode.Right.Parent = leftmostNode.Parent;
            }

            return true;
        }
    }
}
