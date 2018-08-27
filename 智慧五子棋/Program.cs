using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 智慧五子棋
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建设置控制台样式的对象
            ConsoleStyle ConSty = new ConsoleStyle();
            //开始时的控制台样式
            ConSty.Start();

            Console.WriteLine("\n按任意键退出。。。");
            Console.ReadKey();
        }
    }
}
