using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 控制台RPG游戏
{
    class Program
    {

        public static Random Random = new Random();

        static void Main(string[] args)
        {
            

            if (GameRes.LoadGameRes())
            {
                GameHelper.StartGame();

            }
            else
            {
                Console.WriteLine("游戏载入失败");
            }
        }
    }
}
