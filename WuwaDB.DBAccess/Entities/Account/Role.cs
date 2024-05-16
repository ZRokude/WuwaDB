using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuwaDB.Server.Entities.Account
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //Navigation

        public ICollection<Account_Role> AccountRoles { get; set; }
    }
}
