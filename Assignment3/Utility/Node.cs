using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.Utility
{
    [Serializable]
    public class Node
    {
        //fields
        private User _data;
        private Node _next;

        //properties
        public User Data
        {
            get { return _data; }
            set { _data = value; }
        }
        public Node Next
        {
            get { return _next; }
            set { _next = value; }
        }

        //constructor
        public Node(User Data)
        {
            this._data = Data;
            this._next = null;
        }


    }
}
