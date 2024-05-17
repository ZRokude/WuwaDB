using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Skill_Value
    {
        public Guid Id { get; set; }
        public Guid CharacterSkillId { get; set; }
        public int SkillValue { get; set; }
        public int SkillValueNumber { get; set; }

        //Navigation

        public Character_Skill CharacterSkill { get; set; }

    }
}
