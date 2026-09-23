using Lab1.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    internal class Squad
    {
        public string? Name { get; private set; }

        private static int _globalId = 0;
        public int Id { get; private set; }
        public List<Character> Members { get; }

        public Squad(string name)
        {
            Id = _globalId++;
            Members = new List<Character>();
            Name = name;
        }


    }
}
