using System;
using System.Collections.Generic;
using System.Text;

namespace pr1.Models
{
    internal class Authority
    {
        public string? Name { get; set; }
        private int _damagemultiplier;

        public Authority(string? name, int damagemultiplier)
        {
            Name = name;
            _damagemultiplier = damagemultiplier;
        }

        public int CalculateAuthorityDamage (int characterMana)
        {
            return characterMana * _damagemultiplier;
        }

    }
}
