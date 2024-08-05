using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.Utility
{
    /// <summary>
    /// This class represents a singly linked list that implements the ILinkedListADT interface. 
    /// If supports operations such as adding elements at the beginning or the end, 
    /// removing elements, checking if the list is empty, and finding elements.
    /// This class can be serialized; This is used for storing or transmitting data structures involving nodes.
    /// </summary>
    [Serializable]
    public class SLL : ILinkedListADT
    {
        private Node _head;
        private Node _tail;
        private int _size;

        /// <summary>
        /// Initializes a new instance of the SLL class
        /// Sets the head and the tail nodes to null and the size to zero
        /// </summary>
        public SLL()
        {
            this._head = null;
            this._tail = null;
            this._size = 0;
        }

        /// <summary>
        /// Gets/sets the head node of the linked list
        /// </summary>
        public Node Head
        {
            get { return _head; }
            set { _head = value; } 
        }

        /// <summary>
        /// Gets/sets the tail node of the linked list
        /// </summary>
        public Node Tail
        {
            get { return _tail; }
            set { _tail = value; }
        }

        /// <summary>
        /// Gets/sets the side of the linked list
        /// </summary>
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        //Implemented methods

        /// <summary>
        /// Determines whether the linked list is empty
        /// </summary>
        /// <returns>True is empty, false if not</returns>
        public bool IsEmpty()
        {
            if (Size == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Clears all elements (users) from the linked list by setting the size to zero and head to null
        /// </summary>
        public void Clear()
        {
            this.Head = null;
            this.Size = 0;
        }

        /// <summary>
        /// Adds a new user element at the end of the linked list
        /// </summary>
        /// <param name="value">The value to be added (user)</param>
        public void AddLast(User value)
        {
            Node newNode = new Node(value);

            if (this.Tail == null)
            {
                this.Head = this.Tail = newNode;
            }
            else
            {                
                this.Tail.Next = newNode;

                this.Tail = newNode;
            }
            Size++;
        }

        /// <summary>
        /// Adds a new user element at the beginning of the linked list
        /// </summary>
        /// <param name="value">The value to be added (user)</param>
        public void AddFirst(User value)
        {
            Node newNode = new Node(value);

            if (this.IsEmpty())
            {
                this.Head = this.Tail = newNode;
            }
            else
            {
                newNode.Next = this.Head;
                this.Head = newNode;
            }
            Size++;
        }

        /// <summary>
        /// Adds a new user element at the specified index in the linked list
        /// </summary>
        /// <param name="value">The value to be added (user)</param>
        /// <param name="index">The zero-based index at which value should be inserted</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Add(User value, int index)
        {
            //Exception handling
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("Index must be greater than 0");
            }
            if (index > Count())
            {
                throw new ArgumentOutOfRangeException("Index is greater than list size ");
            }

            //Node currentNode = this.Head;
            Node newNode = new Node(value);

            if (index == 0)
            {
                AddFirst(value);
                return;
            }
            else
            {
                Node currentNode = this.Head;
                for (int i = 0; i < index - 1; i++)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Next = newNode.Next;
                currentNode.Next = newNode;
                //Increments size of list to account for new item added
            }
            Size++;

        }

        /// <summary>
        /// Replaces the user element at the specified index in the linked list with a new value.
        /// </summary>
        /// <param name="value">The new value</param>
        /// <param name="index">The zero-based index of the element to replace</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void Replace(User value, int index)
        {
            //Exception handling
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("Index must be greater than 0");
            }
            if (this.IsEmpty())
            {
                throw new Exception("The list is empty");
            }
            if (index > Size)
            {
                throw new ArgumentOutOfRangeException("Index is greater than size of list");
            }

            Node currentNode = this.Head;

            if (index == 0)
            {
                RemoveFirst();
                AddFirst(value);
                return;
            }
            else
            {
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
                //Increments size of list to account for new item added

                if (currentNode.Next != null)
                {
                    currentNode.Data = value;
                }
                Size++;
            }
        }

        /// <summary>
        /// Returns the count of nodes in the linked list
        /// </summary>
        /// <returns>The number of nodes in the linked list</returns>
        public int Count()
        {
            return this.Size;
        }

        /// <summary>
        /// Removes the first user element from the linked list
        /// </summary>
        public void RemoveFirst()
        {
            if (this.Head != null)
            {
                Node newNode = this.Head;
                this.Head = this.Head.Next;
                newNode.Next = null;
                Size--;

                if (IsEmpty())
                {
                    this.Tail = null;
                }
            }
        }

        /// <summary>
        /// Removes the last user element from the linked list
        /// </summary>
        public void RemoveLast()
        {
            if (this.IsEmpty())
            {
                throw new Exception("Cannot remove, list is empty");
            }
            else if (this.Count() == 1)
            {
                this.Head = this.Tail = null;
            }
            else
            {
                Node currentNode = this.Head;
                while (currentNode.Next != this.Tail)
                {
                    currentNode = currentNode.Next;
                }
                //Make the second last node pointer to null
                currentNode.Next = null;
                currentNode = this.Tail;


            }
            Size--;
        }

        /// <summary>
        /// Removes the user element at the specificed index from the linked list.
        /// </summary>
        /// <param name="index">The zero-based index of the user element to remove</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index is not within the valid specified range</exception>
        public void Remove(int index)
        {
            //Exception handling
            if (index < 0 || index >= Count())
            {
                throw new ArgumentOutOfRangeException("Index is out of range");
            }
            else if (index == 0)
            {
                RemoveFirst();
                return;
            }
            else
            {
                Node currentNode = this.Head;
                Node removedNode = null;

                for (int i = 0; i < index; ++i)
                {
                    removedNode = currentNode;
                    currentNode = currentNode.Next;
                }
                if (removedNode != null)
                {
                    removedNode.Next = currentNode.Next;
                }
            }
            Size--;
        }

        /// <summary>
        /// Returns the value of the user element at the specified index
        /// </summary>
        /// <param name="index">The zero-based index of the user element to retrieve</param>
        /// <returns>The user element value at the specified index</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public User GetValue(int index)
        {
            //Exception handling
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("Index must be greater than 0");
            }
            if (this.IsEmpty())
            {
                throw new Exception("The list is empty");
            }
            if (index > Size)
            {
                throw new ArgumentOutOfRangeException("Index is greater than size of list");
            }

            Node currentNode = this.Head;

            if (index == 0)
            {
                return currentNode.Data;
            }
            else
            {
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
                
                return currentNode.Data;
            }
        }

        /// <summary>
        /// Returns the zero-based index of the first occured value in the linked list
        /// </summary>
        /// <param name="value">The value to locate in the linked list</param>
        /// <returns>The zero-based index of the 1st occured value within the whole 
        /// linked list, if found. otherwise, -1 </returns>
        public int IndexOf(User value)
        {
            Node currentNode = this.Head;

            int index = 0;

            while (currentNode != null)
            {
                if (currentNode.Data.Equals(value))
                {
                    return index;
                }
                currentNode = currentNode.Next;
                index++;
            }
            return -1;


        }

        /// <summary>
        /// Returns the value of the first occured value in the linked list
        /// </summary>
        /// <param name="value">The value to locate in the linked list</param>
        /// <returns>The value of the 1st occured value within the whole 
        /// linked list</returns>
        public bool Contains(User value)
        {
            return this.IndexOf(value) >= 0;
        }

        public void Reverse()
        {
            Node previousNode = null;
            Node currentNode = this.Head;
            Node nextNode = null;

            while ( currentNode != null )
            {
                nextNode = currentNode.Next;
                currentNode.Next = previousNode;
                previousNode = currentNode;
                currentNode = nextNode;
            }

            this.Head = previousNode;
        }
    }
}
