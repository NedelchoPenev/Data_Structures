using System;
using System.Collections.Generic;

public class KdTree
{
    private Rectangle galagySize;

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

    public KdTree(Rectangle galagySize)
    {
        this.galagySize = galagySize;
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

    private int Compare(Point2D nodePoint, Point2D point, int depth)
    {
        int compare = 0;
        if (depth % 2 == 0)
        {
            compare = nodePoint.X.CompareTo(point.X);
            if (compare == 0)
            {
                compare = nodePoint.Y.CompareTo(point.Y);
            }
        }
        else
        {
            compare = nodePoint.Y.CompareTo(point.Y);
            if (compare == 0)
            {
                compare = nodePoint.X.CompareTo(point.X);
            }
        }

        return compare;
    }

    public int RangeSearch(Rectangle searchedPart)
    {
        HashSet<Node> result = new HashSet<Node>();

        this.RangeSearch(this.root, searchedPart, this.galagySize, 0, result);

        return result.Count;
    }

    private void RangeSearch(
        Node node,
        Rectangle searchedPart,
        Rectangle visibleGalaxy,
        int depth,
        HashSet<Node> result)
    {
        if (node == null)
        {
            return;
        }

        bool sideToMove = this.CheckSide(depth);
        Rectangle firstSide;
        Rectangle secondSide;

        if (sideToMove)
        {
            firstSide = new Rectangle(visibleGalaxy.X1, visibleGalaxy.Y1, node.Point.X, visibleGalaxy.Y2);
            secondSide = new Rectangle(node.Point.X, visibleGalaxy.Y1, visibleGalaxy.X2, visibleGalaxy.Y2);
        }
        else
        {
            firstSide = new Rectangle(visibleGalaxy.X1, visibleGalaxy.Y1, visibleGalaxy.X2, node.Point.Y);
            secondSide = new Rectangle(visibleGalaxy.X1, node.Point.Y, visibleGalaxy.X2, visibleGalaxy.Y2);
        }

        if (firstSide.Intersects(searchedPart))
        {
            if (searchedPart.IsInside(node.Point))
            {
                result.Add(node);
            }
            this.RangeSearch(node.Left, searchedPart, firstSide, depth + 1, result);
        }

        if (secondSide.Intersects(searchedPart))
        {
            if (searchedPart.IsInside(node.Point))
            {
                result.Add(node);
            }
            this.RangeSearch(node.Right, searchedPart, secondSide, depth + 1, result);
        }
    }

    private bool CheckSide(int depth)
    {
        return depth % 2 == 0;
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