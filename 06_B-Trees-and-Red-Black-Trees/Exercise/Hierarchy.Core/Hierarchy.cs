using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Hierarchy<T> : IHierarchy<T>
{
    private Dictionary<T, Node> nodesByValue;

    private Node root;

    private class Node
    {
        public T Value { get; set; }

        public Node Parent { get; set; }

        public List<Node> Children { get; set; }

        public Node(T value)
        {
            this.Value = value;
            this.Children = new List<Node>();
        }
    }

    public Hierarchy(T root)
    {
        this.root = new Node(root);
        this.nodesByValue = new Dictionary<T, Node>();
        this.nodesByValue.Add(root, this.root);
    }

    public int Count
    {
        get
        {
            return this.nodesByValue.Count;
        }
    }

    public void Add(T element, T child)
    {
        if (!this.Contains(element) || this.Contains(child))
        {
            throw new ArgumentException();
        }

        Node parent = this.nodesByValue[element];
        Node childNode = new Node(child) { Parent = parent };
        parent.Children.Add(childNode);
        this.nodesByValue.Add(child, childNode);
    }

    public void Remove(T element)
    {
        if (!this.nodesByValue.ContainsKey(element))
        {
            throw new ArgumentException();
        }
        Node nodeToRemove = this.nodesByValue[element];
        if (nodeToRemove.Equals(this.root))
        {
            throw new ArgumentException();
        }

        List<Node> children = nodeToRemove.Children;
        Node parent = nodeToRemove.Parent;
        parent.Children.Remove(nodeToRemove);
        foreach (Node child in children)
        {
            child.Parent = parent;
            parent.Children.Add(child);
        }

        this.nodesByValue.Remove(element);
    }

    public IEnumerable<T> GetChildren(T item)
    {
        if (!this.Contains(item))
        {
            throw new ArgumentException();
        }

        Node parent = this.nodesByValue[item];
        List<Node> children = parent.Children;
        return parent.Children.Select(n => n.Value).ToList();
    }

    public T GetParent(T item)
    {
        if (!this.Contains(item))
        {
            throw new ArgumentException();
        }

        if (this.root.Value.Equals(item))
        {
            return default(T);
        }
        return this.nodesByValue[item].Parent.Value;
    }

    public bool Contains(T value)
    {
        return this.nodesByValue.ContainsKey(value);
    }

    public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
    {
        return this.nodesByValue.Keys.ToList().Intersect(other.nodesByValue.Keys.ToList());
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (this.root == null)
        {
            throw new ArgumentException();
        }
        return this.OrderBFS();
    }

    private IEnumerator<T> OrderBFS()
    {
        List<T> result = new List<T> { this.root.Value };
        Queue<List<Node>> nodeQueue = new Queue<List<Node>>();
        nodeQueue.Enqueue(this.root.Children);

        while (nodeQueue.Count != 0)
        {
            List<Node> currentChildren = nodeQueue.Dequeue();
            foreach (Node child in currentChildren)
            {
                result.Add(child.Value);

                if (child.Children.Count > 0)
                {
                    nodeQueue.Enqueue(child.Children);
                }
            }
        }

        return result.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}