using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

    private static int searchedSum;

    static void Main(string[] args)
    {
        ReadTree();
        var root = GetRootNode();
        Console.WriteLine("Subtrees of sum {0}:", searchedSum);

        foreach (var subtree in GetSubtrees(root))
        {
            PrintPreOrder(subtree);
            Console.WriteLine();
        }
        
    }

    private static void PrintPreOrder(Tree<int> node)
    {
        Console.Write(node.Value + " ");
        foreach (var child in node.Children)
        {
            PrintPreOrder(child);
        }
    }

    static Tree<int> GetTreeNodeByValue(int value)
    {
        if (!nodeByValue.ContainsKey(value))
        {
            nodeByValue[value] = new Tree<int>(value);
        }

        return nodeByValue[value];
    }

    private static void AddEdge(int parent, int child)
    {
        Tree<int> parrentNode = GetTreeNodeByValue(parent);
        Tree<int> childNode = GetTreeNodeByValue(child);

        parrentNode.Children.Add(childNode);
        childNode.Parent = parrentNode;
    }

    static void ReadTree()
    {
        int nodeCount = int.Parse(Console.ReadLine());
        for (int i = 1; i < nodeCount; i++)
        {
            string[] edge = Console.ReadLine().Split(' ');
            AddEdge(int.Parse(edge[0]), int.Parse(edge[1]));
        }

        searchedSum = int.Parse(Console.ReadLine());
    }

    static Tree<int> GetRootNode()
    {
        return nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
    }

    static List<Tree<int>> FindLeafs()
    {
        return nodeByValue.Values.Where(n => n.Children.Count == 0).ToList();
    }

    static void PrintMiddleNodes()
    {
        var nodes = nodeByValue.Values.Where(x => x.Parent != null && x.Children.Count != 0).Select(x => x.Value)
            .OrderBy(x => x).ToList();

        Console.WriteLine("Middle nodes: " + string.Join(" ", nodes));
    }

    static List<int> FindDeepestNode()
    {
        var leafs = FindLeafs();
        int depth = 0;
        Tree<int> deepestLeaf = null;
        var result = new List<int>();

        foreach (var leaf in leafs)
        {
            int currentDepth = 0;
            var parent = leaf.Parent;
            while (parent != null)
            {
                currentDepth++;
                parent = parent.Parent;
            }

            if (currentDepth > depth)
            {
                depth = currentDepth;
                deepestLeaf = leaf;
            }
        }

        while (deepestLeaf != null)
        {
            result.Add(deepestLeaf.Value);
            deepestLeaf = deepestLeaf.Parent;
        }

        result.Reverse();
        return result;
    }

    static List<List<int>> FindPathWithGivenSum()
    {

        var result = new List<List<int>>();
        var stack = new Stack<Tree<int>>();
        stack.Push(GetRootNode());

        while (stack.Count != 0)
        {
            var current = stack.Pop();

            foreach (var child in current.Children)
            {
                stack.Push(child);
            }

            if (current.Children.Count == 0)
            {
                List<int> currentList = new List<int>();
                var parent = current;
                while (parent != null)
                {
                    currentList.Add(parent.Value);
                    parent = parent.Parent;
                }
                currentList.Reverse();

                if (searchedSum == currentList.Sum())
                {
                    result.Add(currentList);
                }
            }
        }

        result.Reverse();
        return result;
    }


    private static List<Tree<int>> GetSubtrees(Tree<int> root)
    {
        var result = new List<Tree<int>>();

        var sum = FindSubtreeDFS(root, result, 0);

        return result;
    }

    private static int FindSubtreeDFS(Tree<int> node, List<Tree<int>> currentList, int current)
    {
        if (node == null)
        {
            return 0;
        }

        current = node.Value; 

        foreach (var rootChild in node.Children)
        {
            current += FindSubtreeDFS(rootChild, currentList, current);
        }

        if (searchedSum == current)
        {
            currentList.Add(node);
        }

        return current;
    }
}