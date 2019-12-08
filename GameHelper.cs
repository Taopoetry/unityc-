using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 控制台RPG游戏
{
    class GameHelper
    {
        private static string name;

        public static void StartGame()
        {
            string flag;
            //string story = "几千年前，正当天阳一族内乱时，弱小的魔族却大举进攻。\n\n面对弱小的魔族，天阳一族未对其上心，使魔族快速发展起来，成为天阳的大敌。\n\n随后，魔族攻城略地，占领了大半个天阳世界\n\n而内乱的天阳一族现在才醒悟过来，可惜大势已成。\n\n千钧一发之际，天阳大先知为保天阳族，只能与魔皇死战，最后魔皇不得不答应天阳大先知不屠杀天阳族，留天阳一条生路，但条件是大先知自尽。。。\n\n大先知临死前，传下一言：本是异界客，却来此人生。让族人好好体会。。。\n\n林北本是地球上一名孤儿，一名普普通通的大学生，\n\n一次机遇让他穿越到了异界，成为了大先知所预言的异界勇士。\n\n并且携带着天阳修炼系统，氪金无敌，带领人族打败魔族。\n\n林北：你们统统别想跑！死亡如风，常伴汝身！\n\n";
            //foreach (char s in story)
            //{

            //    Console.Write(s);
            //    Thread.Sleep(100);
            //}

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("\t叮咚，欢迎来到天阳世界。");
            Console.WriteLine();

            Console.WriteLine("我是系统携带的人工智能，很高兴为您服务。");

            do
            {
                Console.WriteLine();

                Console.WriteLine("系统提示：请选择吧");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("1.开始游戏");
                Console.WriteLine();
                Console.WriteLine("2.退出游戏");
                Console.ResetColor();
                Console.WriteLine();

                flag = Console.ReadLine();
                Console.Clear();

                switch (flag)
                {
                    case "1":
                        JobChoose();
                        break;
                    case "2":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("系统提示：输入有误。");
                        break;
                }
            } while (true);

        }

        static void JobChoose()
        {
            Console.WriteLine("系统提示：请选择角色：");

            do
            {
                Console.WriteLine();
                Console.WriteLine("\t职业\t技能");
                Console.WriteLine();
                foreach (Job jb in GameRes.Jobs.Values)
                {
                    Console.WriteLine("{0}\t{1}\t{2}:{3}\n",jb.id,jb.career,jb.skill,jb.tip);
                    Console.WriteLine();
                }

                int flag = int.Parse(Console.ReadLine());

                Getname();

                Console.Clear();

                Job j = GameRes.GetJobById(flag);

                if (j != null)
                {
                    PlayerModel.Instance.SetJob(j);
                    CityNew();
                }
                else
                {
                    Console.Clear();
                    string oldManSay2 = "勇士你在说什么？我不懂。\n\n";

                    foreach (char s2 in oldManSay2)
                    {
                        Console.Write(s2);
                        Thread.Sleep(80);
                    }
                }

            } while (true);
        }

        static void Getname()
        {
            Console.WriteLine("请输入你的名字。");
            name = Console.ReadLine();
            PlayerModel.Instance.name = name;
        }

        static void CityNew()
        {
            string OldManSay = "异界的勇士啊，\n\n欢迎来到天阳世界，\n\n我是华西村的村长。\n\n";
            Console.Write("华西村村长：");
            foreach (char s in OldManSay)
            {
                Console.Write(s);
                Thread.Sleep(80);
            }

            City();
        }

        public static void City()
        {
            string flag;

            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("华西村");

                Console.WriteLine();

                Console.WriteLine("系统提示：请选择吧");

                Console.WriteLine();
                Console.WriteLine("1.我的属性：");
                Console.WriteLine();
                Console.WriteLine("2.我要消灭怪物。");
                Console.WriteLine();
                Console.WriteLine("3.我要买东西。");

                Console.WriteLine();
                Console.WriteLine("村长：");
                Console.ResetColor();
                flag = Console.ReadLine();

                Console.Clear();

                switch (flag)
                {
                    case "1":
                        MyAttribute();
                        break;
                    case "2":
                        Map();
                        break;
                    case "3":
                        Shop.ShopEntry();
                        break;
                    case "-gold":
                        PlayerModel.Instance.gold += 9999;
                        break;
                    case "-HP" :
                        PlayerModel.Instance.hp += 9999;
                        break;
                    case "-ATT":
                        PlayerModel.Instance.attack += 9999;
                        break;
                    default:
                        Console.WriteLine("奇怪了，这是什么？");
                        break;
                }


            } while (true);
        }

        public static void MyAttribute()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PlayerModel.Instance.name = name;
            Console.WriteLine("姓名："+PlayerModel.Instance.name);
            Console.WriteLine();
            Console.WriteLine("等级："+PlayerModel.Instance.lv);
            Console.WriteLine();
            Console.WriteLine("血量："+PlayerModel.Instance.hp);
            Console.WriteLine();
            Console.WriteLine("魔法值：" + PlayerModel.Instance.mp);
            Console.WriteLine();
            Console.WriteLine("攻击力："+ PlayerModel.Instance.attack);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("金钱："+PlayerModel.Instance.gold);
            Console.ResetColor();

            Console.WriteLine("任意键退出属性。");

            string flag = Console.ReadLine();

            Console.Clear();

            if(flag != null)
            {
                City();
            }
        }

        public static void Map()
        {
            Console.WriteLine("天阳世界：");
            foreach (Map mp in GameRes.Maps.Values)
            {
                Console.WriteLine("{0}.{1}", mp.id, mp.name);
            }

            int flag = int.Parse(Console.ReadLine());

            Console.Clear();

            Map m = GameRes.GetMapById(flag);

            if (m != null)
            {
                MoveTo(m);
            }
            else
            {
                Console.WriteLine("这块地图已经被魔物占领。");
            }
        }

        public static void MoveTo(Map mp)
        {
            int flag;

            do
            {
                Console.WriteLine(mp.name);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("当前血量："+PlayerModel.Instance.hp);
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("当前魔法值："+PlayerModel.Instance.mp);
                Console.WriteLine();

                Console.WriteLine("当前的攻击力："+PlayerModel.Instance.attack);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("当前的金币："+PlayerModel.Instance.gold);
                Console.ResetColor();
                Console.WriteLine();

                Console.WriteLine("\t1.向前走");
                Console.WriteLine("2.向左走\t3.向右走");
                Console.WriteLine("\t4.返回村庄。");

                flag = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (flag)
                {
                    case 1:
                        RandomMonster(mp);
                        break;
                    case 2:
                        RandomMonster(mp);
                        break;
                    case 3:
                        RandomMonster(mp);
                        break;
                    case 4:
                        City();
                        break;
                    default:
                        Console.WriteLine("你好像按错了。");
                        break;
                }
            } while (flag != 4);
        }


        static void RandomMonster(Map mp)
        {
            Random random = new Random();

            int randomMonster = random.Next(1, 11);

            switch (randomMonster)
            {
                case 1:
                case 2:
                    Console.WriteLine();
                    Console.WriteLine("什么都没有发生。");
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                    Console.WriteLine();
                    Console.WriteLine("魔物出现啦。");
                    Battle.Start(mp.monster1,mp);
                    break;
                case 7:

                    break;
                case 8:
                    Console.WriteLine();
                    Console.WriteLine("大魔物出现啦。");
                    Battle.Start(mp.monster2,mp);
                    break;
                case 9:
                    Console.WriteLine();
                    Console.WriteLine("发现宝物啦。");
                    int randomTreasure = random.Next(0, 2);
                    switch (randomTreasure)
                    {
                        case 0:
                            int randomHP = random.Next(1, 4);
                            PlayerModel.Instance.hp += randomHP;
                            Console.WriteLine("HP增加" + randomHP);
                            break;
                        case 1:
                            int randomATT = random.Next(1, 3);
                            PlayerModel.Instance.attack += randomATT;
                            Console.WriteLine("攻击力增加" + randomATT);
                            break;                    
                    }
                    Console.WriteLine();
                    break;
                case 10:
                    int randomGold = random.Next(1, 5);
                    PlayerModel.Instance.gold += randomGold;
                    Console.WriteLine();
                    Console.WriteLine("捡到{0}金", randomGold);
                    break;
                default:
                    break;
            }
        }
    }
}
