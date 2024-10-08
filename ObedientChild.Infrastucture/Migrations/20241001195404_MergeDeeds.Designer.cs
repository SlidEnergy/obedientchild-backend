﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ObedientChild.Infrastructure;

#nullable disable

namespace ObedientChild.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241001195404_MergeDeeds")]
    partial class MergeDeeds
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ObedientChild.Domain.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<int>("TrusteeId")
                        .HasColumnType("integer");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("TrusteeId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ObedientChild.Domain.AuthToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AuthTokens");
                });

            modelBuilder.Entity("ObedientChild.Domain.BadDeed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BadDeeds");
                });

            modelBuilder.Entity("ObedientChild.Domain.BalanceHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("BalanceType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("EntityId")
                        .HasColumnType("integer")
                        .HasColumnName("ChildId");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CoinHistory");
                });

            modelBuilder.Entity("ObedientChild.Domain.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("bytea");

                    b.Property<int>("Balance")
                        .HasColumnType("integer");

                    b.Property<int>("BigGoalBalance")
                        .HasColumnType("integer");

                    b.Property<int>("BigGoalId")
                        .HasColumnType("integer");

                    b.Property<int>("DreamBalance")
                        .HasColumnType("integer");

                    b.Property<int?>("DreamId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("ObedientChild.Domain.ChildStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChildId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ChildStatuses");
                });

            modelBuilder.Entity("ObedientChild.Domain.ChildTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChildId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("GoodDeedId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GoodDeedId");

                    b.ToTable("ChildTasks");
                });

            modelBuilder.Entity("ObedientChild.Domain.GoodDeed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GoodDeeds");
                });

            modelBuilder.Entity("ObedientChild.Domain.HabitHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChildId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("Day")
                        .HasColumnType("date");

                    b.Property<int>("HabitId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("HabitHistory");
                });

            modelBuilder.Entity("ObedientChild.Domain.Habits.ChildHabit", b =>
                {
                    b.Property<int>("HabitId")
                        .HasColumnType("integer");

                    b.Property<int>("ChildId")
                        .HasColumnType("integer");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("HabitId", "ChildId");

                    b.ToTable("ChildHabits");
                });

            modelBuilder.Entity("ObedientChild.Domain.Habits.Habit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DeedType")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Deeds");
                });

            modelBuilder.Entity("ObedientChild.Domain.LifeEnergy.LifeEnergyAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Balance")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("LifeEnergyAccounts");
                });

            modelBuilder.Entity("ObedientChild.Domain.LifeEnergy.TrusteeLifeEnergyAccount", b =>
                {
                    b.Property<int>("TrusteeId")
                        .HasColumnType("integer");

                    b.Property<int>("LifeEnergyAccountId")
                        .HasColumnType("integer");

                    b.HasKey("TrusteeId", "LifeEnergyAccountId");

                    b.HasIndex("LifeEnergyAccountId");

                    b.ToTable("TrusteeLifeEnergyAccounts");
                });

            modelBuilder.Entity("ObedientChild.Domain.LifeEnergyHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LifeEnergyAccountId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LifeEnergyHistory");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.CharacterTrait", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BadDeedId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("GoodDeedId")
                        .HasColumnType("integer");

                    b.Property<int?>("HabitId")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BadDeedId");

                    b.HasIndex("GoodDeedId");

                    b.HasIndex("HabitId");

                    b.ToTable("CharacterTraits");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.CharacterTraitBadDeed", b =>
                {
                    b.Property<int>("CharacterTraitId")
                        .HasColumnType("integer");

                    b.Property<int>("BadDeedid")
                        .HasColumnType("integer");

                    b.HasKey("CharacterTraitId", "BadDeedid");

                    b.ToTable("CharacterTraitBadDeeds");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.CharacterTraitGoodDeed", b =>
                {
                    b.Property<int>("CharacterTraitId")
                        .HasColumnType("integer");

                    b.Property<int>("GoodDeedId")
                        .HasColumnType("integer");

                    b.HasKey("CharacterTraitId", "GoodDeedId");

                    b.ToTable("CharacterTraitGoodDeeds");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.CharacterTraitHabit", b =>
                {
                    b.Property<int>("CharacterTraitId")
                        .HasColumnType("integer");

                    b.Property<int>("HabitId")
                        .HasColumnType("integer");

                    b.HasKey("CharacterTraitId", "HabitId");

                    b.ToTable("CharacterTraitHabits");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.CharacterTraitLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CharacterTraitId")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("NeedExperience")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CharacterTraitId");

                    b.ToTable("CharacterTraitsLevel");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.CharacterTraitPersonality", b =>
                {
                    b.Property<int>("PersonalityId")
                        .HasColumnType("integer");

                    b.Property<int>("CharacterTraitId")
                        .HasColumnType("integer");

                    b.HasKey("PersonalityId", "CharacterTraitId");

                    b.ToTable("PersonalitiesCharacterTraits");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.ChildCharacterTrait", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CharacterTraitId")
                        .HasColumnType("integer");

                    b.Property<int>("ChildId")
                        .HasColumnType("integer");

                    b.Property<int>("Experience")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CharacterTraitId");

                    b.ToTable("ChildCharacterTraits");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.Destiny", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Destinies");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.DestinyPersonality", b =>
                {
                    b.Property<int>("DestinyId")
                        .HasColumnType("integer");

                    b.Property<int>("PersonalityId")
                        .HasColumnType("integer");

                    b.HasKey("DestinyId", "PersonalityId");

                    b.ToTable("DestiniesPersonalities");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.Personality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Personalities");
                });

            modelBuilder.Entity("ObedientChild.Domain.Reward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("ObedientChild.Domain.Trustee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Trustee");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ObedientChild.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ObedientChild.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ObedientChild.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ObedientChild.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ObedientChild.Domain.ApplicationUser", b =>
                {
                    b.HasOne("ObedientChild.Domain.Trustee", "Trustee")
                        .WithMany()
                        .HasForeignKey("TrusteeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trustee");
                });

            modelBuilder.Entity("ObedientChild.Domain.AuthToken", b =>
                {
                    b.HasOne("ObedientChild.Domain.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ObedientChild.Domain.ChildTask", b =>
                {
                    b.HasOne("ObedientChild.Domain.GoodDeed", "GoodDeed")
                        .WithMany()
                        .HasForeignKey("GoodDeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GoodDeed");
                });

            modelBuilder.Entity("ObedientChild.Domain.LifeEnergy.TrusteeLifeEnergyAccount", b =>
                {
                    b.HasOne("ObedientChild.Domain.LifeEnergy.LifeEnergyAccount", "LifeEnergyAccount")
                        .WithMany()
                        .HasForeignKey("LifeEnergyAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ObedientChild.Domain.Trustee", "Trustee")
                        .WithMany()
                        .HasForeignKey("TrusteeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LifeEnergyAccount");

                    b.Navigation("Trustee");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.CharacterTrait", b =>
                {
                    b.HasOne("ObedientChild.Domain.BadDeed", null)
                        .WithMany("CharacterTraits")
                        .HasForeignKey("BadDeedId");

                    b.HasOne("ObedientChild.Domain.GoodDeed", null)
                        .WithMany("CharacterTraits")
                        .HasForeignKey("GoodDeedId");

                    b.HasOne("ObedientChild.Domain.Habits.Habit", null)
                        .WithMany("CharacterTraits")
                        .HasForeignKey("HabitId");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.CharacterTraitLevel", b =>
                {
                    b.HasOne("ObedientChild.Domain.Personalities.CharacterTrait", null)
                        .WithMany("Levels")
                        .HasForeignKey("CharacterTraitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.ChildCharacterTrait", b =>
                {
                    b.HasOne("ObedientChild.Domain.Personalities.CharacterTrait", "CharacterTrait")
                        .WithMany()
                        .HasForeignKey("CharacterTraitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CharacterTrait");
                });

            modelBuilder.Entity("ObedientChild.Domain.BadDeed", b =>
                {
                    b.Navigation("CharacterTraits");
                });

            modelBuilder.Entity("ObedientChild.Domain.GoodDeed", b =>
                {
                    b.Navigation("CharacterTraits");
                });

            modelBuilder.Entity("ObedientChild.Domain.Habits.Habit", b =>
                {
                    b.Navigation("CharacterTraits");
                });

            modelBuilder.Entity("ObedientChild.Domain.Personalities.CharacterTrait", b =>
                {
                    b.Navigation("Levels");
                });
#pragma warning restore 612, 618
        }
    }
}
