using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 控制台RPG游戏
{
    class Battle
    {
        private static Monster monster;
        private static Map currentMap;


        public enum STATE
        {
            DEAD = -1,
            DRAW = 0,
            VECTORY = 1,
            ESCAPE = 2,
            ERROR = 3,
            OTHER = 4,
        }

        public static void Start(int monsterId,Map map)
        {
            currentMap = map;
            monster = GameRes.GetMonsterById(monsterId).Clone();
            if (monster != null)
            {
                StartBattle();
            }
        }

        private static void StartBattle()
        {
            STATE state = STATE.OTHER;
            while (true)
            {
                PrintInfo();
                Console.WriteLine();
                Console.WriteLine("输入指令：1.攻击 2.逃跑");
                string cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "1":
                        state = DoAttack();
                        break;
                    case "2":
                        state = STATE.ESCAPE;
                        break;

                    default:
                        state = STATE.ERROR;
                        Console.WriteLine("无效指令。");
                        break;
                }

                if (state != STATE.DRAW && state != STATE.ERROR)
                {
                    BattleResult(state);
                    break;
                }
            }
        }

        private static void PrintInfo()
        {
            Console.WriteLine();
            Console.WriteLine("\t{0}\t\t{1}",PlayerModel.Instance.name,monster.name);
            Console.WriteLine("攻击力{0}   vs  {1}",PlayerModel.Instance.attack,monster.atk);

            Console.WriteLine("生命值{0}\t\t{1}",PlayerModel.Instance.hp,monster.hp);
        }

        private static STATE DoAttack()
        {
            int damage = GetRoleDamage();
            monster.hp -= damage;
            Console.WriteLine("你对{0}进行攻击，造成{1}点伤害。",monster.name,damage);

            if (monster.hp <= 0)
            {
                return STATE.VECTORY;
            }

            PlayerModel.Instance.hp += GetRecover();
            damage = GetMonsterDamage();
            PlayerModel.Instance.hp -= damage;
            Console.WriteLine("{0}打了你一下，造成{1}点伤害",monster.name,damage);
            
            if(PlayerModel.Instance.hp <= 0)
            {
                return STATE.DEAD;
            }
            return STATE.DRAW;
        }

        private static int GetRoleDamage()
        {
            if(PlayerModel.Instance.jobId == 2)
            {
                int num = Program.Random.Next(101);

                if (num <= PlayerModel.Instance.percent)
                {
                    Console.WriteLine("技能{0}触发。", PlayerModel.Instance.skillName);
                    return PlayerModel.Instance.attack * 2;

                }
            }
            return PlayerModel.Instance.attack;
        }

        private static int GetMonsterDamage()
        {
            if (PlayerModel.Instance.jobId == 1)
            {
                int num = Program.Random.Next(101);
                if (num <= PlayerModel.Instance.percent)
                {
                    Console.WriteLine("技能{0}触发。", PlayerModel.Instance.skillName);

                    return 0;

                }
            }

            return monster.atk;
        }

        private static int GetRecover()
        {
            if(PlayerModel.Instance.jobId == 3)
            {
                int num = Program.Random.Next(101);
                if (num <= PlayerModel.Instance.percent)
                {
                    int recover = PlayerModel.Instance.attack;
                    recover = recover <= monster.hp ? recover : monster.hp;

                    Console.WriteLine("技能{0}触发，回复生命值{1}。",PlayerModel.Instance.skillName,recover);

                    return recover;
                }
            }

            return 0;


        }

        private static void BattleResult(STATE result)
        {
            Console.Clear();
            switch (result)
            {
                case STATE.DEAD:
                    Console.WriteLine("系统提示：你已经死亡，游戏结束。");
                    Environment.Exit(0);
                    break;
                case STATE.VECTORY:
                    Console.WriteLine("系统提示：战斗胜利!");
                    Console.WriteLine("系统提示：获得金币{0}", monster.gold);
                    PlayerModel.Instance.gold += monster.gold;
                    if (monster.name == "巴尔")
                    {
                        Console.WriteLine("打败了最终的boss，游戏结束。");
                        Environment.Exit(0);
                    }
                    break;
                case STATE.ESCAPE:
                    Console.WriteLine("系统提示：成功逃脱。");
                    PlayerModel.Instance.ReduceGold();
                    GameHelper.MoveTo(currentMap);
                    break;
            }
        }
    }
}
