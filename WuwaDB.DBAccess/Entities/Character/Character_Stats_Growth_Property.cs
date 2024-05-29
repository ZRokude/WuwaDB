using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Stats_Growth_Property
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int BreachLevel {  get; set; }
        public int LifeMaxRatio { get; set; }
        public int AtkRatio { get; set; }
        public int DefRatio { get; set; }
    }
}
