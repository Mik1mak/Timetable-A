﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimetableA.Entities.Data;

namespace TimetableA.Entities.Migrations
{
    [DbContext(typeof(TimetableAContext))]
    [Migration("20210711135539_initial2")]
    partial class initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimetableA.Entities.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HexColor")
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("TimetableId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TimetableId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TimetableA.Entities.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Classroom")
                        .HasColumnType("nvarchar(32)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("TimetableA.Entities.Models.Timetable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Cycles")
                        .HasColumnType("int");

                    b.Property<string>("EditKey")
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("ReadKey")
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Timetables");
                });

            modelBuilder.Entity("TimetableA.Entities.Models.Group", b =>
                {
                    b.HasOne("TimetableA.Entities.Models.Timetable", "Timetable")
                        .WithMany("Gropus")
                        .HasForeignKey("TimetableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Timetable");
                });

            modelBuilder.Entity("TimetableA.Entities.Models.Lesson", b =>
                {
                    b.HasOne("TimetableA.Entities.Models.Group", "Group")
                        .WithMany("Lessons")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("TimetableA.Entities.Models.Group", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("TimetableA.Entities.Models.Timetable", b =>
                {
                    b.Navigation("Gropus");
                });
#pragma warning restore 612, 618
        }
    }
}
