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
    [Migration("20240520065156_UpdateCharacterEntity")]
    partial class UpdateCharacterEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("Name");

                    b.ToTable("CharacterSkills");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Perform", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterSkillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterSkillId");

                    b.ToTable("CharacterSkillPerforms");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Perform_Level", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterSkillPerformId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CharacterSkillPerformId");

                    b.ToTable("CharacterSkillPerformLevels");
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

                    b.Property<float>("Critical_Damage")
                        .HasColumnType("real");

                    b.Property<float>("Critical_Rate")
                        .HasColumnType("real");

                    b.Property<int>("DEF")
                        .HasColumnType("int");

                    b.Property<float>("Energy_Regen")
                        .HasColumnType("real");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.Property<int>("Max_Resonance_Energy")
                        .HasColumnType("int");

                    b.Property<int>("Max_Stamina")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterStatsBases");
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

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Perform", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character_Skill", "CharacterSkill")
                        .WithMany("CharacterSkillPerforms")
                        .HasForeignKey("CharacterSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CharacterSkill");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Perform_Level", b =>
                {
                    b.HasOne("WuwaDB.DBAccess.Entities.Character.Character_Skill_Perform", "CharacterSkillPerform")
                        .WithMany("CharacterSkillPerformLevels")
                        .HasForeignKey("CharacterSkillPerformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CharacterSkillPerform");
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
                    b.Navigation("CharacterSkillPerforms");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Character.Character_Skill_Perform", b =>
                {
                    b.Navigation("CharacterSkillPerformLevels");
                });

            modelBuilder.Entity("WuwaDB.DBAccess.Entities.Shared.Language", b =>
                {
                    b.Navigation("VoiceActors");
                });
#pragma warning restore 612, 618
        }
    }
}
