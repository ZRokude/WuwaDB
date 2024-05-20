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
using BC = BCrypt.Net.BCrypt;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="additionalProp"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task CreateUserDataAsync(string username, string password, string role, string[] additionalProp)
        {
            int i = 0;
            //Create DbContext of WuwaDBContext
            await using WuwaDbContext context = await _context.CreateDbContextAsync();
            
            //Checking if account has already there if yes then decline the function
            var existingAccount = await context.Accounts
                .FirstOrDefaultAsync(u => u.Username == username);
            // Find or create the new role
            var existingRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == role);
            if (existingRole == null)
            {
                existingRole = new Role { Name = role };
                await context.Roles.AddAsync(existingRole);
            }
            //Get the entitytype based of role name
            var entityType = Type.GetType($"WuwaDB.DBAccess.Entities.Account.{existingRole.Name}");
            if (entityType == null)
            {
                throw new ArgumentException($"No entity model found for role name: {existingRole.Name}");
            }
            //Create instance of inherited Account entity based on entityType
            var account = (Account)Activator.CreateInstance(entityType);
            //Add the inherited class value 
            account.Username = username;
            account.RoleId = existingRole.Id;
            account.Password = BC.EnhancedHashPassword(password, 10);
            var dbSetName = existingRole.Name;
            //Add the additional class value from derrived class
            foreach (var prop in entityType.GetProperties())
            {
                if (prop.Name != nameof(Account.Username) &&
                    prop.Name != nameof(Account.RoleId) &&
                    prop.Name != nameof(Account.Password))
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(account, additionalProp[i++].ToString());
                    }
                    if (prop.PropertyType == typeof(bool))
                    {
                        if (bool.TryParse(additionalProp[i++], out bool boolValue))
                            prop.SetValue(account, boolValue);
                    }
                    if (prop.PropertyType == typeof(int))
                    {
                        if (int.TryParse(additionalProp[i++], out int intValue))
                            prop.SetValue(account, intValue);
                    }
                }
            }
            // Add the new account to the DbSet
            await context.AddAsync(account);
            await context.SaveChangesAsync();


        }
        public async Task<List<Character>> GetCharacterAsync()
        {
            //Create DbContext of WuwaDBContext
            await using WuwaDbContext context = await _context.CreateDbContextAsync();
            return await context.Characters.ToListAsync(); 
        }

    }
}
