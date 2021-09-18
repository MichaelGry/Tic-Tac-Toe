using System;
using System.Collections.Generic;

namespace Kolko_I_Krzyzyk
{
    class Program
    {

        static void Main(string[] args)
        {
            Start();
        }
        static void Start()
        {
            (int width, int height) windowSize = (100, 30);
            Console.SetWindowSize(windowSize.width, windowSize.height);
            Console.SetBufferSize(windowSize.width, windowSize.height);
            Console.Title = "KÓŁKO I KRZYŻYK";
            ShowMenu();
        }
        static void ShowMenu()
        {
            Console.Clear();
            string[] menu =
            {
                "ROZPOCZNIJ NOWĄ GRĘ", //0
                "INSTRUKCJA", //1
                "ZDEFINIUJ SWOJE ZNAKI",
                "WIĘCEJ O GRZE...", //2
                "ZAKOŃCZ" //3
            };
            int make = GetMenu(menu);
            switch (make)
            {
                case 0: StartGame(); break;
            }
        }
        static int GetMenu(string[] menu)
        {
            int amountsOfElem = menu.Length;
            int currentPosition = 0;
            ConsoleKeyInfo click;
            do
            {
                Console.Clear();
                for (int i = 0; i < amountsOfElem; i++)
                {
                    if (currentPosition == i)
                    {
                        ConsoleColor currentFColor = Console.ForegroundColor;
                        ConsoleColor currentBColor = Console.BackgroundColor;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine("\t" + menu[i]);
                        Console.ForegroundColor = currentFColor;
                        Console.BackgroundColor = currentBColor;
                    }
                    else Console.WriteLine("\t" + menu[i]);
                }
                click = Console.ReadKey(true);
                if (click.Key == ConsoleKey.UpArrow)
                {
                    currentPosition--;
                    if (currentPosition <= -1) currentPosition = amountsOfElem - 1;
                }
                else if (click.Key == ConsoleKey.DownArrow)
                {
                    currentPosition++;
                    if (currentPosition >= amountsOfElem) currentPosition = 0;
                }
            } while (click.Key != ConsoleKey.Enter);
            return currentPosition;
        }
        static void StartGame()
        {
            string[] x = {@"  \  /   ", @"   \/    ", @"   /\    ", @"  /  \   ", "         "};
            string[] o = { "   ***   ", "  *   *  ", "  *   *  ", "  *   *  ", "   ***   " };
            int[,] board = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            List<Object> allowedKeys = new List<Object>()
            {
                ConsoleKey.Enter,
                ConsoleKey.UpArrow,
                ConsoleKey.DownArrow,
                ConsoleKey.LeftArrow,
                ConsoleKey.RightArrow,
                ConsoleKey.Escape
            };
            Player p1 = new Player();
            Player p2 = new Player();
            bool play = true;
            (p1.Name, p2.Name) = SetNames();
            Random rmd = new Random();
            _ = ((rmd.Next(2)) == 0) ? (p1.TwójRuch = true) : (p2.TwójRuch = true);
            //board[0, 0] = p1.TwójRuch == true ? 2 : 3;
            Board board1 = new Board(x, o, (int[,])board.Clone());
            while (play)
            {
                Console.Clear();
                Console.WriteLine($"{p1.Name} [{p1.points}:{p2.points}] {p2.Name}");
                Console.WriteLine($"TWÓJ RUCH {(board1.APlayer == 2 ? p1.Name : p2.Name)}");
                board1.Show();
                ConsoleKey key = (ConsoleKey)GetKey(allowedKeys);
                if (key == ConsoleKey.Escape) play = false;
                else if (key == ConsoleKey.Enter)
                {
                    (bool Win, bool Draw) result = board1.Sprawdz();
                    if (result.Win)
                    {
                        string name = board1.APlayer == 2 ? p1.Name : p2.Name;
                        if (board1.APlayer == 2) p1.points++; else p2.points++;
                        Wining(name);
                        board1 = new Board(x, o, (int[,])board.Clone());
                    }
                    if (result.Draw)
                    {
                        Draw();
                        board1 = new Board(x, o, (int[,])board.Clone());
                    }
                }
                else board1.Move(key);
            }
            ShowMenu();
        }
        static (string name1, string name2) SetNames()
        {
            int nameSize = 10;
            Console.Clear();
            Console.Write("PODAJ IMIE PIERWSZEGO GRACZA ");
            string n1 = Console.ReadLine();
            n1 = n1.Substring(0, n1.Length > nameSize ? nameSize : n1.Length);
            Console.Write("PODAJ IMIE DRUGIEGO GRACZA ");
            string n2 = Console.ReadLine();
            n2 = n2.Substring(0, n2.Length > nameSize ? nameSize : n2.Length);
            return (n1, n2);

        }
        static Object GetKey(List<Object> keys)
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
            } while (!keys.Contains(key.Key));
            return key.Key;
        }
        static void Wining(string name)
        {
            Console.Clear();
            Console.WriteLine("    BRAWO!!! WYGRAŁ GRACZ {0}", name);
            Console.ReadKey();
        }
        static void Draw()
        {
            Console.Clear();
            Console.WriteLine("REMIS, TYM RAZEM NIE UDAŁO SIĘ NIKOMU WYGRAĆ, PRÓBUJCIE DALEJ!");
            Console.ReadKey();
        }
    }
}
