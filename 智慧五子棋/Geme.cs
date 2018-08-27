using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace 智慧五子棋
{
    class Game
    {
        //玩家1
        User u1 = new User();
        //玩家2
        User u2 = new User();
        //电脑
        Computer Co = new Computer();
        //创建裁判
        Judge jud = new Judge();
        //音效开关,默认为开
        public bool voice = true;
        //创建播放声音的对象
        public SoundPlayer sp = new SoundPlayer();

        #region 单机
        /// <summary>
        /// 单机
        /// </summary>
        public void One()
        {
            //设置背景色
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Clear();
            //取消玩家2的优先权
            u2.main = !u1.main;
            //设置音效开关
            u1.voice = this.voice;
            u2.voice = this.voice;
            //玩家2设置信息
            u2.SetInfo();
            //玩家1设置信息
            u1.SetInfo();
            //设置玩家2的棋子颜色与玩家1相反
            u2.black = !u1.black;

            //信息栏
            Info info = new Info();
            string[] black = { "黑棋：", "\t先出棋", "\t开始" };
            string[] white = { "白棋", "\t等待黑棋", "\t等待。。。" };
            info.ShowInfo(black, white);

            //创建棋盘
            Chessboard board = new Chessboard();
            board.Px = 0; board.Py = 5;
            board.ShowBord(u1.Px, u1.Py, ConsoleColor.Green);
            board.ShowFrom();

            #region 对弈
            sbyte success = 0;  //胜利方0：平 1：白 -1：黑
            for (int i = 0; i < 113; i++)
            {


                if (u1.black)
                {
                    u1.Play(ref board);    //玩家1出棋

                    if (jud.Umpire(board.bord))    //判断胜利方
                    {
                        success = -1;
                        break;
                    }
                    u2.Play(ref board);    //玩家2出棋
                    if (jud.Umpire(board.bord))    //判断胜利方
                    {
                        success = 1;
                        break;
                    }
                }
                else
                {
                    u2.Play(ref board);    //玩家2出棋
                    if (jud.Umpire(board.bord))    //判断胜利方
                    {
                        success = -1;
                        break;
                    }
                    u1.Play(ref board);    //玩家1出棋
                    if (jud.Umpire(board.bord))    //判断胜利方
                    {
                        success = 1;
                        break;
                    }
                }
            }
            #endregion

            #region 显示对弈结果
            for (int i = 0; i < 10; i++)
            {
                //sleep一下                
                System.Threading.Thread.Sleep(100 + i * 60);
                Console.MoveBufferArea(0, i, Console.WindowWidth, 5, 0, i + 1, '*', ConsoleColor.Yellow, ConsoleColor.Red);
            }
            Console.SetCursorPosition(15, 6);
            this.PlayMusic("game over.wav",false);
            //sleep一下                
            System.Threading.Thread.Sleep(600);
            if (u1.black)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                if (success == -1) Console.Write("●胜利！");
                if (success == 1) Console.Write("●失败");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                if (success == 1) Console.Write("●胜利！");
                if (success == -1) Console.Write("●失败");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            if (success == 0) Console.Write("完美●平局");
            #endregion

        }
        #endregion

        #region 双人游戏
        /// <summary>
        /// 双人
        /// </summary>
        public void Two()
        {
            //设置背景色
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Clear();
            //设置音效开关
            u1.voice = this.voice;
            u2.voice = this.voice;
            //玩家1设置信息
            u1.SetInfo();
            //设置电脑的棋子颜色与玩家1相反
            Co.black = !u1.black;

            //信息栏
            Info info = new Info();
            string[] black = { "黑棋：", "\t先出棋", "\t开始" };
            string[] white = { "白棋", "\t等待黑棋", "\t等待。。。" };
            info.ShowInfo(black, white);

            //创建棋盘
            Chessboard board = new Chessboard();
            board.Px = 0; board.Py = 5;
            board.ShowBord(u1.Px, u1.Py, ConsoleColor.Green);
            board.ShowFrom();

            #region 对弈
            sbyte success = 0;  //胜利方0：平 1：白 -1：黑
            for (int i = 0; i < 113; i++)
            {


                if (u1.black)
                {
                    u1.Play(ref board);    //玩家出棋

                    if (jud.Umpire(board.bord))    //判断胜利方
                    {
                        success = -1;
                        break;
                    }
                    Co.Play(ref board);    //电脑出棋
                    if (jud.Umpire(board.bord))    //判断胜利方
                    {
                        success = 1;
                        break;
                    }
                }
                else
                {
                    Co.Play(ref board);    //电脑出棋
                    if (jud.Umpire(board.bord))    //判断胜利方
                    {
                        success = -1;
                        break;
                    }
                    u1.Play(ref board);    //玩家1出棋
                    if (jud.Umpire(board.bord))    //判断胜利方
                    {
                        success = 1;
                        break;
                    }
                }
            }
            #endregion

            #region 显示对弈结果
            for (int i = 0; i < 10; i++)
            {
                //sleep一下                
                System.Threading.Thread.Sleep(100 + i * 60);
                Console.MoveBufferArea(0, i, Console.WindowWidth, 5, 0, i + 1, '*', ConsoleColor.Yellow, ConsoleColor.Red);
            }
            Console.SetCursorPosition(15, 6);
            this.PlayMusic("game over.wav", false);
            //sleep一下                
            System.Threading.Thread.Sleep(600);
            if (u1.black)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                if (success == -1) Console.Write("●胜利！");
                if (success == 1) Console.Write("●失败");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                if (success == 1) Console.Write("●胜利！");
                if (success == -1) Console.Write("●失败");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            if (success == 0) Console.Write("完美●平局");
            #endregion

        }
        #endregion

        #region 播放声音文件
        /// <summary>
        /// 播放波形文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="Loop">是否循环播放</param>
        public void PlayMusic(string path, bool Loop)
        {
            //设置声音文件的路径
            sp.SoundLocation = "Music\\" + path;
            //如果声音设置为开就播放声音
            if (this.voice)
            {
                if (Loop)
                    sp.PlayLooping();
                else
                    sp.Play();
            }
        }
        #endregion

    }
}
