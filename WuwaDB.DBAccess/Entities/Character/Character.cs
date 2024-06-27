using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuwaDB.DBAccess.Enum;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ElementType Element { get; set; }
        public RarityType Rarity { get; set; }
        public WeaponType Weapon { get; set; }
        //Nav
        public ICollection<Character_Skill> CharacterSkills { get; set; }
        public ICollection<VoiceActor> VoiceActors { get; set; }
        public ICollection<Character_Stats_Base> CharacterStatBases { get; set; }
        public ICollection<Character_ImageCard> CharacterImageCards { get; set; }
        public ICollection<Character_ImageModel> CharacterImageModels { get; set; } 

    }
}
