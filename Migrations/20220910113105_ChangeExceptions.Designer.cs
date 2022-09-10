﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

#nullable disable

namespace ScanOrganizer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220910113105_ChangeExceptions")]
    partial class ChangeExceptions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("ScanOrganizer.Models.FolderScan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DeleteSourceWhenFinished")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IncludeSubFolders")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Languages")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MonitorFolderPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ResultFolderPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FolderScans");
                });

            modelBuilder.Entity("ScanOrganizer.Models.FolderScanExceptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FolderScanId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InnerExceptionMessage")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FolderScanId");

                    b.ToTable("FolderScanExceptions");
                });

            modelBuilder.Entity("ScanOrganizer.Models.SortTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FindTag")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FolderName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FolderType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SortOrder")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("UseRegex")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SortTags");
                });

            modelBuilder.Entity("ScanOrganizer.Models.FolderScanExceptions", b =>
                {
                    b.HasOne("ScanOrganizer.Models.FolderScan", null)
                        .WithMany("Exceptions")
                        .HasForeignKey("FolderScanId");
                });

            modelBuilder.Entity("ScanOrganizer.Models.FolderScan", b =>
                {
                    b.Navigation("Exceptions");
                });
#pragma warning restore 612, 618
        }
    }
}
