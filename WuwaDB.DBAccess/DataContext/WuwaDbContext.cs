
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
        public DbSet<User> Users { get; set; }
        public DbSet<Admin>Admins { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Character_Skill> CharacterSkills { get; set; }
        public DbSet<Character_Stats_Base> CharacterStatsBases { get; set; }
        public DbSet<Character_Skill_Detail> CharacterSkillDetails { get; set; }
        public DbSet<Character_Stats_Growth_Property> CharacterStatsGrowthproperties { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .ToTable("Accounts")
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Admin>("Admin")
                .HasValue<User>("User");

            modelBuilder.Entity<Account>()
                .HasIndex(c => c.Username);
            modelBuilder.Entity<User>()
                .HasIndex(c => c.Email);
            modelBuilder.Entity<Character>()
                .HasIndex(c => c.Name);
            modelBuilder.Entity<Character_Skill>()
                .HasIndex(c => c.Name);

            //Relations
            modelBuilder.Entity<Character_Skill_Detail>()
                .HasOne(c => c.Character_Skill)
                .WithMany(u => u.Character_Skill_Details)
                .HasForeignKey(c => c.CharacterSkillId);




        }

    }
}
