using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 智慧五子棋
{
    /// <summary>
    /// 棋盘类
    /// </summary>
    class Chessboard
    {
        //保存棋盘（0：格子 1：白棋 -1：黑棋）
        public sbyte[,] bord = new sbyte[15, 15];
        //棋盘位置
        public int Px, Py;

        public void ShowFrom()
        {
            //棋盘四角
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(this.Px, this.Py);
            Console.Write("┏");
            Console.SetCursorPosition(this.Px + 32, this.Py);
            Console.Write("┓");
            Console.SetCursorPosition(this.Px, this.Py + 16);
            Console.Write("┗");
            Console.SetCursorPosition(this.Px + 32, this.Py + 16);
            Console.Write("┛");
            //棋盘四边
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < 15; i++)
            {
                Console.SetCursorPosition(this.Px + i * 2 + 2, this.Py);     //上
                Console.Write("┳");
                Console.SetCursorPosition(this.Px + i * 2 + 2, this.Py + 16);//下
                Console.Write("┻");
                Console.SetCursorPosition(this.Px + i * 2 + 2, this.Py + 17);//字母索引
                Console.Write((char)(65 + i));
                Console.SetCursorPosition(this.Px, this.Py + i + 1);     //左
                Console.Write("┣");
                Console.SetCursorPosition(this.Px + 32, this.Py + i + 1);//右
                Console.Write("┫" + (15 - i));
            }
        }
        /// <summary>
        /// 打印棋盘
        /// </summary>
        /// <param name="Px">光标横坐标（左上角为0,0）</param>
        /// <param name="Py">光标纵坐标</param>
        public void ShowBord(int Px, int Py, ConsoleColor P)
        {
            //记录背景色
            ConsoleColor B = Console.BackgroundColor;
            //棋盘背景色
            ConsoleColor BordBg = ConsoleColor.Gray;
            //格子颜色
            ConsoleColor F = ConsoleColor.DarkYellow;

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Console.BackgroundColor = BordBg;   //设置棋盘背景色
                    Console.SetCursorPosition(j * 2 + this.Px + 2, this.Py + i + 1);
                    //显示光标
                    if (i == Py && j == Px)
                    {
                        //设置光标背景色
                        Console.BackgroundColor = P;
                    }
                    switch (bord[i, j])
                    {
                        case 0:
                            Console.ForegroundColor = F;
                            Console.Write('╋');
                            break;
                        case -1:
                            //显示黑子
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write('●');
                            break;
                        case 1:
                            //显示白子
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write('●');
                            break;
                    }
                }
            }
            //恢复背景色
            Console.BackgroundColor = B;
        }
    }
}
