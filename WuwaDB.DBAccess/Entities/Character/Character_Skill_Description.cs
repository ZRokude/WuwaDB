using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Skill_Description
    {
        public Guid CharacterSkillId { get; set; }
        public string DescriptionTitle { get; set; }
        public string Description { get; set; }

        //Nav
        public Character_Skill Character_Skill { get; set; }
    }
}
