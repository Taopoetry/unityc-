using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 控制台RPG游戏
{
    class Shop
    {
        public static void ShopEntry()
        {
            int i = 控制台RPG游戏.Program.Random.Next(1, 4);
            switch (i)
            {
                case 1:
                    Console.Write("欢迎光临，异界的勇士。");
                    break;
                case 2:
                    Console.Write("你需要点什么？");
                    break;
                case 3:
                    Console.Write("哈哈，很高兴又见到你了。");
                    break;
            }
            shop();
        }

        public static void BuyWeapon()
        {
            foreach (Weapon wp in GameRes.Weapons.Values)
            {
                Console.WriteLine("{0},{1,5}\t\t攻击力 + {2}\t\t${3}\n",wp.id,wp.name,wp.atk,wp.price);

            }

            Console.WriteLine("请输入商品编号：");

            try
            {
                int i = Convert.ToInt16(Console.ReadLine());
                Weapon wu = GameRes.GetWeaponById(i);
                if(wu.price <= PlayerModel.Instance.gold)
                {
                    Console.Clear();
                    Console.WriteLine("已购入{0}", wu.name);
                    PlayerModel.Instance.attack += wu.atk;
                    PlayerModel.Instance.gold -= wu.price;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("钱不够");
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("本店只有这些货物。");
            }
        }

        public static void BuyMedicine()
        {
            foreach (Medicine mc in GameRes.Medicines.Values)
            {
                Console.WriteLine("{0}.{1,5}\t\thp + {2}\t\t${3}\n",mc.id,mc.name,mc.hp,mc.price);    
            }
            Console.WriteLine("请输入编号：");

            try
            {
                int i = Convert.ToInt16(Console.ReadLine());
                Medicine med = GameRes.GetMedicineById(i);
                if(med.price <= PlayerModel.Instance.gold)
                {
                    Console.Clear();
                    Console.WriteLine("已购入{0}", med.name);
                    PlayerModel.Instance.hp += med.hp;
                    PlayerModel.Instance.gold -= med.price;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("钱不够");
                }
            } 
            catch
            {
                Console.Clear();
                Console.WriteLine("本店只有这些货物。");
            }
        }

        static void shop()
        {
            Console.WriteLine("\t\t\t\t金币:{0}", PlayerModel.Instance.gold);
            Console.WriteLine();
            Console.WriteLine("1.力量强化药剂\t\t2.生命强化药剂\t\t3.退出商店");

            string chioce = Console.ReadLine();

            switch (chioce)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("**********力量强化药剂************\t\t金币{0}", PlayerModel.Instance.gold);
                    Console.WriteLine();
                    BuyWeapon();
                    shop();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("**********生命强化药剂************\t\t金币{0}", PlayerModel.Instance.gold);
                    Console.WriteLine();
                    BuyMedicine();
                    shop();
                    break;
                case "3":
                    ShopExit();
                    break;
                default:
                    Console.WriteLine("没有这个选项哦。");
                    shop();
                    break;
            }
        }

        static void ShopExit()
        {
            Console.Clear();
            Console.WriteLine("去吧，勇士，与魔物们决一死战");
            GameHelper.City();
        }

    }
}
