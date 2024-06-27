using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuwaDB.DBAccess.Enum;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Skill_Image
    {
        public Guid CharacterSkillId { get; set; }
        public SkillType Type { get; set; }
        public byte[] Image { get; set; }

        //Nav
        public Character_Skill CharacterSkill { get; set; }
    }
}
