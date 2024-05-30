using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Stats_Base
    {
        public Guid Id { get; set; }
        public Guid CharacterId { get; set; }
        public int HP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public double Critical_Rate { get; set; }
        public double Critical_Damage { get; set; }
        public double Energy_Regen { get; set; }
        public double Max_Resonance_Energy { get; set; }

        //Nav

        public Character Character { get; set; }

    }
}
