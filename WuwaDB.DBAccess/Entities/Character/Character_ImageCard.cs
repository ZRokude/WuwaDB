using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Character
{
    public class Character_ImageCard
    {
        public Guid CharacterId { get; set; }

        public byte[] Image { get; set; }

        //Nav

        public Character Character { get; set; }    
    }
}
