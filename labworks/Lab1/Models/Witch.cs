using System;
using System.Collections.Generic;
using System.Text;
using Lab1.Base;
namespace Lab1.Models
{
    internal class Witch : Character
    {
        public Authority WitchAuthority { get; private set; }
        public Witch(int initialMana, string name, Authority authority) : base(initialMana, name)
        {
            WitchAuthority = authority;
        }

        public override int CastSpell()
        {
            Console.WriteLine($"[Відьма] {Name} використовує повноваження: {WitchAuthority.Name}!");
            int finalDamage = WitchAuthority.CalculateAuthorityDamage(Mana);
            return finalDamage;
        }


    }
}
