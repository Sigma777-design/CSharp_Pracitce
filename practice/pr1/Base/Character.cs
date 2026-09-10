using System;
using System.Collections.Generic;
using System.Text;

namespace pr1.Base;

//Абстрактний клас "Персонаж". Абстрактний, так як створений тільки для наслідування.
internal abstract class Character
{
    private int _mana;
    private int _damage;
    private static int _globalId = 0;
    private int initialMana;

    public string? Name { get; private set; }
    public int Id { get; private set; }
    public int Damage
    {
        get { return _damage; }
        set { _damage = value < 0 ? 0 : value; }
    }
    public int Mana
    {
        get { return _mana; }
        set { _mana = value < 0 ? 0 : value; }
    }


    public Character(int initialMana, string name)
    {
        Id = _globalId++;
        Mana = initialMana;
        Name = name;
    }

    private void DamageMath()
    {
        Damage = Mana * 10;
    }

    public void Rest() {
        Mana += 10;
    }

    public void Rest(int hours)
    {
        Mana += hours* 10;
    }

    public virtual int CastSpell()
    {
        Console.WriteLine("Каст звичайного спеллу.");
        DamageMath();
        return _damage;
    }

} 
