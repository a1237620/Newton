using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newton.Equ
{
    public static class StrExt
    {
        public static void ToConsole(this  string message,  ConsoleColor color = ConsoleColor.White)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                bool isColorDefault = (color == ConsoleColor.White);
                if (!isColorDefault)
                {
                    Console.ForegroundColor = color;
                }

                Console.WriteLine(message);

                if (!isColorDefault)
                {
                    Console.ResetColor();
                }
            }
        }
    }
}
