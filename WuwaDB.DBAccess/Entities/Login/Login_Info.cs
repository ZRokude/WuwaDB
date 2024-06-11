using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Login
{
    public class Login_Info
    {
        public int Id { get; set; }
        public Guid LoginUrl { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
