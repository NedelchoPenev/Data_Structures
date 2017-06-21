using System;

public class KdTree
{
    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(Point2D point)
    {
        return this.Contains(this.root, point, 0) != null;
    }

    private Node Contains(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return null;
        }

        int compare = this.Compare(node.Point, point, depth);
        if (compare > 0)
        {
            return this.Contains(node.Left, point, depth + 1);
        }
        else if (compare < 0)
        {
            return this.Contains(node.Right, point, depth + 1);
        }

        return node;
    }

    public void Insert(Point2D point)
    {
        this.root = this.Insert(this.root, point, 0);
    }

    private Node Insert(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return new Node(point);
        }

        var compare = this.Compare(node.Point, point, depth);
        if (compare > 0)
        {
            node.Left = this.Insert(node.Left, point, depth + 1);
        }
        else if (compare < 0)
        {
            node.Right = this.Insert(node.Right, point, depth + 1);
        }

        return node;
    }

    private int Compare(Point2D leftPoint, Point2D rightPoint, int depth)
    {
        int compare = 0;
        if (depth % 2 == 0)
        {
            compare = leftPoint.X.CompareTo(rightPoint.X);
            if (compare == 0)
            {
                compare = leftPoint.Y.CompareTo(rightPoint.Y);
            }
        }
        else
        {
            compare = leftPoint.Y.CompareTo(rightPoint.Y);
            if (compare == 0)
            {
                compare = leftPoint.X.CompareTo(rightPoint.X);
            }
        }

        return compare;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }
}
