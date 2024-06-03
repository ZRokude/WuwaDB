using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuwaDB.DBAccess.DataContext;
using WuwaDB.DBAccess.Entities.Character;
using System.Dynamic;
using WuwaDB.DBAccess.Entities.Account;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Components;
namespace WuwaDB.DBAccess.Repository
{
    public class AdminRepository
    {
        [Inject] SharedRepository ShareRepository { get; set; } = new();
        [Inject] UserRepository UserRepository { get; set; }
        private readonly IDbContextFactory<WuwaDbContext> _context;

        public AdminRepository(IDbContextFactory<WuwaDbContext> context)
        {
            _context = context;
        }
        /// <summary>
        /// its method function where you can SaveChangesAsync for every model you have
        /// </summary>
        /// <typeparam name="T">T is mean to know what entity class that has been passed </typeparam>
        /// <param name="entity">entity is an abstract class property before the parameter being passed</param>
        /// <returns></returns>
        public async Task SavesAsync<T>(T entity) where T:class
        {
            await using WuwaDbContext context = await _context.CreateDbContextAsync();
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task UpdatesAsync<T>(T entity) where T : class
        {
            await using WuwaDbContext context = await _context.CreateDbContextAsync();
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
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
    }
}
