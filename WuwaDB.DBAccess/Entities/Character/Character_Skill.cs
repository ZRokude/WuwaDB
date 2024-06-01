using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuwaDB.DBAccess.Enum;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_Skill
    {
        public Guid Id { get; set; }
        public Guid CharacterId { get; set; }
        public string Name { get; set; }
        public SkillType Type { get; set; }
        public string DescriptionName { get; set; }
        public string Description {  get; set; }
        public string? ImageName { get; set; }
        public byte[]? ImageFile { get; set; }
        // Navigation
        public Character Character { get; set; }
        public ICollection<Character_Skill_Detail> Character_Skill_Details { get; set; }
    }
}
