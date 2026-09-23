using Lab1.Base;
using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Services
{
    internal class SquadManager
    {
        //Списки доступні тільки всередині SquadManager
        private readonly List<Squad> Squads = new();
        private readonly List<Character> Characters = new();

        private void AddCharacter(Character character) { //Додавання персонажу (універсальна перевірка для методу додавання нащадків)
            if (character == null) throw new ArgumentNullException(nameof(character), "Персонаж не може бути порожнім!");
            if (character.Mana <= 0) throw new InvalidOperationException($"Неможливо зареєструвати {character.Name}: недостатньо мани.");
            //------------------------
            Characters.Add(character);
            Console.WriteLine($"Персонаж {character.Name} (ID: {character.Id}) доданий до бази.");
        }

        public Witch CreateWitch(int mana, string name, string authorityName, int multiplier) //Створення відьми з повноваженням за параметрами
        {
            var authority = new Authority(authorityName, multiplier);
            var witch = new Witch(mana, name, authority);

            AddCharacter(witch); 

            return witch;
        }

        public Knight CreateKnight(int mana, string name, string weapon) //Додавання рицаря за параметрами  
        {
            var knight = new Knight(mana, name, weapon);

            AddCharacter(knight);

            return knight;
        }

        public Squad CreateSquad(string name) //Додавання загону
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва загону не може бути порожньою!", nameof(name));
            //------------------------
            var squad = new Squad(name);
            Squads.Add(squad);
            Console.WriteLine($"Створено новий загін \"{squad.Name}\" (ID: {squad.Id}).");

            return squad;
        }



        public Character? GetCharacterById(int id) //пошук персонажа за ID за допомогою LINQ 
        {
            return Characters.FirstOrDefault(c => c.Id == id); //FirstOrDefault : перший елемент, чи NULL/default
        }

        public Character? GetCharacterByName(string name) //пошук персонажа за іменем за допомогою LINQ 
        {
            return Characters.FirstOrDefault(c => c.Name == name); 
        }

        public Squad? GetSquadById(int id) //пошук загону за ID за допомогою LINQ 
        {
            return Squads.FirstOrDefault(s => s.Id == id);
        }

        public Squad? GetSquadByName(string name) //пошук загону за іменем за допомогою LINQ 
        {
            return Squads.FirstOrDefault(s => s.Name == name);
        }

        public void AddCharacterToSquad(int characterId, int squadId) 
        {
            var character = GetCharacterById(characterId);
            if (character == null)
                throw new KeyNotFoundException($"Персонажа з ID {characterId} не знайдено в базі!");

            var squad = GetSquadById(squadId);
            if (squad == null)
                throw new KeyNotFoundException($"Загін з ID {squadId} не знайдено!");

            // ліміт бійців у загоні (максимум 3)
            if (squad.Members.Count >= 3)
                throw new InvalidOperationException($"Загін \"{squad.Name}\" вже заповнений (максимум 3 бійці)!");

            // персонаж не може бути доданий до одного загону повторно
            if (squad.Members.Any(m => m.Id == characterId)) //any : чи існує хоча б 1 елемент, що відповідає умові
                throw new InvalidOperationException($"Боєць {character.Name} вже є у складі загону \"{squad.Name}\"!");

            //------------------------

            squad.Members.Add(character);
            Console.WriteLine($"{character.Name} успішно призначений до загону \"{squad.Name}\".");
        }

        public void RemoveCharacterFromSquad(int characterId, int squadId)
        {
            var character = GetCharacterById(characterId);
            if (character == null)
                throw new KeyNotFoundException($"Персонажа з ID {characterId} не знайдено!");

            var squad = GetSquadById(squadId);
            if (squad == null)
                throw new KeyNotFoundException($"Загін з ID {squadId} не знайдено!");

            // перевірка, чи взагалі боєць є в цьому загоні
            if (!squad.Members.Contains(character))
                throw new InvalidOperationException($"Боєць {character.Name} не перебуває у складі загону \"{squad.Name}\"!");

            //------------------------

            squad.Members.Remove(character);
            Console.WriteLine($"{character.Name} виключений із загону \"{squad.Name}\".");
        }

        public bool RemoveCharacter(int id)
        {
            var character = GetCharacterById(id);
            if (character == null)
            {
                Console.WriteLine($"Персонажа з ID {id} не знайдено для видалення.");
                return false;
            }

            // прибираємо бійця з усіх загонів
            foreach (var squad in Squads)
            {
                squad.Members.Remove(character);
            }

            //------------------------

            Characters.Remove(character);
            Console.WriteLine($"Персонаж {character.Name} (ID: {id}) видалений з системи.");
            return true;
        }

        public void SendSquadToBattle(int squadId) //Варіант виконання з main практичної №1
        {
            var squad = GetSquadById(squadId);
            if (squad == null)
                throw new KeyNotFoundException($"Загін з ID {squadId} не знайдено!");

            // загін без людей не може воювати
            if (squad.Members.Count == 0)
                throw new InvalidOperationException($"Загін \"{squad.Name}\" порожній! Нікому йти в бій.");

            Console.WriteLine($"\n=== [БІЙ]: Загін \"{squad.Name}\" вступає в битву! ===");
            int totalDamage = 0;

            foreach (var fighter in squad.Members)
            {
                Console.WriteLine($"\nБоєць {fighter.Name} (Мана: {fighter.Mana}) готує атаку:");
                int damage = fighter.CastSpell();
                Console.WriteLine($"Завдано шкоди: {damage}");
                totalDamage += damage;
            }

            Console.WriteLine($"\n>>> Бій завершено. Загальна завдана шкода загону \"{squad.Name}\": {totalDamage} <<<\n");
        }

    }
}
