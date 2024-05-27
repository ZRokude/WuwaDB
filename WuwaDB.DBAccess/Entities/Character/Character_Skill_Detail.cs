using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Skill_Detail
    {
        public Guid Id { get; set; }

        public Guid CharacterSkillId { get; set; }

        public string[] SkillDetailsNum { get; set; }
        public string[] SkillDetailsName { get; set; }

        //Nav
        public Character_Skill Character_Skill { get; set; }
    }
}
