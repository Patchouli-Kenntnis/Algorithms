using System;
using System.IO;
namespace Algorithms
{
    public class UFset
    {
        public int[] id = new int[1024];
        public int[] weight = new int[1024];
        public UFset()
        {
            for (int i = 0; i != id.Length; ++i)
            {
                id[i] = i;
                weight[i] = 1;
            }
        }
        public bool IsConnected(int p, int q)
        {
            return Root(p) == Root(q);// have common root:connected
        }
        public int Root(int p)
        {
            int i = p;
            while (id[i] != i)
            {
                id[i] = id[id[i]];//Path compression: set id[i] to its grandfather node, thus reduce tree height
                i = id[i];
            }
            return i;

        }
        public void Merge(int p, int q)//merge p as a subnode of q
        {
            var pr = Root(p);
            var qr = Root(q);
            if (weight[pr] > weight[qr]) //always merge nodes with smaller weight to one with larger weight
                                         //this optimization makes the tree more flat
            {
                id[qr] = pr;
                weight[pr] += weight[qr];
            }
            else
            {
                id[pr] = qr;
                weight[qr] += weight[pr];
            }
        }
        static void Demo(string[] args)
        {
            var uf = new UFset();
            uf.Merge(1, 3);
            uf.Merge(3, 7);
            uf.Merge(1, 5);
            Console.WriteLine(uf.IsConnected(1, 8));
            Console.WriteLine(uf.IsConnected(5, 7));
            Console.WriteLine(uf.weight[3]);
        }
    }
}
  
