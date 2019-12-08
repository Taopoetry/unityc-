using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 控制台RPG游戏
{
    class PlayerModel
    {
        private static PlayerModel instance = null;
        
        public static PlayerModel Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new PlayerModel();

                }

                return instance;
            }
        }

        public int attack;
        public int hp;
        public int gold;
        public string name;
        public int percent;
        public int jobId;
        public int lv;
        public int mp;

        public string skillName;

        public void ReduceGold()
        {
            this.gold = this.gold / 2;
        }

        public void SetJob(Job job)
        {
            this.jobId = job.id;
            this.name = job.name;
            this.hp = job.hp;
            this.percent = job.percent;
            this.skillName = job.skill;
            this.lv = job.lv;
            this.mp = job.mp;
        }
    }
}
