using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolko_I_Krzyzyk
{
    public enum boardVariant: int
    {
        Pusty = 1, x =2, o=3, xZielony=4, xCzerwony=5, oZielony=6, oCzerwony=7
    };
    public enum ActualPlayer : int {X = 2, O = 3 }
    class Board
    {
        #region WŁAŚCIWOŚCI
        private readonly string[] x, o;
        private int[,] board = new int[3,3];
        ConsoleColor fColor, bColor;
        (int kolumna, int wiersz) HotPosituin = (0, 0), PreviousPosition = (0, 0);
        int HotChar, PreviousChar = 1;
        Random rmd;
        int AP; public int APlayer { get => AP; }
        #endregion
        public Board(string[] x, string[] o, int[,] board)
        {
            this.x = x;
            this.o = o;
            this.board = board;
            fColor = Console.ForegroundColor;
            bColor = Console.BackgroundColor;
            rmd = new Random();
            AP = rmd.Next(2) + 2;
            board[0, 0] = HotChar = AP == 2 ? 4 : 6;
        }  //KONSTRUKTOR
        public void Show()
        {
            string line = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < x.Length; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        line = "";
                        switch (board[i, k])
                        {
                            case (int)boardVariant.Pusty:
                                line = "         "; break;
                            case (int)boardVariant.x:
                                line = x[j]; break;
                            case (int)boardVariant.o:
                                line = o[j]; break;
                            case (int)boardVariant.xCzerwony:
                                Console.ForegroundColor = ConsoleColor.Red;
                                line = x[j]; break;
                            case (int)boardVariant.xZielony:
                                Console.ForegroundColor = ConsoleColor.Green;
                                line = x[j]; break;
                            case (int)boardVariant.oCzerwony:
                                Console.ForegroundColor = ConsoleColor.Red;
                                line = o[j]; break;
                            case (int)boardVariant.oZielony:
                                Console.ForegroundColor = ConsoleColor.Green;
                                line = o[j]; break;
                        }
                        Console.Write(line);
                        Console.ForegroundColor = fColor;
                        if(k < 2) Console.Write("*");
                    }
                    Console.WriteLine();
                }
                if (i < 2) Console.WriteLine("*****************************");
            }
        }
        public void Move(ConsoleKey key)
        {
            switch (((int)key))
            {
                case ((int)ConsoleKey.UpArrow): HotPosituin.kolumna = MoveDo(HotPosituin.kolumna, -1); break;
                case ((int)ConsoleKey.DownArrow): HotPosituin.kolumna = MoveDo(HotPosituin.kolumna, 1); break;
                case ((int)ConsoleKey.LeftArrow): HotPosituin.wiersz = MoveDo(HotPosituin.wiersz, -1); break;
                case ((int)ConsoleKey.RightArrow): HotPosituin.wiersz = MoveDo(HotPosituin.wiersz, 1); break;
            }  // ZMIEŃ AKTUALNĄ POZYCJĘ
            HotChar = board[HotPosituin.kolumna, HotPosituin.wiersz]; // ZMIEŃ AKTUALNY ZNAK
            //int TemporaryChar = HotChar;
            #region PRZYPISZ_NOWY_ZNAK //ZMIEN ZNAK W BOARD NA HP
            if (AP == (int)ActualPlayer.X)
            {
                if (HotChar == (int)boardVariant.Pusty)
                    board[HotPosituin.kolumna, HotPosituin.wiersz] = (int)boardVariant.xZielony;
                else board[HotPosituin.kolumna, HotPosituin.wiersz] = (int)boardVariant.xCzerwony;
            }
            else
            {
                if (HotChar == (int)boardVariant.Pusty)
                    board[HotPosituin.kolumna, HotPosituin.wiersz] = (int)boardVariant.oZielony;
                else board[HotPosituin.kolumna, HotPosituin.wiersz] = (int)boardVariant.oCzerwony;
            }
            #endregion 
            board[PreviousPosition.kolumna, PreviousPosition.wiersz] = PreviousChar;
            PreviousPosition = HotPosituin;
            PreviousChar = HotChar;
        } // jest ok
        private int MoveDo(int actualPosition ,int direction)
        {
            actualPosition = (actualPosition + direction) % 3;

            if (actualPosition == -1) return 2;
            return actualPosition;
        }
        public (bool Win, bool draw) Sprawdz()
        {
            int _HotChar = board[HotPosituin.kolumna, HotPosituin.wiersz];
            if (_HotChar == (int)boardVariant.oZielony || _HotChar == (int)boardVariant.xZielony)
            {
                if(AP == (int)ActualPlayer.O)
                {
                    PreviousChar = board[HotPosituin.kolumna, HotPosituin.wiersz] = (int)boardVariant.o;
                    if (BoardAssistant.IsWinning(board, AP)) return (true, false);
                    AP = (int)ActualPlayer.X;
                } else
                {
                    PreviousChar = board[HotPosituin.kolumna, HotPosituin.wiersz] = (int)boardVariant.x;
                    if (BoardAssistant.IsWinning(board, AP)) return (true, false);
                    AP = (int)ActualPlayer.O;
                }

            }
            if (BoardAssistant.IsFull(board, (int)boardVariant.Pusty)) return (false, true);
            return (false, false);
        }
    }
}
