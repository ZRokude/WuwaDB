using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuwaDB.DBAccess.DataContext;
using WuwaDB.DBAccess.Entities.Character;

namespace WuwaDB.DBAccess.Repository
{
    public class AdminRepository
    {
        private readonly IDbContextFactory<WuwaDbContext> _context;

        public AdminRepository(IDbContextFactory<WuwaDbContext> context)
        {
            _context = context;
        }
        
        public async Task CreateCharacter(Character character)
        {
            //Create DbContext of WuwaDBContext
            await using WuwaDbContext context = await _context.CreateDbContextAsync();

            await context.AddAsync(character);
            await context.SaveChangesAsync();
        }
    }
}
