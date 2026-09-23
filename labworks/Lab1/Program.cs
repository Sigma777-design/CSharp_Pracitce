using System;
using System.Collections.Generic;
using System.Text;

using Lab1.Base;
using Lab1.Models;
using Lab1.Services;
namespace Lab1
{
    internal class Program
    {
        // Допоміжний метод для гарного заголовка
        private static void PrintHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"   {title}");
            Console.WriteLine(new string('=', 60));
            Console.ResetColor();
            Console.WriteLine();
        }

        // "Пауза" + очищення екрана
        private static void NextStep()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Натисніть будь-яку клавішу для переходу до наступного етапу...");
            Console.ResetColor();
            Console.ReadKey();
        }
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var manager = new SquadManager();



            // ЕТАП 1: Реєстрація бійців та валідація
            PrintHeader("ЕТАП 1: РЕЄСТРАЦІЯ ТА ВАЛІДАЦІЯ БІЙЦІВ");

            var sekhmet = manager.CreateWitch(1000, "Сехмет", "Лінь", 20);
            var echidna = manager.CreateWitch(220, "Єхидна", "Жадібність", 13);
            var julius = manager.CreateKnight(100, "Юліус", "Меч Духів");
            var reinhart = manager.CreateKnight(150, "Райнхард", "Драконячий меч");

            Console.WriteLine("\n--- Перевірка бізнес-обмеження (мана <= 0) ---");
            try
            {
                Console.WriteLine("Спроба зареєструвати виснаженого бійця з 0 мани...");
                manager.CreateKnight(0, "Виснажений воїн", "Іржавий меч");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ПЕРЕХОПЛЕНО ПОМИЛКУ]: {ex.Message}");
                Console.ResetColor();
            }

            NextStep();

            // ЕТАП 2: Створення загону та перевірка імені
            PrintHeader("ЕТАП 2: СТВОРЕННЯ БОЙОВИХ ЗАГОНІВ");

            var vanguard = manager.CreateSquad("Авангард Лугуніки");

            Console.WriteLine("\n--- Перевірка бізнес-обмеження (порожня назва) ---");
            try
            {
                Console.WriteLine("Спроба створити загін із порожньою назвою...");
                manager.CreateSquad("   ");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ПЕРЕХОПЛЕНО ПОМИЛКУ]: {ex.Message}");
                Console.ResetColor();
            }

            NextStep();

            // ЕТАП 3: Формування складу та перевірка обмежень
            PrintHeader("ЕТАП 3: РОЗПОДІЛ ПО ЗАГОНАХ ТА ЛІМІТИ");

            manager.AddCharacterToSquad(sekhmet.Id, vanguard.Id);
            manager.AddCharacterToSquad(echidna.Id, vanguard.Id);
            manager.AddCharacterToSquad(julius.Id, vanguard.Id);

            Console.WriteLine("\n--- Перевірка бізнес-обмеження (дублікат бійця) ---");
            try
            {
                Console.WriteLine($"Спроба додати {julius.Name} в той самий загін повторно...");
                manager.AddCharacterToSquad(julius.Id, vanguard.Id);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ПЕРЕХОПЛЕНО ПОМИЛКУ]: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\n--- Перевірка бізнес-обмеження (переповнення загону > 3) ---");
            try
            {
                Console.WriteLine($"Спроба додати четвертого бійця ({reinhart.Name})...");
                manager.AddCharacterToSquad(reinhart.Id, vanguard.Id);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ПЕРЕХОПЛЕНО ПОМИЛКУ]: {ex.Message}");
                Console.ResetColor();
            }

            NextStep();

            // ЕТАП 4: Перевірка методів пошуку (Read)
            PrintHeader("ЕТАП 4: ПОШУК СУТНОСТЕЙ ЧЕРЕЗ LINQ");

            Console.WriteLine("Пошук персонажа за ім'ям 'Єхидна':");
            var foundChar = manager.GetCharacterByName("Єхидна");
            if (foundChar != null)
                Console.WriteLine($"Знайдено: {foundChar.Name} (ID: {foundChar.Id}, Мана: {foundChar.Mana})");

            Console.WriteLine("\nПошук загону за ID 1:");
            var foundSquad = manager.GetSquadById(1);
            if (foundSquad != null)
                Console.WriteLine($"Знайдено загін: '{foundSquad.Name}', учасників: {foundSquad.Members.Count}");

            NextStep();

            // ЕТАП 5: Бойовий рейд (Сценарій 3-х сутностей)
            PrintHeader("ЕТАП 5: БОЙОВИЙ РЕЙД ЗАГОНУ");
            manager.SendSquadToBattle(vanguard.Id);

            NextStep();

            // ЕТАП 6: Виключення із загону та повторний бій
            PrintHeader("ЕТАП 6: РОТАЦІЯ БІЙЦІВ У ЗАГОНІ");

            Console.WriteLine("Виключаємо бійця із загону (але не видаляємо з гри):");
            manager.RemoveCharacterFromSquad(julius.Id, vanguard.Id);

            Console.WriteLine("\nТепер у загін звільнилося місце. Додаємо Райнхарда:");
            manager.AddCharacterToSquad(reinhart.Id, vanguard.Id);

            Console.WriteLine("\nПовторний бій оновленого складу:");
            manager.SendSquadToBattle(vanguard.Id);

            NextStep();

            // ЕТАП 7: Повне каскадне видалення бійця
            PrintHeader("ЕТАП 7: ПОВНЕ ВИДАЛЕННЯ З СИСТЕМИ");

            Console.WriteLine($"Видаляємо бійця {reinhart.Name} назавжди:");
            manager.RemoveCharacter(reinhart.Id);

            Console.WriteLine("\nЗагін іде в бій без вибулого учасника:");
            manager.SendSquadToBattle(vanguard.Id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[УСПІХ]: Всі сценарії та бізнес-правила протестовано без збоїв!");
            Console.ResetColor();
            Console.WriteLine("Натисніть будь-яку клавішу для завершення програми...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
