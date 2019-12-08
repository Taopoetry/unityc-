using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 控制台RPG游戏
{
    public class Weapon
    {
        public int id;
        public string name;
        public int atk;
        public int price;
    };

    public class Medicine
    {
        public int id;
        public string name;
        public int hp;
        public int price;
    };

    public class Job
    {
        public int id;
        public string career;
        public string name;
        public int hp;
        public int atk;
        public string skill;
        public string tip;
        public int percent;
        public int lv;
        public int mp;
    };

    public  class Monster
    {
        public int id;
        public string name;
        public int hp;
        public int atk;
        public int gold;

        public Monster Clone()
        {
            Monster temp = new Monster();

            temp.id = this.id;
            temp.name = this.name;
            temp.hp = this.hp;
            temp.atk = this.atk;
            temp.gold = this.gold;
            return temp;
        }
    };

    public  class Map
    {
        public int id;
        public string name;
        public int monster1;
        public int monster2;
    };

    class GameRes
    {
        public static Dictionary<int, Weapon> Weapons = new Dictionary<int, Weapon>();
        public static Dictionary<int, Medicine> Medicines = new Dictionary<int, Medicine>();
        public static Dictionary<int, Monster> Monsters = new Dictionary<int, Monster>();
        public static Dictionary<int, Map> Maps = new Dictionary<int, Map>();
        public static Dictionary<int, Job> Jobs = new Dictionary<int, Job>();

        private const string ResPath = "../../config.xml";

        public static bool LoadGameRes()
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                xml.Load(ResPath);

                XmlNode root = xml.SelectSingleNode("config");
                XmlNode role = root.SelectSingleNode("hero");

                PlayerModel.Instance.hp = int.Parse(role.Attributes["hp"].Value);
                PlayerModel.Instance.gold = int.Parse(role.Attributes["gold"].Value);

                XmlNodeList list = root.SelectNodes("weapon");

                foreach (XmlNode nd in list)
                {
                    Weapon wp = new Weapon();

                    wp.id = int.Parse(nd.Attributes["id"].Value);
                    wp.name = nd.Attributes["name"].Value;
                    wp.atk = int.Parse(nd.Attributes["attack"].Value);
                    wp.price = int.Parse(nd.Attributes["price"].Value);

                    Weapons.Add(wp.id, wp);
                }

                list = root.SelectNodes("medicine");


                foreach (XmlNode nd in list)
                {
                    Medicine md = new Medicine();

                    md.id = int.Parse(nd.Attributes["id"].Value);
                    md.name = nd.Attributes["name"].Value;
                    md.hp = int.Parse(nd.Attributes["hp"].Value);
                    md.price = int.Parse(nd.Attributes["price"].Value);

                    Medicines.Add(md.id, md);
                }

                list = root.SelectNodes("monster");


                foreach (XmlNode nd in list)
                {
                    Monster mt = new Monster();

                    mt.id = int.Parse(nd.Attributes["id"].Value);
                    mt.name = nd.Attributes["name"].Value;
                    mt.hp = int.Parse(nd.Attributes["hp"].Value);
                    mt.atk = int.Parse(nd.Attributes["atk"].Value);
                    mt.gold = int.Parse(nd.Attributes["gold"].Value);

                    Monsters.Add(mt.id, mt);
                }

                list = root.SelectNodes("map");

                foreach (XmlNode nd in list)
                {
                    Map mp = new Map();

                    mp.id = int.Parse(nd.Attributes["id"].Value);
                    mp.name = nd.Attributes["name"].Value;
                    string monsterStr = nd.Attributes["monster"].Value;
                    string[] mArr = monsterStr.Split(',');
                    mp.monster1 = int.Parse(mArr[0]);
                    mp.monster2 = int.Parse(mArr[1]);

                    Maps.Add(mp.id, mp);
                }

                list = root.SelectNodes("job");

                foreach (XmlNode nd in list)
                {
                    Job jb = new Job();

                    jb.id = int.Parse(nd.Attributes["id"].Value);
                    jb.career = nd.Attributes["career"].Value;
                    jb.name = nd.Attributes["name"].Value;
                    jb.hp = int.Parse(nd.Attributes["hp"].Value);
                    jb.atk = int.Parse(nd.Attributes["atk"].Value);
                    jb.skill = nd.Attributes["skill"].Value;
                    jb.tip = nd.Attributes["tip"].Value;
                    jb.percent = int.Parse(nd.Attributes["percent"].Value);

                    Jobs.Add(jb.id, jb);
                }
            }
            catch
            {

                return false;
            }

            return true;
        }

        public static Weapon GetWeaponById(int id)
        {
            if(Weapons.ContainsKey(id))
            {
                return Weapons[id];
            }

            return null;
        }

        public static Medicine GetMedicineById(int id)
        {
            if (Medicines.ContainsKey(id))
            {
                return Medicines[id];
            }

            return null;
        }

        public static Monster GetMonsterById(int id)
        {
            if (Monsters.ContainsKey(id))
            {
                return Monsters[id];
            }

            return null;
        }

        public static Map GetMapById(int id)
        {
            if (Maps.ContainsKey(id))
            {
                return Maps[id];
            }

            return null;
        }

        public static Job GetJobById(int id)
        {
            if (Jobs.ContainsKey(id))
            {
                return Jobs[id];
            }

            return null;
        }
    }
}
