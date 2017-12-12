using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ListBoxTest
{
    class User : IEquatable<User>, IComparable<User>
    {
        public User(String name, IPEndPoint endPoint)
        {
            this.Name = name;
            this.EndPoint = endPoint;
        }

        public String Name { get; private set; }

        public IPEndPoint EndPoint { get; private set; }

        public int CompareTo(User other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(User other)
        {
            return this.Name == other.Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
