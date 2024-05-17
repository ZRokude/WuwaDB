using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuwaDB.DBAccess.DataContext;
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Character;

namespace WuwaDB.DBAccess.Repository
{
    public class UserRepository
    {
        private readonly IDbContextFactory<WuwaDbContext> _context;
       
        public UserRepository(IDbContextFactory<WuwaDbContext> context)
        {
            _context = context;
        }
        public async Task<Character> GetCharacterAsync()
        {
            var dbContext = await _context.CreateDbContextAsync();

            return await dbContext.Characters.FindAsync();
        }
        public async Task<Account?> GetUserDataAsync(string Username)
        {
            await using WuwaDbContext context = await _context.CreateDbContextAsync();
            return await context.Accounts.Include(c=>c.Roles).FirstOrDefaultAsync(x => x.Username == Username);

            
        }

    }
}
