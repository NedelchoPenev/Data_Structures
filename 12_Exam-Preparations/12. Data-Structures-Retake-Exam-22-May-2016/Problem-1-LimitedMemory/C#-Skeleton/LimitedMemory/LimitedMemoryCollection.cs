using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace LimitedMemory
{
    using System;

    public class LimitedMemoryCollection<K, V> : ILimitedMemoryCollection<K, V>
    {
        private LinkedList<Pair<K, V>> byPairs;

        private Dictionary<K, LinkedListNode<Pair<K, V>>> byKey;

        public LimitedMemoryCollection(int capacity)
        {
            this.Capacity = capacity;
            this.byPairs = new LinkedList<Pair<K, V>>();
            this.byKey = new Dictionary<K, LinkedListNode<Pair<K, V>>>();
        }

        public IEnumerator<Pair<K, V>> GetEnumerator()
        {
            return this.byPairs.Reverse().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Capacity { get; private set; }

        public int Count => this.byPairs.Count;

        public void Set(K key, V value)
        {
            var newPair = new Pair<K, V>(key, value);

            if (this.byKey.ContainsKey(key))
            {
                this.byPairs.Remove(this.byKey[key]);
            }
            else if (this.Capacity == this.Count)
            {
                var first = this.byPairs.First.Value.Key;
                this.byPairs.RemoveFirst();
                this.byKey.Remove(first);
            }

            var node = this.byPairs.AddLast(newPair);
            this.byKey[key] = node;
        }

        public V Get(K key)
        {
            if (!this.byKey.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            var current = this.byKey[key];
            this.byPairs.Remove(current);
            this.byPairs.AddLast(current);
            return this.byKey[key].Value.Value;
        }
    }
}