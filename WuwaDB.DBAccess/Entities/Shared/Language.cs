using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuwaDB.DBAccess.Entities.Character;

namespace WuwaDB.DBAccess.Entities.Shared
{
    public class Language
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }

        //Navigation

        public ICollection<VoiceActor> VoiceActors { get; set;}
    }
}
