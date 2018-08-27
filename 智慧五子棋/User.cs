using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace 智慧五子棋
{
    /// <summary>
    /// 玩家
    /// </summary>
    class User
    {
        //玩家姓名
        public string name;
        //是黑棋么
        public bool black = false;
        //光标位置
        public int Px = 7, Py = 7;
        //下棋步数
        public int count;
        //是否优先
        public bool main = true;
        //音效开关,默认为开
        public bool voice = true;
        //创建播放声音的对象
        public SoundPlayer sp = new SoundPlayer();

        public void SetInfo()
        {
            #region 输入姓名
            Console.Write("请输入玩家姓名：\n\t\t");
            this.name = Console.ReadLine();
            Console.WriteLine();
            this.PlayMusic("wong.wav",false);
            #endregion

            //没有优先权就不能设置棋子颜色
            if (main == false)
                return;

            #region 选择黑白棋
            Console.WriteLine("请按←或→选择：");
            int L0 = Console.CursorTop;//记录当前行

            Info info = new Info();
            string[] info1 = { "  黑棋●", "    先出棋", "" };
            string[] info2 = { "  白棋●", "    后出棋", "" };
            info.ShowInfo(info1, info2);
            Console.SetCursorPosition(7, L0 + 5);
            Console.WriteLine("默认为白棋");

            //设置光标不可见
            Console.CursorVisible = false;

            do
            {
                ConsoleKeyInfo In = Console.ReadKey(true);             //用户按下一个键
                this.PlayMusic("wong.wav", false);

                if (In.Key == ConsoleKey.LeftArrow) //按向左键
                {
                    black = true;   //是黑棋
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, L0);
                    Console.Write("┏");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, L0);
                    Console.Write("┓");
                    Console.SetCursorPosition(0, L0 + 3);
                    Console.Write("┗");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, L0 + 3);
                    Console.Write("┛");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 1, L0);
                    Console.WriteLine("  ");
                    Console.SetCursorPosition(Console.WindowWidth - 2, L0);
                    Console.WriteLine("  ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 1, L0 + 3);
                    Console.WriteLine("  ");
                    Console.SetCursorPosition(Console.WindowWidth - 2, L0 + 3);
                    Console.WriteLine("  ");
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                if (In.Key == ConsoleKey.RightArrow) //按向右键
                {
                    black = false;  //是白棋
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, L0);
                    Console.Write("  ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, L0);
                    Console.Write("  ");
                    Console.SetCursorPosition(0, L0 + 3);
                    Console.Write("  ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 2, L0 + 3);
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 1, L0);
                    Console.WriteLine("┏");
                    Console.SetCursorPosition(Console.WindowWidth - 2, L0);
                    Console.WriteLine("┓");
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 1, L0 + 3);
                    Console.WriteLine("┗");
                    Console.SetCursorPosition(Console.WindowWidth - 2, L0 + 3);
                    Console.WriteLine("┛");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(Math.Min(10 + this.name.Length * 2 + 6,Console.WindowWidth-1), L0 - 3);
                Console.WriteLine("●");
                if (In.Key == ConsoleKey.Enter)     //按Enter键
                    break;
                Console.SetCursorPosition(7, L0 + 5);
                Console.WriteLine("按Enter键确定");
            } while (true);
            Console.Clear();
            #endregion
        }

        #region 出棋方法
        public void Play(ref Chessboard board)
        {
            count++;
            //设置光标不可见
            Console.CursorVisible = false;

            do
            {
                board.ShowBord(Px, Py, (this.black ? ConsoleColor.Red : ConsoleColor.Green));
                //信息栏
                Console.SetCursorPosition(0, 0);
                Info info = new Info();
                string[] user = { "我：" + this.name, "\t第" + count + "步", "\t位置:" + (char)(Px + 65) + (15 - Py) };
                info.ShowInfo(black ? user : new string[0], black ? new string[0] : user);

                ConsoleKeyInfo In = Console.ReadKey(true);             //用户按下一个键

                #region 移动光标
                if (In.Key == ConsoleKey.LeftArrow) //按向左键
                {
                    Px = (Px + 14) % 15;
                    this.PlayMusic("move.wav",false);
                    continue;
                }
                if (In.Key == ConsoleKey.RightArrow) //按向右键
                {
                    Px = (Px + 1) % 15;
                    this.PlayMusic("move.wav", false);
                    continue;
                }
                if (In.Key == ConsoleKey.UpArrow) //按向上键
                {
                    Py = (Py + 14) % 15;
                    this.PlayMusic("move.wav", false);
                    continue;
                }
                if (In.Key == ConsoleKey.DownArrow) //按向下键
                {
                    Py = (Py + 1) % 15;
                    this.PlayMusic("move.wav", false);
                    continue;
                }
                #endregion

                #region 出棋
                if (In.Key == ConsoleKey.Enter && board.bord[Py, Px] == 0) //按Enter键
                {
                    if (board.bord[Py, Px] == 0) board.bord[Py, Px] = (sbyte)(this.black ? -1 : 1);
                    this.PlayMusic("LuoQi.wav", false);
                    break;  //完成出棋，退出循环
                }
                #endregion


            } while (true);
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
