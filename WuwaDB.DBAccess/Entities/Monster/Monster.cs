using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Monster
{
    public class Monster
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public byte[]? ImageFile { get; set; }
        public string? ImageName { get; set; }

        //Nav
        public ICollection<Monster_Stats_Base> MonsterStatBases { get; set; }

    }
}
