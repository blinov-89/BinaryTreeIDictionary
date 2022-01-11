using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BinaryTreeIDictionary
{
    [Serializable]
    public class BinaryTreeDictionary<Tkey, Tvalue> : IDictionary<Tkey, Tvalue>
        where Tkey : IComparable
    {
        private BinaryTree<Tkey, Tvalue> tree = new BinaryTree<Tkey, Tvalue>();


        public Tvalue this[Tkey key]
        {
            get => tree.Find(key).Value;
            set
            {
                var keyValuePair = tree.Find(key);
                if (keyValuePair.Equals(new KeyValuePair<Tkey, Tvalue>()))
                    Add(key, value);
                else
                {
                    tree.Remove(keyValuePair.Key);
                    tree.Add(new KeyValuePair<Tkey, Tvalue>(key, value));
                }
            }
        }

        public ICollection<Tkey> Keys => tree.Traverse().Select(keyValuePair => keyValuePair.Key).ToList();

        public ICollection<Tvalue> Values => tree.Traverse().Select(keyValuePair => keyValuePair.Value).ToList();

        public int Count => tree.Traverse().Count();

        public bool IsReadOnly => false;

        public void Add(Tkey key, Tvalue value)
        {
            var KeyValuePair = new KeyValuePair<Tkey, Tvalue>(key, value);
            tree.Add(KeyValuePair);
        }

        public void Add(KeyValuePair<Tkey, Tvalue> item)
        {
            tree.Add(item);
        }

        public void Clear()
        {
            tree = new BinaryTree<Tkey, Tvalue>();
        }

        public bool Contains(KeyValuePair<Tkey, Tvalue> item)
        {
            var currentKeyValuePair = tree.Find(item.Key);
            return !currentKeyValuePair.Equals(new KeyValuePair<Tkey, Tvalue>()) && currentKeyValuePair.Value.Equals(item.Value);
        }

        public bool ContainsKey(Tkey key)
        {
            var currentKeyValuePair = tree.Find(key);
            return !currentKeyValuePair.Equals(new KeyValuePair<Tkey, Tvalue>());
        }

        public void CopyTo(KeyValuePair<Tkey, Tvalue>[] array, int arrayIndex)
        {
            if (array.Length < arrayIndex + Count)
            {
                throw new ArgumentException(
                    "The length of the current array is not enough to copy the elements of the collection!");
            }
            foreach (var keyValuePair in tree.Traverse())
            {
                array[arrayIndex] = keyValuePair;
                arrayIndex++;
            }
        }

        public bool Remove(Tkey key)
        {
            return tree.Remove(key);
        }

        public bool Remove(KeyValuePair<Tkey, Tvalue> item)
        {
            return tree.Remove(item.Key);
        }

        public bool TryGetValue(Tkey key, [MaybeNullWhen(false)] out Tvalue value)
        {
            var keyValuePair = tree.Find(key);
            value = default;
            if (!keyValuePair.Equals(new KeyValuePair<Tkey, Tvalue>()))
            {
                value = keyValuePair.Value;
                return true;
            }
            return false;
        }

        public IEnumerator<KeyValuePair<Tkey, Tvalue>> GetEnumerator()
        {
            return tree.Traverse()
                .Select(keyValuePair => keyValuePair)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
