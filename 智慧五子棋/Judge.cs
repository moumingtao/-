using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 智慧五子棋
{
    /// <summary>
    /// 裁判类
    /// </summary>
    class Judge
    {
        #region 裁判方法
        public bool Umpire(sbyte[,] bord)
        {
            //判断是否存在数着的5子相连
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (Math.Abs(bord[i + 4, j] + bord[i + 3, j] + bord[i + 2, j] + bord[i + 1, j] + bord[i, j]) == 5)
                        return true;
                }
            }
            //判断是否存在横着的5子相连
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (Math.Abs(bord[i, j + 4] + bord[i, j + 3] + bord[i, j + 2] + bord[i, j + 1] + bord[i, j]) == 5)
                        return true;
                }
            }
            //判断是否存在\的五子相连
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (Math.Abs(bord[i + 4, j + 4] + bord[i + 3, j + 3] + bord[i + 2, j + 2] + bord[i + 1, j + 1] + bord[i, j]) == 5)
                        return true;
                }
            }
            //判断是否存在/的五子相连
            for (int i = 4; i < 15; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (Math.Abs(bord[i - 4, j + 4] + bord[i - 3, j + 3] + bord[i - 2, j + 2] + bord[i - 1, j + 1] + bord[i, j]) == 5)
                        return true;
                }
            }

            return false;
        }
        #endregion
    }
}
