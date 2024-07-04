﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WuwaDB.DBAccess.DataContext;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    [DbContext(typeof(WuwaDbContext))]
    partial class WuwaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Account.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Accounts", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("Account");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Account.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Element")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<int>("Weapon")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_ImageCard", b =>
                {
                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("CharacterId");

                    b.ToTable("Character_ImageCard");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_ImageModel", b =>
                {
                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("CharacterId");

                    b.ToTable("Character_ImageModel");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Type", "Name")
                        .IsUnique();

                    b.HasIndex("CharacterId", "Type", "Name")
                        .IsUnique();

                    b.ToTable("CharacterSkills");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Description", b =>
                {
                    b.Property<Guid>("CharacterSkillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DescriptionTitle")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CharacterSkillId", "DescriptionTitle");

                    b.ToTable("CharacterSkillDescriptions");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Detail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterSkillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SkillDetailsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterSkillId", "SkillDetailsName")
                        .IsUnique();

                    b.ToTable("CharacterSkillDetails");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Detail_Number", b =>
                {
                    b.Property<Guid>("CharacterSkillDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.HasKey("CharacterSkillDetailId", "Level");

                    b.ToTable("CharacterSkillDetailNumbers");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Image", b =>
                {
                    b.Property<Guid>("CharacterSkillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("CharacterSkillId", "Type");

                    b.ToTable("Character_Skill_Image");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Stats_Base", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ATK")
                        .HasColumnType("int");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Critical_Damage")
                        .HasColumnType("float");

                    b.Property<double>("Critical_Rate")
                        .HasColumnType("float");

                    b.Property<int>("DEF")
                        .HasColumnType("int");

                    b.Property<double>("Energy_Regen")
                        .HasColumnType("float");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterStatsBases");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Stats_Growth_Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AtkRatio")
                        .HasColumnType("int");

                    b.Property<int>("BreachLevel")
                        .HasColumnType("int");

                    b.Property<int>("DefRatio")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("LifeMaxRatio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CharacterStatsGrowthproperties");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.NumberMultiplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CharacterSkillDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPercentage")
                        .HasColumnType("bit");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int?>("Multiplier")
                        .HasColumnType("int");

                    b.Property<double>("Number")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CharacterSkillDetailId", "Level");

                    b.ToTable("NumberMultipliers");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.VoiceActor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("LanguageId");

                    b.ToTable("VoiceActor");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Login.Login_Info", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LoginUrl")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("LoginInfo");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Monster.Monster", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Monsters");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Monster.Monster_Stats_Base", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ATK")
                        .HasColumnType("int");

                    b.Property<int>("DEF")
                        .HasColumnType("int");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.Property<int>("Hardness")
                        .HasColumnType("int");

                    b.Property<Guid>("MonsterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MonsterId")
                        .IsUnique();

                    b.ToTable("MonsterStatsBases");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Monster.Monster_Stats_Base_Ele_Res", b =>
                {
                    b.Property<Guid>("MonsterStatsBaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ElementResist")
                        .HasColumnType("int");

                    b.Property<double>("Number")
                        .HasColumnType("float");

                    b.HasKey("MonsterStatsBaseId", "ElementResist");

                    b.ToTable("Monster_Stats_Base_Ele_Res");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Monster.Monster_Stats_Growth_Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AtkRatio")
                        .HasColumnType("int");

                    b.Property<int>("DefRatio")
                        .HasColumnType("int");

                    b.Property<int>("HardnessMaxRatio")
                        .HasColumnType("int");

                    b.Property<int>("HardnessRatio")
                        .HasColumnType("int");

                    b.Property<int>("HardnessRecoverRatio")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("LifeMaxRatio")
                        .HasColumnType("int");

                    b.Property<int>("RageMaxRatio")
                        .HasColumnType("int");

                    b.Property<int>("RageRatio")
                        .HasColumnType("int");

                    b.Property<int>("RageRecoverRatio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MonsterStatsGrowthProperties");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Shared.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Account.Admin", b =>
                {
                    b.HasBaseType("WuwaDB.DBAccess.Entities.Account.Account");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Account.User", b =>
                {
                    b.HasBaseType("WuwaDB.DBAccess.Entities.Account.Account");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Account.Account", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Account.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_ImageCard", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character", "Character")
                        .WithMany("CharacterImageCards")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_ImageModel", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character", "Character")
                        .WithMany("CharacterImageModels")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character", "Character")
                        .WithMany("CharacterSkills")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Description", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character_Skill", "Character_Skill")
                        .WithMany("Character_Skill_Descriptions")
                        .HasForeignKey("CharacterSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character_Skill");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Detail", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character_Skill", "Character_Skill")
                        .WithMany("Character_Skill_Details")
                        .HasForeignKey("CharacterSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character_Skill");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Detail_Number", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character_Skill_Detail", "Character_Skill_Detail")
                        .WithMany("Character_Skill_Detail_Numbers")
                        .HasForeignKey("CharacterSkillDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character_Skill_Detail");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Image", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character_Skill", "CharacterSkill")
                        .WithMany("CharacterSkillImages")
                        .HasForeignKey("CharacterSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CharacterSkill");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Stats_Base", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character", "Character")
                        .WithMany("CharacterStatBases")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.NumberMultiplier", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character_Skill_Detail_Number", "Character_Skill_Detail_Number")
                        .WithMany("NumberMultipliers")
                        .HasForeignKey("CharacterSkillDetailId", "Level")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character_Skill_Detail_Number");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.VoiceActor", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character", "Character")
                        .WithMany("VoiceActors")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WuwaDB.DBAccess.Entities.Shared.Language", "Language")
                        .WithMany("VoiceActors")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Monster.Monster_Stats_Base", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Monster.Monster", "Monster")
                        .WithMany("MonsterStatBases")
                        .HasForeignKey("MonsterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Monster");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Monster.Monster_Stats_Base_Ele_Res", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Monster.Monster_Stats_Base", "Monster_Stats_Base")
                        .WithMany("MonsterStatsBaseEleRes")
                        .HasForeignKey("MonsterStatsBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Monster_Stats_Base");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Account.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character", b =>
                {
                    b.Navigation("CharacterImageCards");

                    b.Navigation("CharacterImageModels");

                    b.Navigation("CharacterSkills");

                    b.Navigation("CharacterStatBases");

                    b.Navigation("VoiceActors");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill", b =>
                {
                    b.Navigation("CharacterSkillImages");

                    b.Navigation("Character_Skill_Descriptions");

                    b.Navigation("Character_Skill_Details");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Detail", b =>
                {
                    b.Navigation("Character_Skill_Detail_Numbers");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Detail_Number", b =>
                {
                    b.Navigation("NumberMultipliers");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Monster.Monster", b =>
                {
                    b.Navigation("MonsterStatBases");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Monster.Monster_Stats_Base", b =>
                {
                    b.Navigation("MonsterStatsBaseEleRes");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Shared.Language", b =>
                {
                    b.Navigation("VoiceActors");
                });
#pragma warning restore 612, 618
        }
    }
}
