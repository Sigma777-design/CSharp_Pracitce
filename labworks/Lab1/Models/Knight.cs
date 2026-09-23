using System;
using System.Collections.Generic;
using System.Text;
using Lab1.Base;

namespace Lab1.Models
{
    internal class Knight : Character
    {
        public string Weapon {  get; private set; }
        public Knight(int initialMana, string name, string weapon) : base(initialMana, name)
        {
            Weapon = weapon;
        }
        public override int CastSpell()
        {
            Console.WriteLine($"[Лицар] {Name} б'є фізично, використовуючи {Weapon}. Мана майже не витрачається.");
            return Damage + 100;
        }
    }
}
