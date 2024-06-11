
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WuwaDB.DBAccess.Entities.Account;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Entities.Login;


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
        public DbSet<Character_Skill_Detail_Number> CharacterSkillDetailNumbers { get; set; }
        public DbSet<Character_Skill_Description> CharacterSkillDescriptions { get; set; }
        public DbSet<Login_Info> LoginInfo { get; set; }
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
            modelBuilder.Entity<Character_Skill_Detail_Number>()
                .HasOne(c => c.Character_Skill_Detail)
                .WithMany(u => u.Character_Skill_Detail_Numbers)
                .HasForeignKey(c => c.CharacterSkillDetailId);
            modelBuilder.Entity<Character_Skill_Description>()
                .HasOne(c => c.Character_Skill)
                .WithMany(u => u.Character_Skill_Descriptions)
                .HasForeignKey(c => c.CharacterSkillId);
            modelBuilder.Entity<Character_Skill>()
                .HasOne(c=>c.Character)
                .WithMany(u=>u.CharacterSkills)
                .HasForeignKey(c => c.CharacterId);
            modelBuilder.Entity<Character_Skill>()
                .HasIndex(c => new { c.CharacterId, c.Type, c.Name})
                .IsUnique();
            modelBuilder.Entity<Character_Skill_Detail>()
                .HasIndex(c => new { c.CharacterSkillId, c.SkillDetailsName })
                .IsUnique();
            modelBuilder.Entity<Character_Skill>()
                 .HasIndex(c => new { c.Type, c.Name });

            modelBuilder.Entity<Character_Skill_Detail_Number>()
                .HasKey(c => new { c.CharacterSkillDetailId, c.Level });
            modelBuilder.Entity<Character_Skill_Description>()
                .HasKey(c => new { c.CharacterSkillId, c.DescriptionTitle });
        }

    }
}
