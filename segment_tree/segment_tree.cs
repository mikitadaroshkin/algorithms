using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acm_segment_sharp
{
    static class Writer
    {
        static System.IO.StreamWriter file = new System.IO.StreamWriter("output.txt") { AutoFlush = true };
        public static void Write(long line)
        {
            file.WriteLine(line);
        }
    }

    class Node
    {
        public Node()
        {
            Sum = 0;
            Min = 0;
            Max = 0;
            ValueToAdd = 0;
            NumberOfElements = 0;
        }

        //keys
        public long Sum;
        public long Min;
        public long Max;

        //cache
        public long ValueToAdd;
        public long NumberOfElements;
    };

    class SegmentTree
    {
        int N;
        Node[] nodes;

        void recalc(int v)
        {
            nodes[v - 1].Sum = nodes[2 * v - 1].Sum + nodes[2 * v - 1].ValueToAdd * nodes[2 * v - 1].NumberOfElements + nodes[2 * v].Sum + nodes[2 * v].ValueToAdd * nodes[2 * v].NumberOfElements;
            nodes[v - 1].Min = Math.Min(nodes[2 * v - 1].Min + nodes[2 * v - 1].ValueToAdd, nodes[2 * v].Min + nodes[2 * v].ValueToAdd);
            nodes[v - 1].Max = Math.Max(nodes[2 * v - 1].Max + nodes[2 * v - 1].ValueToAdd, nodes[2 * v].Max + nodes[2 * v].ValueToAdd);
        }

        public SegmentTree(int _N)
        {
            N = _N;
            nodes = new Node[4 * N];
            for (int i = 0; i < 4 * N; i++)
                nodes[i] = new Node();
        }


        public void AddToRange(int v, int l, int r, int tl, int tr, int d)
        {
            if (l == tl && r == tr)
            {
                nodes[v - 1].ValueToAdd += d;
                return;
            }
            else
            {
                int tm = (tl + tr) / 2;

                nodes[2 * v].NumberOfElements = tr - tm;
                nodes[2 * v].ValueToAdd += nodes[v - 1].ValueToAdd;

                nodes[2 * v - 1].NumberOfElements = tm - tl + 1;
                nodes[2 * v - 1].ValueToAdd += nodes[v - 1].ValueToAdd;

                nodes[v - 1].ValueToAdd = 0;

                if (r <= tm)
                    AddToRange(2 * v, l, r, tl, tm, d);
                else if (l > tm)
                    AddToRange(2 * v + 1, l, r, tm + 1, tr, d);
                else
                {
                    AddToRange(2 * v, l, tm, tl, tm, d);
                    AddToRange(2 * v + 1, tm + 1, r, tm + 1, tr, d);
                }
                recalc(v);
            }
        }

        public long GetRangeSum(int v, int l, int r, int tl, int tr)
        {
            long sum_returned;
            if (l == tl && r == tr)
                sum_returned = nodes[v - 1].Sum;
            else
            {
                int tm = (tl + tr) / 2;
                if (r <= tm)
                    sum_returned = GetRangeSum(2 * v, l, r, tl, tm);
                else if (l > tm)
                    sum_returned = GetRangeSum(2 * v + 1, l, r, tm + 1, tr);
                else
                    sum_returned = GetRangeSum(2 * v, l, tm, tl, tm) + GetRangeSum(2 * v + 1, tm + 1, r, tm + 1, tr);
            }
            return sum_returned + nodes[v - 1].ValueToAdd * (r - l + 1);
        }

        public long GetRangeMin(int v, int l, int r, int tl, int tr)
        {
            long min_returned;

            if (l == tl && r == tr)
                min_returned = nodes[v - 1].Min;
            else
            {
                int tm = (tl + tr) / 2;
                if (r <= tm)
                    min_returned = GetRangeMin(2 * v, l, r, tl, tm);
                else if (l > tm)
                    min_returned = GetRangeMin(2 * v + 1, l, r, tm + 1, tr);
                else
                    min_returned = Math.Min(GetRangeMin(2 * v, l, tm, tl, tm), GetRangeMin(2 * v + 1, tm + 1, r, tm + 1, tr));
            }
            return min_returned + nodes[v - 1].ValueToAdd;
        }

        public long GetRangeMax(int v, int l, int r, int tl, int tr)
        {
            long max_returned;

            if (l == tl && r == tr)
                max_returned = nodes[v - 1].Max;
            else
            {
                int tm = (tl + tr) / 2;
                if (r <= tm)
                    max_returned = GetRangeMax(2 * v, l, r, tl, tm);
                else if (l > tm)
                    max_returned = GetRangeMax(2 * v + 1, l, r, tm + 1, tr);
                else
                    max_returned = Math.Max(GetRangeMax(2 * v, l, tm, tl, tm), GetRangeMax(2 * v + 1, tm + 1, r, tm + 1, tr));
            }
            return max_returned + nodes[v - 1].ValueToAdd;
        }

        public void Modify(int i, long x)
        {
            int tl = 1, tr = N, tm = 1, v = 1;

            while (tl < tr)
            {
                tm = (tl + tr) / 2;

                nodes[2 * v].NumberOfElements = tr - tm;
                nodes[2 * v].ValueToAdd += nodes[v - 1].ValueToAdd;

                nodes[2 * v - 1].NumberOfElements = tm - tl + 1;
                nodes[2 * v - 1].ValueToAdd += nodes[v - 1].ValueToAdd;

                nodes[v - 1].ValueToAdd = 0;
                nodes[v - 1].NumberOfElements = tr - tl + 1;

                v *= 2;
                if (i < tm)
                    tr = tm;
                else
                {
                    ++v;
                    tl = tm + 1;
                }
            }

            nodes[v - 1].Sum = nodes[v - 1].Min = nodes[v - 1].Max = x;
            nodes[v - 1].ValueToAdd = 0;

            tm = v / 2;

            while (tm > 0)
            {
                recalc(tm);
                tm /= 2;
            }

            return;
        }
    };

    class Program
    {
        static void Main(string[] args)
        {

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");

            int N = Int32.Parse(file.ReadLine());

            String[] tmparr;
            int cmd;
            int a, b, i, val;
            int INITIAL_V = 1, INITIAL_LEFT = 1, INITIAL_RIGHT = N;
            SegmentTree tree = new SegmentTree(N);
            while ((line = file.ReadLine()) != null)
            {
                tmparr = line.Split(' ');
                cmd = Int32.Parse(tmparr[0]);
                switch (cmd)
                {
                    case 0:
                        return;
                    case 1:
                        i = Int32.Parse(tmparr[1]);
                        val = Int32.Parse(tmparr[2]);
                        tree.Modify(i, val);
                        break;
                    case 2:
                        a = Int32.Parse(tmparr[1]) + 1;
                        b = Int32.Parse(tmparr[2]) + 1;
                        val = Int32.Parse(tmparr[3]);
                        tree.AddToRange(INITIAL_V, a, b, 1, N, val);
                        break;
                    case 3:
                        a = Int32.Parse(tmparr[1]) + 1;
                        b = Int32.Parse(tmparr[2]) + 1;
                        Writer.Write(tree.GetRangeSum(INITIAL_V, a, b, INITIAL_LEFT, INITIAL_RIGHT));
                        break;
                    case 4:
                        a = Int32.Parse(tmparr[1]) + 1;
                        b = Int32.Parse(tmparr[2]) + 1;
                        Writer.Write(tree.GetRangeMin(INITIAL_V, a, b, INITIAL_LEFT, INITIAL_RIGHT));
                        break;
                    case 5:
                        a = Int32.Parse(tmparr[1]) + 1;
                        b = Int32.Parse(tmparr[2]) + 1;
                        Writer.Write(tree.GetRangeMax(INITIAL_V, a, b, INITIAL_LEFT, INITIAL_RIGHT));
                        break;
                    default:
                        break;
                }
            }


            return;
        }
    }
}