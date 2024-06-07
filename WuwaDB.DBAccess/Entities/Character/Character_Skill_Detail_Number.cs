using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Skill_Detail_Number
    {
        public Guid CharacterSkillDetailId { get; set; }
        public int Level { get; set; }
        public double Number { get; set; }
        public int? Multiplier { get; set; }

        //Nav
        public Character_Skill_Detail Character_Skill_Detail { get; set; }
       

    }
}
