﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimetableA.DataAccessLayer.EntityFramework.Data;

#nullable disable

namespace TimetableA.DataAccessLayer.EntityFramework.Migrations
{
    [DbContext(typeof(TimetableAContext))]
    [Migration("20230722163023_init3")]
    partial class init3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TimetableA.Models.Group", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HexColor")
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("TimetableId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TimetableId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TimetableA.Models.Lesson", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Classroom")
                        .HasColumnType("nvarchar(32)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("GroupId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("TimetableA.Models.Timetable", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Cycles")
                        .HasColumnType("int");

                    b.Property<bool>("DisplayEmptyDays")
                        .HasColumnType("bit");

                    b.Property<string>("EditKey")
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("ReadKey")
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Timetables");
                });

            modelBuilder.Entity("TimetableA.Models.Group", b =>
                {
                    b.HasOne("TimetableA.Models.Timetable", "Timetable")
                        .WithMany("Groups")
                        .HasForeignKey("TimetableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Timetable");
                });

            modelBuilder.Entity("TimetableA.Models.Lesson", b =>
                {
                    b.HasOne("TimetableA.Models.Group", "Group")
                        .WithMany("Lessons")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("TimetableA.Models.Group", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("TimetableA.Models.Timetable", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
