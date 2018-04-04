using System;
using System.Collections.Generic;

namespace acmsharp
{
    static class Writer
    {
        static System.IO.StreamWriter file = new System.IO.StreamWriter("tst.out") { AutoFlush = true };
        public static void Write(int line)
        {
            file.WriteLine(line);
        }
    }
    public class Node
    {
        public Node(int key)
        {
            Key = key;
            Left = null;
            Right = null;
            MaxPathLen = 0;
        }
        public int Height;
        public int Key;
        public int MaxPathLen;
        public Node Father;
        public Node Left;
        public Node Right;
    }

    class Tree : IComparer<Node>
    {
        public Node Root;
        public Tree()
        {
            Root = null;
        }
        public void Insert(int key)
        {
            Root = InnerInsert(Root, key);
        }
        public void PreorderTraversal()
        {
            InnerPreorderTraversal(Root);
        }
        public void Delete(int key)
        {
            Root = InnerDelete(Root, key);
        }
        void InnerPreorderTraversal(Node node)
        {
            if (node == null)
                return;
            Writer.Write(node.Key);
            InnerPreorderTraversal(node.Left);
            InnerPreorderTraversal(node.Right);
        }
        Node GetLowest(Node node)
        {
            if (node.Left == null)
                return node;
            return GetLowest(node.Left);
        }
        Node InnerInsert(Node node, int key)
        {
            if (node == null)
            {
                return new Node(key);
            }

            if (key < node.Key)
            {
                node.Left = InnerInsert(node.Left, key);
                node.Left.Father = node;
            }
            else if (key > node.Key)
            {
                node.Right = InnerInsert(node.Right, key);
                node.Right.Father = node;
            }
            return node;
        }
        Node InnerDelete(Node node, int key)
        {
            if (node == null)
                return null;
            if (key < node.Key)
                node.Left = InnerDelete(node.Left, key);
            else if (key > node.Key)
                node.Right = InnerDelete(node.Right, key);
            else
            {
                if (node.Left == null)
                    node = node.Right;
                else if (node.Right == null)
                    node = node.Left;
                else
                {
                    int rightestKey = GetLowest(node.Right).Key;
                    node.Key = rightestKey;
                    node.Right = InnerDelete(node.Right, rightestKey);
                }
            }
            return node;
        }


        public int Compare(Node a, Node b)
        {
            if (a.Key == b.Key)
                return 0;
            int aSum = FindMinsOfPath(a);
            int bSum = FindMinsOfPath(b);

            if (aSum < bSum || aSum == bSum && a.Key < b.Key)
                return -1;

            return 1;
        }
        public List<Node> SetMarks()
        {
            List<Node> maxNodes = new List<Node>()
            {
                new Node(0)
                {
                    MaxPathLen = -1
                }
            };
            InnerSetMarks(Root, ref maxNodes);
            return maxNodes;
        }
        public int FindMinsOfPath(Node node)
        {
            Node a = node.Left == null ? null : InnerMaxHeightWalk(node.Left);
            Node b = node.Right == null ? null : InnerMaxHeightWalk(node.Right);
            if (a == null)
                return node.Key + b.Key;
            if (b == null)
                return node.Key + a.Key;
            else
            {
                int diffa = a.Key - a.Father.Key;
                int diffb = b.Key - b.Father.Key;
                if (diffa < diffb)
                    b = b.Father;
                else
                    a = a.Father;
                return a.Key + b.Key;
            }
        }
        public void SortPaths(List<Node> maxNodes)
        {

        }

        public Node InnerMaxHeightWalk(Node node, ref List<int> path)
        {
            path.Add(node.Key);
            if (node.Height == 0)
                return node;
            return InnerMaxHeightWalk(node.Left != null && (node.Right == null || node.Left.Height >= node.Right.Height) ? node.Left : node.Right, ref path);
        }
        public Node InnerMaxHeightWalk(Node node)
        {
            if (node.Height == 0)
                return node;
            return InnerMaxHeightWalk(node.Left != null && (node.Right == null || node.Left.Height >= node.Right.Height) ? node.Left : node.Right);
        }
        int InnerSetMarks(Node node, ref List<Node> maxNodes)
        {
            if (node == null)
                return -1;
            int leftHeight = InnerSetMarks(node.Left, ref maxNodes);
            int rightHeight = InnerSetMarks(node.Right, ref maxNodes);
            node.Height = leftHeight > rightHeight ? leftHeight : rightHeight;
            node.Height++;

            if (node.Left != null)
                node.MaxPathLen += node.Left.Height + 1;
            if (node.Right != null)
                node.MaxPathLen += node.Right.Height + 1;
            if (node.Left != null && node.Right != null)
                node.MaxPathLen--;
            if (node.MaxPathLen > maxNodes[0].MaxPathLen)
            {
                maxNodes.Clear();
                maxNodes.Add(node);
            }
            else if (node.MaxPathLen == maxNodes[0].MaxPathLen)
                maxNodes.Add(node);

            return node.Height;
        }

        public void DeleteMid(Node node)
        {
            if (node.MaxPathLen < 1 || (node.MaxPathLen + 1) % 2 == 0)
                return;
            List<int> path = new List<int>();
            path.Add(node.Key);

            if (node.Left == null)
                InnerMaxHeightWalk(node.Right, ref path);
            else if (node.Right == null)
                InnerMaxHeightWalk(node.Left, ref path);
            else
            {
                Node a = InnerMaxHeightWalk(node.Left, ref path);
                Node b = InnerMaxHeightWalk(node.Right, ref path);
                int diffa = a.Key - a.Father.Key;
                int diffb = b.Key - b.Father.Key;
                if (diffa < diffb)
                    path.Remove(b.Key);
                else if (diffa > diffb)
                    path.Remove(a.Key);
                else
                {
                    path.Remove(a.Key);
                    path.Sort();
                    int aMid = path[path.Count / 2];
                    path.Add(a.Key);
                    path.Remove(b.Key);
                    path.Sort();
                    int bMid = path[path.Count / 2];
                    if (aMid != bMid)
                        return;
                }
            }

            if (path.Count % 2 == 1)
            {
                path.Sort();
                Delete(path[path.Count / 2]);
            }
        }
    };



    class Program
    {
        static void Main(string[] args)
        {
            string line;
            Tree tree = new Tree();
            System.IO.StreamReader file = new System.IO.StreamReader("tst.in");

            while ((line = file.ReadLine()) != null)
            {
                tree.Insert(Int32.Parse(line));
            }
            List<Node> maxNodes = tree.SetMarks();
            if (tree.Root.Height > 0)
            {
                Node maxNode = maxNodes[0];
                int maxNodeSum = tree.FindMinsOfPath(maxNodes[0]);
                if (maxNodes.Count > 1)
                {
                    int aSum = 0;
                    for (int i = 1; i < maxNodes.Count; i++)
                    {
                        aSum = tree.FindMinsOfPath(maxNodes[i]);
                        if (aSum < maxNodeSum || aSum == maxNodeSum && maxNodes[i].Key < maxNode.Key)
                        {
                            maxNode = maxNodes[i];
                            maxNodeSum = aSum;
                        }
                    }
                }

                tree.DeleteMid(maxNode);
            }
            tree.PreorderTraversal();
        }
    }
}