using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 智慧五子棋
{
    /// <summary>
    /// 电脑类
    /// </summary>
    class Computer
    {
        //是黑棋么
        public bool black = false;

        //出棋
        public void Play(ref Chessboard b)
        {
            Random ran = new Random();
            int Px = 0, Py = 0;
            do
            {
                Px = (sbyte)(ran.NextDouble() * 15);
                Py = (sbyte)(ran.NextDouble() * 15);
            } while (b.bord[Py, Px] != 0);
            b.bord[Py, Px] = (sbyte)(black ? -1 : 1);
        }
    }
}
