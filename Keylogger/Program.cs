using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Opdracht_5
{
    class Program
    {
        static void Main()
        {
            List<StringBuilder> allPasswords = new List<StringBuilder>();
            int n = int.Parse(Console.ReadLine().Split(' ')[0]);
            for (int i = 0; i < n; i++)
            {
                StringBuilder password = MakePassword(Console.ReadLine().Split(' ')[0]);
                allPasswords.Add(password);
            }
            foreach (StringBuilder password in allPasswords)
            {
                Console.WriteLine(password);
            }
        }

        static StringBuilder MakePassword(string input)
        {
            StringBuilder password = new StringBuilder();
            MyLinkedList passwordList = BuildPasswordList(input);
            for (MyLinkedList.Node node = passwordList.head; node != null; node = node.next)
            {
                password.Append(node.key);
            }
            return password;
        }

        static MyLinkedList BuildPasswordList(string input)
        {
            MyLinkedList passwordList = new MyLinkedList();
            string typedPassword = input;
            foreach (char character in typedPassword)
            {
                switch (character)
                {
                    case '<':
                        passwordList.MovePointerLeft();
                        break;
                    case '>':
                        passwordList.MovePointerRight();
                        break;
                    case '-':
                        passwordList.Backspace();
                        break;
                    default:
                        passwordList.AddAtPointer(character);
                        break;
                }
            }
            return passwordList;
        }

    }

    class MyLinkedList
    {
        public Node head;
        public Node pointer;
        public Node tail;

        public MyLinkedList()
        {
            head = null;
            tail = null;
            pointer = tail;
        }

        public void Backspace()
        {
            if (pointer != head)
            {
                if (pointer == null) // Dus einde van list
                {
                    if (tail == head)
                    {
                        head = null;
                        tail = null;
                    }
                    else
                    {
                        tail.prev.next = null;
                        tail = tail.prev;
                    }
                }
                    
                else
                {
                    Node nodeToDelete = pointer.prev;
                    if (nodeToDelete == head)
                    {
                        head = pointer;
                        pointer.prev = null;
                    }
                    else
                    {
                        nodeToDelete.prev.next = pointer;
                        pointer.prev = nodeToDelete.prev;
                    }
                }
            }
        }

        public void MovePointerLeft()
        {
            if (pointer == null)
            {
                pointer = tail;
            }
            else
            {
                if (pointer.prev != null)
                {
                    pointer = pointer.prev;
                }
            }
        }

        public void MovePointerRight()
        {
            if (pointer != null)
            {
                pointer = pointer.next;
            }
        }

        public void AddAtPointer(char key)
        {
            Node toAdd = new Node(key);
            if (pointer == null) // Add to end of the list
            {
                toAdd.prev = tail;
                if (tail != null) // So an empty list
                {
                    tail.next = toAdd;
                    tail = toAdd;
                }
                else
                {
                    tail = toAdd;
                    head = toAdd;
                }
            }
            else
            {
                toAdd.next = pointer;
                if (pointer.prev == null) // If adding to the beginning of the list
                {
                    pointer.prev = toAdd;
                    head = toAdd;
                }
                else // Adding somewhere in the middle of the list
                {
                    toAdd.prev = pointer.prev;
                    toAdd.prev.next = toAdd;
                    pointer.prev = toAdd;
                }
            }
        }

        public class Node
        {
            public char key;
            public Node next = null;
            public Node prev = null;

            public Node(char Ikey)
            {
                key = Ikey;
            }
        }
    }
}
