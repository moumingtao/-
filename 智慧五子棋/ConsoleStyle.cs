using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace 智慧五子棋
{
    /// <summary>
    /// 此类包含控制台样式有关的代码
    /// </summary>
    class ConsoleStyle
    {
        //游戏名称
        private const string GemeName = "智慧五子棋";
        //音效开关,默认为开
        public bool voice = true;
        //创建播放声音的对象
        public SoundPlayer sp = new SoundPlayer();
        //创建进行游戏的对象
        Game game = new Game();

        public void Start()
        {
            //菜单标题
            string title = "**********" + GemeName + "**********\n";
            //菜单选项
            string[] select = { "退出游戏", "双人游戏", "单机模式", "声音" };

            while (true)
            {
                //设置窗口尺寸
                Console.SetWindowSize(40, 26);
                //设置缓冲区尺寸
                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
                //设置背景色
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Clear();
                //设置标题
                Console.Title = GemeName + ">>开始游戏";
                //设置前景色
                Console.ForegroundColor = ConsoleColor.Black;
                //游戏音效
                game.voice = this.voice;
            

                //清屏
                Console.Clear();
                //显示开始菜单，并接收用户选择
                int num = this.ShowMenu(title, select, 5, 5);
                //清屏
                Console.Clear();
                //设置标题
                Console.Title = "●智慧五子棋●";
                //执行选择的操作
                switch (num)
                {
                    case 0:
                        return;
                    case 1:
                        game.One();
                        break;
                    case 2:
                        game.Two();
                        break;
                    case 3:
                        //设置声音
                        this.SetVoice();
                        break;
                }
                //游戏结束暂停 
                if (num==1 || num==2)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.SetCursorPosition(0, Console.WindowHeight - 2);
                    Console.Write("按 table 键继续。。。");
                    ConsoleKeyInfo k = Console.ReadKey(true);
                    while (k.Key != ConsoleKey.Tab)
                    {
                        this.PlayMusic("KeyErr.wav", false);
                        k = Console.ReadKey(true);
                    }
                }
            }
        }

        #region 设置声音开关
        public void SetVoice()
        {
            int num = this.ShowMenu("*****声音设置*****", new string[] {"打开声音","关闭声音" },5,5);
            switch (num)
            {
                case 0:
                    voice = true;
                    break;
                case 1:
                    voice = false;
                    break;
            }
        }
        #endregion

        #region 菜单
        /// <summary>
        /// 显示菜单
        /// </summary>
        /// <param name="title">菜单标题</param>
        /// <param name="select">菜单选项（长度应不大于10）</param>
        /// <param name="x">菜单标题与控制台左边的距离</param>
        /// <param name="y">菜单标题与控制台上边的距离</param>
        /// <returns>选择的操作数（select 的下标）</returns>
        public int ShowMenu(String title, string[] select, int x, int y)
        {
            //选中的操作数（select的下标）
            int num = 0;
            //记录当前鼠标是否可见
            bool CurVis0 = Console.CursorVisible;
            //记录当前控制台前景色
            ConsoleColor F = Console.ForegroundColor;
            //记录当前控制台背景色
            ConsoleColor B = Console.BackgroundColor;
            //菜单项数
            int Count = select.Length;
            //设置鼠标不可见
            Console.CursorVisible = false;

            #region 遍历每行，记录所有行中最大的宽度
            //菜单标题占控制台的宽度
            int titleWidth = 0;
            //菜单选项占控制台的宽度
            int selectWidth = 0;
            //设置前景色和背景色一样
            Console.ForegroundColor = B;

            //把标题按换行符拆分成多行
            string[] titles = title.Split(new char[] { '\r', '\n' });
            foreach (string item in titles)
            {
                //设置光标位置
                Console.SetCursorPosition(x, y);
                Console.Write(item);
                //更新菜单宽度
                titleWidth = Math.Max(titleWidth, Console.CursorLeft - x);
            }

            foreach (string item in select)
            {
                //设置光标位置
                Console.SetCursorPosition(x, y);
                Console.Write(item);
                //更新菜单宽度
                selectWidth = Math.Max(selectWidth, Console.CursorLeft - x);
            }
            //清除残留文字
            Console.SetCursorPosition(x, y);
            for (int i = Math.Max(titleWidth, selectWidth); i > 0; i--)
            {
                Console.Write(" ");
            }
            //恢复前景色
            Console.ForegroundColor = F;

            #endregion

            #region 打印菜单标题
            //打印菜单标题
            foreach (string item in titles)
            {
                //设置光标列位置
                Console.CursorLeft = x;
                //打印按换行符拆分后的标题
                Console.WriteLine(item);
            }
            #endregion

            #region 输出菜单选项
            //计算使选项与菜单标题居中对齐的列位置
            x += ((titleWidth - selectWidth) / 2);
            //记录当前行位置
            y = Console.CursorTop;

            for (int i = 0; i < Count; i++)
            {
                //设置要显示的项的下标
                int j = (i + 1) % Count;

                //设置光标列位置
                Console.CursorLeft = x - Convert.ToString(j).Length;
                Console.WriteLine(j + "、" + select[j]);
            }

            #endregion

            #region 输入键来选择相应选项
            while (true)
            {
                //设置光标位置
                Console.SetCursorPosition(x, y + (num + Count - 1) % Count);
                //设置被选择项背景色
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                //设置标注符号的颜色
                Console.ForegroundColor = ConsoleColor.Red;
                //显示标注符号
                Console.Write("●");
                //设置被选择项的前景色
                Console.ForegroundColor = ConsoleColor.Yellow;
                //突出显示被选择项
                Console.Write(select[num]);

                //用户输入一个键
                ConsoleKeyInfo key = Console.ReadKey(true);

                //取消突出显示
                Console.SetCursorPosition(x - Convert.ToString(num).Length, y + (num + Count - 1) % Count);
                Console.BackgroundColor = B;
                Console.ForegroundColor = F;
                Console.Write(num + "、" + select[num]);


                //用户输入向上方向键
                if (key.Key == ConsoleKey.UpArrow)
                {
                    num = (num + Count - 1) % (Count);
                    //播放声音
                    this.PlayMusic("↑ or ↓.wav", false);
                    continue;
                }
                //用户输入向下方向键
                if (key.Key == ConsoleKey.DownArrow)
                {
                    num = (num + Count + 1) % (Count);
                    //播放声音
                    this.PlayMusic("↑ or ↓.wav", false);
                    continue;
                }
                //用户输入数字键
                if (key.KeyChar < Count + 48 && key.KeyChar >= '0')
                {
                    //播放声音
                    this.PlayMusic("num.wav", false);

                    int newNum = (int)key.KeyChar - 48;
                    for (int i = Count; i > 10; i /= 10)
                    {
                        //用户输入一个键
                        ConsoleKeyInfo k = Console.ReadKey(true);
                        //用户输入数字键
                        if (k.KeyChar < Count + 48 && k.KeyChar >= '0')
                        {
                            newNum = newNum * 10 + (int)k.KeyChar - 48;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (newNum < Count)
                        num = newNum;
                    continue;
                }
                //用户输入enter键
                if (key.Key == ConsoleKey.Enter)
                {
                    //播放声音
                    this.PlayMusic("enter.wav", false);
                    break;
                }
                //输入错误
                this.PlayMusic("KeyErr.wav", false);
            }
            #endregion

            return num;
        }
        #endregion

        #region 播放声音文件
        /// <summary>
        /// 播放波形文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="Loop">是否循环播放</param>
        public void PlayMusic(string path,bool Loop)
        {
            //设置声音文件的路径
            sp.SoundLocation = "Music\\"+path;
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
