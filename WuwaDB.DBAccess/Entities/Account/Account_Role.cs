using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Account
{
    public class Account_Role
    {
        public Guid AccountId { get; set; }
        public Guid RoleId { get; set; }

        //Navigation

        public Account Account { get; set; }
        public Role Role { get; set; }
    }
}
