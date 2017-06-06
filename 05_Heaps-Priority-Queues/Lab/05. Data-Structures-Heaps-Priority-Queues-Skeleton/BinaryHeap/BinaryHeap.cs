using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.Count - 1);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && this.IsGreater(index, this.Parent(index)))
        {
            this.Swap(index, this.Parent(index));
            index = this.Parent(index);
        }
    }

    private void Swap(int index, int parent)
    {
        var temp = this.heap[parent];
        this.heap[parent] = this.heap[index];
        this.heap[index] = temp;
    }

    private bool IsGreater(int index, int parent)
    {
        return this.heap[index].CompareTo(this.heap[parent]) > 0;
    }

    private int Parent(int index)
    {
        return (index - 1) / 2;
    }

    public T Peek()
    {
        return this.heap[0];
    }

    public T Pull()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        T item = this.heap[0];
        this.Swap(0, this.heap.Count - 1);
        this.heap.RemoveAt(this.heap.Count - 1);
        this.HeapifyDown(0);

        return item;
    }

    private void HeapifyDown(int index)
    {
        while (index < this.heap.Count / 2)
        {
            int child = this.Left(index);
            if (this.HasChild(child + 1) && this.IsGreater(child + 1, child))
            {
                child = child + 1;
            }

            if (this.IsGreater(index, child))
            {
                break;
            }

            this.Swap(child, index);
            index = child;
        }
    }

    private bool HasChild(int index)
    {
        return this.heap.Count > index + 1;
    }

    private int Left(int index)
    {
        return index + 1;
    }
}
