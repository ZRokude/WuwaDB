using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Skill_Perform
    {
        public Guid Id { get; set; }
        public Guid CharacterSkillId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        //Nav
        public Character_Skill CharacterSkill { get; set; }
        public ICollection<Character_Skill_Perform_Level> CharacterSkillPerformLevels { get; set; }

    }
}
