using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuwaDB.DBAccess.Entities.Shared;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class VoiceActor
    {
        public Guid Id { get; set; }
        public Guid LanguageId { get; set; }
        public Guid CharacterId { get; set; }
        public string Name { get; set; }
        
        //Navigation

        public Language Language { get; set; }
        public Character Character { get; set; }    

    }
}
