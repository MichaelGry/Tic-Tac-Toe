using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolko_I_Krzyzyk
{
    static class BoardAssistant
    {
        static public bool IsWinning(int[,] b, int token)
        {
            if (b[0, 0] == token && b[0, 1] == token && b[0, 2] == token) return true;
            if (b[1, 0] == token && b[1, 1] == token && b[1, 2] == token) return true;
            if (b[2, 0] == token && b[2, 1] == token && b[2, 2] == token) return true;

            if (b[0, 0] == token && b[1, 0] == token && b[2, 0] == token) return true;
            if (b[0, 1] == token && b[1, 1] == token && b[2, 1] == token) return true;
            if (b[0, 2] == token && b[1, 2] == token && b[2, 2] == token) return true;

            if (b[0, 0] == token && b[1, 1] == token && b[2, 2] == token) return true;
            if (b[0, 2] == token && b[1, 1] == token && b[2, 0] == token) return true;

            return false;
        }
        static public bool IsFull(int[,] b, int token)
        {
            for (int i = 0; i < b.GetLength(0); i++) for (int j = 0; j < b.GetLength(1); j++)
                    if (b[i, j] == token) return false;
            return true;
        }
    }
}
