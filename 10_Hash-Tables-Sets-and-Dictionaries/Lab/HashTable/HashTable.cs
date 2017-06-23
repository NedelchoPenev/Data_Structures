using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int InitialCapacity = 16;

    private const float LoadFactor = 0.75f;

    private LinkedList<KeyValue<TKey, TValue>>[] slots; 

    public int Count { get; private set; }

    public int Capacity
    {
        get
        {
            return this.slots.Length;
        }
    }

    public HashTable(int capacity = InitialCapacity)
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
    }

    public void Add(TKey key, TValue value)
    {
        this.GrowIfNeeded();
        int slotNumber = this.FindSlotNumber(key);
        if (this.slots[slotNumber] == null)
        {
            this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var element in this.slots[slotNumber])
        {
            if (element.Key.Equals(key))
            {
                throw new ArgumentException();
            }
        }

        var newElement = new KeyValue<TKey, TValue>(key, value);
        this.slots[slotNumber].AddLast(newElement);
        this.Count++;
    }

    private int FindSlotNumber(TKey key)
    {
        var slotNumber = Math.Abs(key.GetHashCode()) % this.slots.Length;
        return slotNumber;
    }

    private void GrowIfNeeded()
    {
        if ((float) (this.Count + 1) / this.Capacity > LoadFactor)
        {
            this.Grow();
        }
    }

    private void Grow()
    {
        var newHashTable = new HashTable<TKey, TValue>(this.Capacity * 2);
        foreach (var keyValue in this)
        {
            newHashTable.Add(keyValue.Key, keyValue.Value);
        }

        this.slots = newHashTable.slots;
        this.Count = newHashTable.Count;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        this.GrowIfNeeded();
        int slotNumber = this.FindSlotNumber(key);
        if (this.slots[slotNumber] == null)
        {
            this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var element in this.slots[slotNumber])
        {
            if (element.Key.Equals(key))
            {
                element.Value = value;
                return false;
            }
        }

        var newElement = new KeyValue<TKey, TValue>(key, value);
        this.slots[slotNumber].AddLast(newElement);
        this.Count++;
        return true;
    }

    public TValue Get(TKey key)
    {
        var item = this.Find(key);
        if (item == null)
        {
            throw new KeyNotFoundException();
        }

        return item.Value;
    }

    public TValue this[TKey key]
    {
        get
        {
            return this.Get(key);
        }
        set
        {
            this.AddOrReplace(key, value);
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var item = this.Find(key);
        if (item == null)
        {
            value = default(TValue);
            return false;
        }

        value = item.Value;
        return true;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        int slotNumber = this.FindSlotNumber(key);
        var element = this.slots[slotNumber];

        if (element != null)
        {
            foreach (var keyValue in element)
            {
                if (keyValue.Key.Equals(key))
                {
                    return keyValue;
                }
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
    {
        return this.Find(key) != null;
    }

    public bool Remove(TKey key)
    {
        var slotNumber = this.FindSlotNumber(key);
        var element = this.slots[slotNumber];

        if (element != null)
        {
            foreach (var keyValue in element)
            {
                if (keyValue.Key.Equals(key))
                {
                    element.Remove(keyValue);
                    this.Count--;
                    return true;
                }
            }
        }

        return false;
    }

    public void Clear()
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[InitialCapacity];
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            return this.Select(e => e.Key);
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            return this.Select(e => e.Value);
        }
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var item in this.slots)
        {
            if (item != null)
            {
                foreach (var keyValue in item)
                {
                    yield return keyValue;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
