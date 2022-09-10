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
    [Migration("20220905161042_AddFolderScans")]
    partial class AddFolderScans
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

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FolderType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IncludeSubFolders")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MonitorFolderPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ResultFolderPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FolderScans");
                });

            modelBuilder.Entity("ScanOrganizer.Models.OcrTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FindTag")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FolderName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FolderType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("OcrTags");
                });
#pragma warning restore 612, 618
        }
    }
}
