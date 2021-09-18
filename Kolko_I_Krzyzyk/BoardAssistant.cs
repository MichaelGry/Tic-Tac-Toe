using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolko_I_Krzyzyk
{
    static class BoardAssistant
    {
        static public bool IsWinning(int[,] b, int znak)
        {
            if (b[0, 0] == znak && b[0, 1] == znak && b[0, 2] == znak) return true;
            if (b[1, 0] == znak && b[1, 1] == znak && b[1, 2] == znak) return true;
            if (b[2, 0] == znak && b[2, 1] == znak && b[2, 2] == znak) return true;

            if (b[0, 0] == znak && b[1, 0] == znak && b[2, 0] == znak) return true;
            if (b[0, 1] == znak && b[1, 1] == znak && b[2, 1] == znak) return true;
            if (b[0, 2] == znak && b[1, 2] == znak && b[2, 2] == znak) return true;

            if (b[0, 0] == znak && b[1, 1] == znak && b[2, 2] == znak) return true;
            if (b[0, 2] == znak && b[1, 1] == znak && b[2, 0] == znak) return true;

            return false;
        }
        static public bool IsFull(int[,] b, int znak)
        {
            for (int i = 0; i < b.GetLength(0); i++) for (int j = 0; j < b.GetLength(1); j++)
                    if (b[i, j] == znak) return false;
            return true;
        }
    }
}
