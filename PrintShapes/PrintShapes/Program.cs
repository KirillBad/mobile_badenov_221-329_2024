using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrintShapes
{
    internal class Program
    {

        static string[] GetRhomb(int height, char symbol, bool isFilled)
        {
            height = height % 2 == 0 ? height + 1 : height;

            int midHeight = height / 2;
            string[] rhomb = new string[height];

            for (int i = 0; i <= midHeight; i++)
            {
                int spacesBefore = midHeight - i;
                int spacesInside = 2 * i - 1;

                if (isFilled)
                {
                    if (i == 0)
                    {
                        rhomb[i] = new string(' ', spacesBefore) + symbol; 
                    }
                    else
                    {
                        rhomb[i] = new string(' ', spacesBefore) + symbol + new string(symbol, spacesInside) + symbol;
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        rhomb[i] = new string(symbol, spacesBefore) + symbol + new string(symbol, spacesBefore);
                    }
                    else
                        rhomb[i] = new string(symbol, spacesBefore) + symbol + new string(' ', spacesInside) + new string(symbol, spacesBefore) + symbol ;
                }

                rhomb[height - i - 1] = rhomb[i];
            }

            return rhomb;
        }

        static void PrintRhomb(string[] rhomb)
        {
            int height = rhomb.Length;

            for (int i = 0; i < height / 2; i++)
            {
                Console.WriteLine($"{i} {rhomb[i]}");
            }

            for (int i = height / 2; i >= 0; i--)
            {
                Console.WriteLine($"{i} {rhomb[i]}");
            }
        }

        static string[] GetSandClock(int height, char symbol, bool isFilled)
        {
            height = height % 2 == 0 ? height + 1 : height;

            int midHeight = height / 2;
            string[] sandClock = new string[height];

            for (int i = 0; i <= midHeight; i++)
            {
                int spacesBefore = i;
                int spacesInside = height - 2 * (i + 1);

                if (spacesInside < -1) spacesInside = 1;

                if (isFilled)
                {
                    if (i == midHeight)
                    {
                        sandClock[i] = new string(' ', spacesBefore) + new string(symbol, height - 2 * spacesBefore);
                    }
                    else
                    {
                        sandClock[i] = new string(' ', spacesBefore) + symbol + new string(symbol, spacesInside) + symbol;
                    }
                }
                else
                {
                    if (i == 0 || i == height)
                    {
                        sandClock[i] = new string(' ', spacesBefore) + new string(symbol, height - 2 * spacesBefore);
                    }
                    else if (i == midHeight)
                    {
                        sandClock[i] = new string(' ', spacesBefore) + new string(symbol, height - 2 * spacesBefore);
                    }
                    else
                    {
                        sandClock[i] = new string(' ', spacesBefore) + symbol + new string(' ', spacesInside) + symbol;
                    }
                }

                sandClock[height - i - 1] = sandClock[i];
            }

            return sandClock;
        }

        static void PrintSandClock(string[] sandClock)
        {
            foreach (string line in sandClock)
            {
                Console.WriteLine(line);
            } 
        }

        static string[] GetRectangles(int numberOfRectangles, int startWidth, int startHeight, int spaceBetween, char[] symbols)
        {
            int maxWidth = startWidth + 2 * (numberOfRectangles - 1) + 2 * spaceBetween * (numberOfRectangles - 1);
            int maxHeight = startHeight + 2 * (numberOfRectangles - 1) + 2 * spaceBetween * (numberOfRectangles - 1);

            string[] rectangles = new string[maxHeight];

            char[,] canvas = new char[maxHeight, maxWidth];

            for (int i = 0; i < maxHeight; i++)
                for (int j = 0; j < maxWidth; j++)
                    canvas[i, j] = ' ';

            for (int level = 0; level < numberOfRectangles; level++)
            {
                int currentWidth = startWidth + 2 * level + 2 * spaceBetween * level;
                int currentHeight = startHeight + 2 * level + 2 * spaceBetween * level;

                char symbol = symbols[level];

                int startX = (maxWidth - currentWidth) / 2;
                int startY = (maxHeight - currentHeight) / 2;

                for (int i = startX; i < startX + currentWidth; i++)
                {
                    canvas[startY, i] = symbol;
                    canvas[startY + currentHeight - 1, i] = symbol;
                }

                for (int i = startY; i < startY + currentHeight; i++)
                {
                    canvas[i, startX] = symbol;
                    canvas[i, startX + currentWidth - 1] = symbol;
                }
            }

            for (int i = 0; i < maxHeight; i++)
            {
                string line = "";
                for (int j = 0; j < maxWidth; j++)
                {
                    line += canvas[i, j];
                }
                rectangles[i] = line;
            }

            return rectangles;
        }

        static void PrintRectangles(string[] rectangles)
        {
            foreach (var line in rectangles)
            {
                Console.WriteLine(line);
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите высоту ромба (нечетное число > 3): ");
            int height = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите символ, из которого будет состоять ромб: ");
            char symbol = Convert.ToChar(Console.ReadLine());
            Console.Write("Залить фигуру? (y/n): ");
            bool isFilled = Console.ReadLine() == "y" ? true : false;
            string[] rhomb = GetRhomb(height, symbol, isFilled);
            Console.WriteLine();
            PrintRhomb(rhomb);


            Console.WriteLine();


            Console.Write("Введите высоту песочных часов (нечетное число > 3): ");
            height = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите символ, из которого будeт состоять песочные часы: ");
            symbol = Convert.ToChar(Console.ReadLine());
            Console.Write("Залить фигуру? (y/n): ");
            isFilled = Console.ReadLine() == "y" ? true : false;
            string[] sandClock = GetSandClock(height, symbol, isFilled);
            Console.WriteLine();
            PrintSandClock(sandClock);


            Console.WriteLine();


            Console.Write("Введите количество прямоугольников: ");
            int num = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите ширину самого маленького прямоугольника: ");
            int rec_width = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите длину самого маленького прямоугольника: ");
            int rec_height = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите длину промежутка между прямоугольниками: ");
            int space = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите символы, из которого будут состоять прямоугольники: ");
            char[] symbols = new char[num];
            for (int i = 0; i < num; i++)
            {
                symbols[i] = Convert.ToChar(Console.ReadLine());
            }
            string[] rectangles = GetRectangles(num, rec_width, rec_height, space, symbols);
            Console.WriteLine();
            PrintRectangles(rectangles);
        }
    }
}
