using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using WuwaDB.DBAccess.Enum;

namespace WuwaDB.DBAccess.Entities.Monster
{
    public class Monster_Stats_Base
    {
        public Guid Id { get; set; }
        public Guid MonsterId { get; set; }
        public int HP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int Hardness { get; set; }
        public int Rage { get; set; }

        //Nav
        public ICollection<Monster_Stats_Base_Ele_Res> MonsterStatsBaseEleRes { get; set; }
        public Monster Monster { get; set; }
    }
}
