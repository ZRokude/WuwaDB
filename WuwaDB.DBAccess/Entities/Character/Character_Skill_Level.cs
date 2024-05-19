using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Skill_Level
    {
        public Guid Id { get; set; }
        public Guid SkillPerformedId { get; set; }
        public int SkillLevel { get; set; }
        public int SkillPercentage { get; set; }

        //Navigation

        public Character_Skill_Perform CharacterSkillPerformed { get; set; }

    }
}
