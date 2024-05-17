using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using WuwaDB.DBAccess.DataContext;
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Character;
using System.Security.Principal;
using System.Reflection;
using MudBlazor.Interfaces;

namespace WuwaDB.DBAccess.Repository
{
    public class UserRepository
    {
        private readonly IDbContextFactory<WuwaDbContext> _context;
       
        public UserRepository(IDbContextFactory<WuwaDbContext> context)
        {
            _context = context;
        }
        //public async Task<Character> GetCharacterAsync()
        //{
        //    var dbContext = await _context.CreateDbContextAsync();

        //    return await dbContext.Characters.FindAsync();
        //}
        public async Task<Account?> GetUserDataAsync(string Username)
        {
            await using WuwaDbContext context = await _context.CreateDbContextAsync();
            return await context.Accounts.Include(c => c.Role).FirstOrDefaultAsync(x => x.Username == Username);
        }

        public async Task CreateUserDataAsync(string username, string password, string role, string email, bool emailConfirmed)
        {
            await using WuwaDbContext context = await _context.CreateDbContextAsync();
            
            var existingAccount = await context.Accounts
                .FirstOrDefaultAsync(u => u.Username == username);
            // Find or create the new role
            var existingRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == role);
            if (existingRole == null)
            {
                existingRole = new Role { Name = role };
                await context.Roles.AddAsync(existingRole);
            }
            var entityType = Type.GetType($"WuwaDB.DBAccess.Entities.Account.{existingRole.Name}");
            if (entityType == null)
            {
                throw new ArgumentException($"No entity model found for role name: {existingRole.Name}");
            }
            var account = (Account)Activator.CreateInstance(entityType);
            account.Username = username;
            account.RoleId = existingRole.Id;
            account.Password = password;
            var dbSetName = existingRole.Name + "s";
            var dbSet = context.Set<Account>(dbSetName);
            dbSet.AddAsync(account);
            await context.SaveChangesAsync();
           

        }


    }
}
