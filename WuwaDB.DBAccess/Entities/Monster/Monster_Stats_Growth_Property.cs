using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WuwaDB.DBAccess.Entities.Monster
{
    public class Monster_Stats_Growth_Property
    {
        [Key]
        public int Id { get; set; }
        public int Level { get; set; }
        public int LifeMaxRatio { get; set; }
        public int AtkRatio { get; set; }
        public int DefRatio { get; set; }
        public int HardnessMaxRatio { get; set; }
        public int HardnessRatio { get; set; }
        public int HardnessRecoverRatio { get; set; }
        public int RageMaxRatio { get; set; }
        public int RageRatio { get; set; }
        public int RageRecoverRatio { get; set; }
    }
}
