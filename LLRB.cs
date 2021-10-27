using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace Algorithms
{
    //Left leaning Red Black Tree
    //A modification 
    public partial class LLRB<KeyType, ValueType> where KeyType : IComparable
    {
        private LLRBNode root;
        public LLRB(KeyType key, ValueType value)
        {
            root = new LLRBNode(key, value);
            root.IsRed = false;
        }
        private LLRBNode PutNode(KeyType key, ValueType value, LLRBNode node)
        {
            if (node == null)
            {
                return new LLRBNode(key, value);
            }

            int comp = node.Key.CompareTo(key);
            if (comp == -1)
            {
                node.RChild = PutNode(key, value, node.RChild);
            }
            else if (comp == 0)
            {
                node.Value = value;
            }
            else
            {
                node.LChild = PutNode(key, value, node.LChild);
            }
            //Maintain an R-B tree
            if (isRed(node.RChild) && !isRed(node.LChild)) //Right red, left black: rotate left
                node = rotateLeft(node);
            if (isRed(node.LChild) && isRed(node.LChild.LChild)) //two left edges are red
                node = rotateRight(node);
            if (isRed(node.LChild) && isRed(node.RChild))
                flipColors(node);
            return node;
        }
        public void Put(KeyType key, ValueType value) => PutNode(key, value, root);

        public ValueType Find(KeyType key) => FindFromNode(key, root);// If the node with key exists, return the corresponding value; else return null
        private ValueType FindFromNode(KeyType key, LLRBNode node)
        {
            if (node == null)
            {
                return default;// return null if search misses
            }

            int comp = node.Key.CompareTo(key);
            switch (comp)
            {
                case -1:// node.key < key
                    return FindFromNode(key, node.RChild);
                case 0:// node.key == key
                    return node.Value;
                default:// node.key > key
                    return FindFromNode(key, node.LChild);
            }
        }
        public ValueType Delete(KeyType key) =>
            // TODO: delete the node with key and return the corresponding value
            throw new NotImplementedException();//The process is a bit difficult, it will be implemented later
        private bool isRed(LLRBNode node)
        {
            if (node == null) return false;// edge to null nodes is regarded as black
            if (node == root) return false;
            return node.IsRed;
        }
        private LLRBNode rotateLeft(LLRBNode h)// If oldroot contains a red RChild,
                                               // make the RChild the new root and reset color
        {
            //Console.WriteLine(h.Value + " RotateLeft");
            Debug.Assert(isRed(h.RChild));
            var x = h.RChild;
            h.RChild = x.LChild;
            x.LChild = h;
            x.IsRed = h.IsRed;
            h.IsRed = true;
            if (root == h) root = x;
            return x;
        }
        private LLRBNode rotateRight(LLRBNode h)// Symmetric to rotateLeft
        {
            //Console.WriteLine(h.Value + " RotateRight");
            Debug.Assert(isRed(h.LChild));
            var x = h.LChild;

            h.LChild = x.RChild;
            x.RChild = h;
            x.IsRed = h.IsRed;
            h.IsRed = true;
            if (root == h) root = x;
            return x;
        }
        private void flipColors(LLRBNode node)
        {
            //Console.WriteLine(node.Value + " Flip");
            Debug.Assert(!isRed(node));
            Debug.Assert(isRed(node.LChild));
            Debug.Assert(isRed(node.RChild));
            node.IsRed = true;
            node.LChild.IsRed = false;
            node.RChild.IsRed = false;
        }
        public void ShowBFS()
        {
            Queue<LLRBNode> nodes = new Queue<LLRBNode>();
            nodes.Enqueue(root);
            while (nodes.Count != 0)
            {
                var n = nodes.Dequeue();
                if (n.LChild != null) nodes.Enqueue(n.LChild);
                if (n.RChild != null) nodes.Enqueue(n.RChild);
                Console.Write(n.Value + "," + n.IsRed + "|");
            }
        }
        public static void Demo()
        {
            var rng = new Random();
            int p = rng.Next(10);
            var llrb = new LLRB<int, string>(p, p + "Jews");
            //llrb.ShowBFS();
            //Console.WriteLine();
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i != 100000; ++i)
            {
                int q = rng.Next(int.MaxValue);
                //Console.Write(q + ">>>");
                llrb.Put(q, q + "Jews");
                //llrb.ShowBFS();
                //Console.WriteLine();
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}

