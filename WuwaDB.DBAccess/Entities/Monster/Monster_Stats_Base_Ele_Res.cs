using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuwaDB.DBAccess.Enum;

namespace WuwaDB.DBAccess.Entities.Monster
{
    public class Monster_Stats_Base_Ele_Res
    {
        public Guid MonsterStatsBaseId { get; set; }
        public ElementType ElementResist { get; set; }
        public double Number { get; set; }

        //Nav
        public Monster_Stats_Base Monster_Stats_Base { get; set; }
    }
}
