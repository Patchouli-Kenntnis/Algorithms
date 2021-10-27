using System;
using System.Diagnostics;
namespace Algorithms
{
    class BinaryHeap
    {
        StackByResizeableArray<int> stack;// where the data is stored
        //Left child of stack[i]: stack[2*i]
        //Right child of stack[i]: stack[2*i+1]
        BinaryHeap(int root)
        {
            stack = new StackByResizeableArray<int>();
            stack.Push(0);
            stack.Push(root);
        }
        void Exch(int a, int b)// exchange the stack[a] and stack[b]
        {
            int i = stack[a];
            stack[a] = stack[b];
            stack[b] = i;
        }
        public int Top()// return the largest key
        {
            return stack[1];//The root must be the largest key
        }
        public void Add(int newKey)//Add newKey to the heap
        {
            int i = stack.Length();// index of the new key
            stack.Push(newKey);//add the into the 
            while (i >= 2)
            {
                if (stack[i] > stack[i / 2])// The left child never smaller than the right one
                    //Thus exchange the node with its left child
                {
                    Exch(i, i / 2);
                    i /= 2;
                }
                else
                {
                    break;
                }
            }
            Debug.Assert(isVerified());
            
        }
        public int Size => stack.Length() - 1;
        public int RemoveLargest()
        {
            int ret = stack[1];//[1] is the root node
            Exch(1, Size);
            stack.Pop();
            int i = 1;//index of the item which will be sunk
            while ((2 * i) <= Size)//validate left child
            {
                int child = 2 * i;//default: left child
                if (child + 1 <= Size)//Right child validation
                {
                    if (stack[child] < stack[child + 1])
                    {
                        ++child;// if right child is bigger than left, set child to the right
                    }
                }
                if (stack[child] > stack[i])
                {
                    Exch(child, i);
                    i = child;
                }
                else
                {
                    break;
                }
            }
            Debug.Assert(isVerified());
            return ret;
        }
        public void Show()
        {
            for (int i = 0; i != stack.Length(); ++i)
            {
                Console.Write(stack[i] + " ");
            }
            Console.WriteLine();
        }
        public int[] ToArray()
        {
            var array = new int[stack.Length()];
            for (int i = 0; i != stack.Length(); ++i)
            {
                array[i] = stack[i];
            }
            array[0] = int.MaxValue;
            return array;
        }
        private bool isVerified()
        {
            //Check the invariant of heap: any node except the root must smaller than its parent node
            bool isVerified = true;
            if (Size == 1) return true;
            for (int i = 2; i <= Size; ++i)
            {
                if (stack[i] > stack[i / 2])
                {
                    isVerified = false;
                    break;
                }
            }
            return isVerified;
        }
        //Demo: add and remove
        public static void Demo()
        {
            var heap = new BinaryHeap(50);
            int[] a = { 83, 25, 4, 92, 80, 70, 35, 29, 16, 35 };
            foreach (var i in a)
            {
                heap.Add(i);
            }
            heap.Show();


            Console.WriteLine(heap.isVerified());
            for (int i = 0; i != 5; ++i)
            {
                heap.RemoveLargest();
                heap.Show();
                Console.WriteLine(heap.isVerified());
            }
        }

    }
}
