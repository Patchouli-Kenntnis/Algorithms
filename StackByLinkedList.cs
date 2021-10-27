using System;
namespace Algorithms
{
    public class StackByLinkedList<T>
    {
        public class node<T1>
        {
            public T1 value;
            public node<T1> next;
            public node(T1 newItem, node<T1> nextnode)
            {
                value = newItem;
                next = nextnode;
            }
        }
        private node<T> top;
        public void Push(T newstr) //add a new node
        {
            var newHead = new node<T>(newstr, top);
            top = newHead;
        }
        public T Top()
        {
            return top.value;
        }
        public T Pop() //remove the item which is latest added and return that item
        {
            if (top != null)
            {
                var ret = top.value;
                top = top.next; //The removed item will be GCed
                return ret;
            }
            else
            {
                return default; //Return null if the stack is empty
            }
        }
        public bool IsEmpty()
        {
            return top == null;
        }
        public static void Demo()        //Demo of "Stack By Linked List"
        {
            var s = new StackByLinkedList<int>();
            for (int i = 0; i != 8; ++i)
            {
                s.Push(i + 1);
            }
            for (int i = 0; i != 8; ++i)
            {
                Console.WriteLine(Convert.ToString(s.Top()));
                s.Pop();
            }
        }
    }
}