using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.DBAccess.Entities.Account
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //Navigation
        public ICollection<Account> Accounts { get; set; }
    }
}
