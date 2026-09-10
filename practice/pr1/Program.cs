using System;
using System.Collections.Generic;
using System.Text;

using pr1.Base;
using pr1.Models;



namespace pr1;

internal class Program
{
    static void Main (string[] args) 
    {
        //Підтримка української мови.
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        //Створення об'єктів класу Authority.
        Authority sloth = new Authority("Лінь", 20);
        Authority greed = new Authority("Жадібноість", 13);

        //Створення списку бійців
        List<Character> fighters = new List<Character>
            {
                new Witch(1000, "Cехмет", sloth), 
                new Witch (220, "Єхидна", greed),
                new Knight(100, "Юліус", "Меч Духів")  
            };

        //-------------------------------------------------------------------------------------------

        Console.WriteLine("=== Початок битви ===");

        foreach (Character character in fighters)
        {

            Console.WriteLine($"\nХодить: {character.Name} (ID: {character.Id}, Мана: {character.Mana})");

            int damage = character.CastSpell();

            Console.WriteLine($"Нанесено урону: {damage}");
        }

        Console.ReadLine();
    }

}