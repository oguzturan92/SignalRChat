﻿// <auto-generated />
using System;
using Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240511152210_start12")]
    partial class start12
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Entity.Concrete.Chat", b =>
                {
                    b.Property<int>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChatId"), 1L, 1);

                    b.Property<int>("ChatReceiverUserId")
                        .HasColumnType("int");

                    b.Property<int>("ChatSenderUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ChatStartingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ChatId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Entity.Concrete.ChatLine", b =>
                {
                    b.Property<int>("ChatLineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChatLineId"), 1L, 1);

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ChatLineDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ChatLineMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChatLineReceiverUserId")
                        .HasColumnType("int");

                    b.Property<int>("ChatLineSenderUserId")
                        .HasColumnType("int");

                    b.HasKey("ChatLineId");

                    b.HasIndex("ChatId");

                    b.ToTable("ChatLines");
                });

            modelBuilder.Entity("Entity.Concrete.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"), 1L, 1);

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentRowNumber")
                        .HasColumnType("int");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Entity.Concrete.Mentor", b =>
                {
                    b.Property<int>("MentorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MentorId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GraduationYear")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Point")
                        .HasColumnType("float");

                    b.HasKey("MentorId");

                    b.ToTable("Mentors");
                });

            modelBuilder.Entity("Entity.Concrete.Point", b =>
                {
                    b.Property<int>("PointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PointId"), 1L, 1);

                    b.Property<int>("MentorId")
                        .HasColumnType("int");

                    b.Property<int>("PointNo")
                        .HasColumnType("int");

                    b.Property<int>("PointScorerUserId")
                        .HasColumnType("int");

                    b.HasKey("PointId");

                    b.HasIndex("MentorId");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("Entity.Concrete.ProjeRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Entity.Concrete.ProjeUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("MentorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("MentorId")
                        .IsUnique()
                        .HasFilter("[MentorId] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("StudentId")
                        .IsUnique()
                        .HasFilter("[StudentId] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Entity.Concrete.SocialMedia", b =>
                {
                    b.Property<int>("SocialMediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SocialMediaId"), 1L, 1);

                    b.Property<int>("MentorId")
                        .HasColumnType("int");

                    b.Property<string>("SocialMediaIcon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMediaLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SocialMediaRowNumber")
                        .HasColumnType("int");

                    b.Property<string>("SocialMediaTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SocialMediaId");

                    b.HasIndex("MentorId");

                    b.ToTable("SocialMedias");
                });

            modelBuilder.Entity("Entity.Concrete.Staj", b =>
                {
                    b.Property<int>("StajId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StajId"), 1L, 1);

                    b.Property<int>("MentorId")
                        .HasColumnType("int");

                    b.Property<int>("StajRowNumber")
                        .HasColumnType("int");

                    b.Property<string>("StajSubTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StajTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StajId");

                    b.HasIndex("MentorId");

                    b.ToTable("Stajs");
                });

            modelBuilder.Entity("Entity.Concrete.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"), 1L, 1);

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartingYear")
                        .HasColumnType("datetime2");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Entity.Concrete.ChatLine", b =>
                {
                    b.HasOne("Entity.Concrete.Chat", "Chat")
                        .WithMany("ChatLines")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("Entity.Concrete.Point", b =>
                {
                    b.HasOne("Entity.Concrete.Mentor", "Mentor")
                        .WithMany("Points")
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mentor");
                });

            modelBuilder.Entity("Entity.Concrete.ProjeUser", b =>
                {
                    b.HasOne("Entity.Concrete.Mentor", "Mentor")
                        .WithOne("ProjeUser")
                        .HasForeignKey("Entity.Concrete.ProjeUser", "MentorId");

                    b.HasOne("Entity.Concrete.Student", "Student")
                        .WithOne("ProjeUser")
                        .HasForeignKey("Entity.Concrete.ProjeUser", "StudentId");

                    b.Navigation("Mentor");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Entity.Concrete.SocialMedia", b =>
                {
                    b.HasOne("Entity.Concrete.Mentor", "Mentor")
                        .WithMany("SocialMedias")
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mentor");
                });

            modelBuilder.Entity("Entity.Concrete.Staj", b =>
                {
                    b.HasOne("Entity.Concrete.Mentor", "Mentor")
                        .WithMany("Stajs")
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mentor");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Entity.Concrete.ProjeRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Entity.Concrete.ProjeUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Entity.Concrete.ProjeUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Entity.Concrete.ProjeRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entity.Concrete.ProjeUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Entity.Concrete.ProjeUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entity.Concrete.Chat", b =>
                {
                    b.Navigation("ChatLines");
                });

            modelBuilder.Entity("Entity.Concrete.Mentor", b =>
                {
                    b.Navigation("Points");

                    b.Navigation("ProjeUser");

                    b.Navigation("SocialMedias");

                    b.Navigation("Stajs");
                });

            modelBuilder.Entity("Entity.Concrete.Student", b =>
                {
                    b.Navigation("ProjeUser");
                });
#pragma warning restore 612, 618
        }
    }
}
