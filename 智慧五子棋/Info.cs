using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 智慧五子棋
{
    class Info
    {
        public void ShowInfo(string[] black, string[] white)
        {
            //保存前景色、背景色
            ConsoleColor F = Console.ForegroundColor;
            ConsoleColor B = Console.BackgroundColor;
            //信息栏行数
            int Line = Math.Max(black.Length, white.Length);
            //开始行号
            int L0 = Console.CursorTop;
            #region 显示黑棋信息
            Console.SetCursorPosition(0, L0);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int j = Console.CursorLeft + 1; j < Console.WindowWidth / 2; j += 2)
                Console.Write('●');
            for (int i = 0; i < Line; i++)
            {
                Console.SetCursorPosition(0, i + L0 + 1);
                Console.Write(i < black.Length ? black[i] : "");
                for (int j = Console.CursorLeft; j < Console.WindowWidth / 2; j++)
                {
                    Console.Write(' ');
                }
            }
            #endregion

            #region 显示白棋信息
            Console.SetCursorPosition(Console.WindowWidth / 2, L0);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            for (int j = Console.CursorLeft + 1; j < Console.WindowWidth; j += 2)
                Console.Write('●');
            for (int i = 0; i < Line; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2, L0 + i + 1);
                Console.Write(i < white.Length ? white[i] : "");
                for (int j = Console.CursorLeft; j < Console.WindowWidth; j++)
                {
                    Console.Write(' ');
                }
            }
            #endregion

            //恢复进入方法前的前景色、背景色
            Console.ForegroundColor = F;
            Console.BackgroundColor = B;
        }
    }
}
