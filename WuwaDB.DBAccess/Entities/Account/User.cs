using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.Server.Entities.Account
{
    public class User : Account
    {
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
