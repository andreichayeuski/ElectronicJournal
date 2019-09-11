﻿// <auto-generated />
using System;
using EJ.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EJ.Entities.Migrations
{
    [DbContext(typeof(ElectronicJournalContext))]
    [Migration("20190508125551_Subjects")]
    partial class Subjects
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EJ.Entities.Models.Auditorium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Number");

                    b.HasKey("Id");

                    b.ToTable("Auditorium");
                });

            modelBuilder.Entity("EJ.Entities.Models.ClassType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.HasKey("Id");

                    b.ToTable("ClassType");
                });

            modelBuilder.Entity("EJ.Entities.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<int>("Number");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("EJ.Entities.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseId");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("HalfGroup")
                        .HasColumnType("bit");

                    b.Property<int>("Number");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("EJ.Entities.Models.GroupShedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId");

                    b.Property<int>("SemesterId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("SemesterId");

                    b.ToTable("GroupShedule");
                });

            modelBuilder.Entity("EJ.Entities.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("EJ.Entities.Models.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Semester");
                });

            modelBuilder.Entity("EJ.Entities.Models.SheduleSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupSheduleId");

                    b.Property<int>("SubjectId");

                    b.HasKey("Id");

                    b.HasIndex("GroupSheduleId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SheduleSubject");
                });

            modelBuilder.Entity("EJ.Entities.Models.SheduleTimeSpending", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditoriumId");

                    b.Property<int>("ClassTypeId");

                    b.Property<int>("SheduleSubjectId");

                    b.Property<int>("TimeSpendingId");

                    b.Property<int>("WeekDayId");

                    b.HasKey("Id");

                    b.HasIndex("AuditoriumId");

                    b.HasIndex("ClassTypeId");

                    b.HasIndex("SheduleSubjectId");

                    b.HasIndex("TimeSpendingId");

                    b.HasIndex("WeekDayId");

                    b.ToTable("SheduleTimeSpending");
                });

            modelBuilder.Entity("EJ.Entities.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.HasKey("Id");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("EJ.Entities.Models.TimeSpending", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("Number");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("TimeSpending");
                });

            modelBuilder.Entity("EJ.Entities.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasMaxLength(70);

                    b.Property<bool>("EmailVerified");

                    b.Property<string>("FName")
                        .HasColumnName("FName")
                        .HasMaxLength(50);

                    b.Property<int?>("GroupId");

                    b.Property<string>("MName")
                        .HasColumnName("MName")
                        .HasMaxLength(50);

                    b.Property<byte[]>("Password");

                    b.Property<string>("PersonalNumber")
                        .HasMaxLength(15);

                    b.Property<DateTime?>("RemovalDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("RoleId");

                    b.Property<string>("SName")
                        .HasColumnName("SName")
                        .HasMaxLength(50);

                    b.Property<bool>("Sex");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<int>("UserStateId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserStateId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EJ.Entities.Models.UserHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EditingDate")
                        .HasColumnType("datetime");

                    b.Property<int>("StateId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserHistories");
                });

            modelBuilder.Entity("EJ.Entities.Models.UserRolesHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EditingDate")
                        .HasColumnType("datetime");

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRolesHistories");
                });

            modelBuilder.Entity("EJ.Entities.Models.UserState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("UserStates");
                });

            modelBuilder.Entity("EJ.Entities.Models.WeekDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Day");

                    b.Property<int>("NumberOfWeek");

                    b.HasKey("Id");

                    b.ToTable("WeekDay");
                });

            modelBuilder.Entity("EJ.Entities.Models.Group", b =>
                {
                    b.HasOne("EJ.Entities.Models.Course", "Course")
                        .WithMany("Groups")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("EJ.Entities.Models.GroupShedule", b =>
                {
                    b.HasOne("EJ.Entities.Models.Group", "Group")
                        .WithMany("GroupShedules")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EJ.Entities.Models.Semester", "Semester")
                        .WithMany("GroupShedules")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EJ.Entities.Models.SheduleSubject", b =>
                {
                    b.HasOne("EJ.Entities.Models.GroupShedule", "GroupShedule")
                        .WithMany("SheduleSubjects")
                        .HasForeignKey("GroupSheduleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EJ.Entities.Models.Subject", "Subject")
                        .WithMany("SheduleSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EJ.Entities.Models.SheduleTimeSpending", b =>
                {
                    b.HasOne("EJ.Entities.Models.Auditorium", "Auditorium")
                        .WithMany("SheduleTimeSpendings")
                        .HasForeignKey("AuditoriumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EJ.Entities.Models.ClassType", "ClassType")
                        .WithMany("SheduleTimeSpendings")
                        .HasForeignKey("ClassTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EJ.Entities.Models.SheduleSubject", "SheduleSubject")
                        .WithMany("SheduleTimeSpendings")
                        .HasForeignKey("SheduleSubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EJ.Entities.Models.TimeSpending", "TimeSpending")
                        .WithMany("SheduleTimeSpendings")
                        .HasForeignKey("TimeSpendingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EJ.Entities.Models.WeekDay", "WeekDay")
                        .WithMany("SheduleTimeSpendings")
                        .HasForeignKey("WeekDayId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EJ.Entities.Models.User", b =>
                {
                    b.HasOne("EJ.Entities.Models.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId");

                    b.HasOne("EJ.Entities.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.HasOne("EJ.Entities.Models.UserState", "UserState")
                        .WithMany("Users")
                        .HasForeignKey("UserStateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EJ.Entities.Models.UserHistory", b =>
                {
                    b.HasOne("EJ.Entities.Models.User", "User")
                        .WithMany("UserHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EJ.Entities.Models.UserRolesHistory", b =>
                {
                    b.HasOne("EJ.Entities.Models.User", "User")
                        .WithMany("UserRolesHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
