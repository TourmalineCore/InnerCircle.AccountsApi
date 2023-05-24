﻿// <auto-generated />
using System;
using Accounts.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UserManagementService.DataAccess.Migrations
{
    [DbContext(typeof(AccountsDbContext))]
    partial class UsersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Accounts.Core.Entities.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CorporateEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Instant>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Instant?>("DeletedAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CorporateEmail")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CorporateEmail = "ceo@tourmalinecore.com",
                            CreatedAt = NodaTime.Instant.FromUnixTimeTicks(15778368000000000L),
                            FirstName = "Ceo",
                            IsBlocked = false,
                            LastName = "Ceo",
                            MiddleName = "Ceo"
                        },
                        new
                        {
                            Id = 2L,
                            CorporateEmail = "inner-circle-admin@tourmalinecore.com",
                            CreatedAt = NodaTime.Instant.FromUnixTimeTicks(15778368000000000L),
                            FirstName = "Admin",
                            IsBlocked = false,
                            LastName = "Admin",
                            MiddleName = "Admin"
                        });
                });

            modelBuilder.Entity("Accounts.Core.Entities.AccountRole", b =>
                {
                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("AccountId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AccountRoles");

                    b.HasData(
                        new
                        {
                            AccountId = 1L,
                            RoleId = 2L
                        },
                        new
                        {
                            AccountId = 2L,
                            RoleId = 1L
                        });
                });

            modelBuilder.Entity("Accounts.Core.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("Permissions")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Admin",
                            Permissions = new[] { "ViewPersonalProfile", "EditPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "EditAccounts", "ViewRoles", "EditRoles" }
                        },
                        new
                        {
                            Id = 2L,
                            Name = "CEO",
                            Permissions = new[] { "ViewPersonalProfile", "EditPersonalProfile", "ViewContacts", "ViewSalaryAndDocumentsData", "EditFullEmployeesData", "AccessAnalyticalForecastsPage", "ViewAccounts", "EditAccounts", "ViewRoles", "EditRoles" }
                        });
                });

            modelBuilder.Entity("Accounts.Core.Entities.AccountRole", b =>
                {
                    b.HasOne("Accounts.Core.Entities.Account", "Account")
                        .WithMany("AccountRoles")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accounts.Core.Entities.Role", "Role")
                        .WithMany("AccountRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Accounts.Core.Entities.Account", b =>
                {
                    b.Navigation("AccountRoles");
                });

            modelBuilder.Entity("Accounts.Core.Entities.Role", b =>
                {
                    b.Navigation("AccountRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
