using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Skill_Perform_Level
    {
        public Guid Id { get; set; }
        public Guid CharacterSkillPerformId { get; set; }
        public int Level { get; set; }
        public double Value { get; set; }

        //Navigation
        public Character_Skill_Perform CharacterSkillPerform{ get; set; }
    }
}
