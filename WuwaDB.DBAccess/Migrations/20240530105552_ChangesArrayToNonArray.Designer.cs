﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WuwaDB.DBAccess.DataContext;

#nullable disable

namespace WuwaDB.DBAccess.Migrations
{
    [DbContext(typeof(WuwaDbContext))]
    [Migration("20240530105552_ChangesArrayToNonArray")]
    partial class ChangesArrayToNonArray
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.HasIndex("Username");

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

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<int>("Weapon")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("CharacterId", "Type")
                        .IsUnique();

                    b.ToTable("CharacterSkills");
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SkillDetailsNum")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CharacterSkillId");

                    b.ToTable("CharacterSkillDetails");
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

                    b.Property<double>("Max_Resonance_Energy")
                        .HasColumnType("float");

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

                    b.HasIndex("Email");

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

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character", "Character")
                        .WithMany("CharacterSkills")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
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

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Stats_Base", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character", "Character")
                        .WithMany("CharacterStatBases")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
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

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Account.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character", b =>
                {
                    b.Navigation("CharacterSkills");

                    b.Navigation("CharacterStatBases");

                    b.Navigation("VoiceActors");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill", b =>
                {
                    b.Navigation("Character_Skill_Details");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Shared.Language", b =>
                {
                    b.Navigation("VoiceActors");
                });
#pragma warning restore 612, 618
        }
    }
}
