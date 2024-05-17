using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Skill
    {
        public Guid Id { get; set; }
        public Guid CharacterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation

        public Character Character { get; set; }
        public ICollection<Character_Skill_Value> CharacterSkillValues { get; set; }
    }
}
