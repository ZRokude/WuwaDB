
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Character;

namespace WuwaDB.DBAccess.DataContext
{
    public class WuwaDbContext : DbContext
    {
        
        public WuwaDbContext(DbContextOptions<WuwaDbContext> options)
            : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account_Role> AccountRoles { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Character_Skill> CharacterSkills { get; set; }
        public DbSet<Character_Skill_Value> CharacterSkillValues { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .ToTable("Accounts")
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Admin>("Admin")
                .HasValue<User>("User");

            modelBuilder.Entity<Account_Role>()
                .HasKey(c => new
                {
                    c.AccountId,
                    c.RoleId
                });
        }
        
    }
}
