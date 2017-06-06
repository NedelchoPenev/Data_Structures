using System;
using System.Collections;
using System.Collections.Generic;

public class ReversedList<T> : IEnumerable<T>
{
    private const int InitialSize = 2;

    private T[] items;

    public ReversedList()
    {
        this.items = new T[InitialSize];
    }

    public int Capacity => this.items.Length;

    public int Count { get; private set; }

    public T this[int index]
    {
        get
        {
            if (index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.items[this.Count - index - 1];
        }

        set
        {
            if (index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.items[this.Count - index - 1] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count == this.Capacity)
        {
            this.Resize();
        }

        this.items[this.Count++] = item;
    }

    private void Resize()
    {
        T[] newArray = new T[this.items.Length * 2];
        Array.Copy(this.items, newArray, this.Count);
        this.items = newArray;
    }

    public T RemoveAt(int index)
    {
        if (index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }

        var reverseIndex = this.Count - index - 1;
        T element = this.items[reverseIndex];
        this.items[reverseIndex] = default(T);
        this.Shift(reverseIndex);
        this.Count--;

        if (this.Count <= this.items.Length / 4)
        {
            this.Shrink();
        }

        return element;
    }

    private void Shrink()
    {
        T[] newArray = new T[this.items.Length / 2];
        Array.Copy(this.items, newArray, this.Count);
        this.items = newArray;
    }

    private void Shift(int index)
    {
        for (int i = index; i < this.Count; i++)
        {
            this.items[i] = this.items[i + 1];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.Count - 1; i >= 0; i--)
        {
            yield return this.items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}