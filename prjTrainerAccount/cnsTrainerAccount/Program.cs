using System;
using libCore;

namespace cnsTrainerAccount
{
    internal class Program
    {
        private static Game g;

        static void Main(string[] args)
        {
            Console.WriteLine("Игра 'Устный счет'");
            Console.WriteLine();

            g = new Game();
            g.ChangeQuestion += () => Console.WriteLine($"Вопрос: {g.QuestionText}");
            g.ChangeStatistic += () =>
                Console.WriteLine($"Статистика: Верно = {g.CountCorrect}, Неверно = {g.CountWrong}";
            g.GameStart();

            var dt_start = DateTime.Now; // TODO вынести в класс Game

            while (true)
            {
                Console.WriteLine("Ответ Y/N ? ");
                var line = Console.ReadLine()?.ToUpper();
                if (line == "Y")
                    g.DoAnswer(true);
                else if (line == "N")
                    g.DoAnswer(false);
                else
                    break;
            }

            Console.WriteLine();
            Console.WriteLine($"Ты играл {(DateTime.Now - dt_start).TotalSeconds} секунд!");
            Console.WriteLine("Пока!");
        }
    }
}
