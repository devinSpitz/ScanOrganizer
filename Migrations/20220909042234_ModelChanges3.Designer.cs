﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

#nullable disable

namespace ScanOrganizer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220909042234_ModelChanges3")]
    partial class ModelChanges3
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
#pragma warning restore 612, 618
        }
    }
}
