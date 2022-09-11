﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Interview.Infrastructure.Migrations;

[DbContext(typeof(RestaurantEFContext))]
[Migration("20220910133456_Init")]
partial class Init
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "6.0.8")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

        modelBuilder.Entity("Interview.Domain.Restaurant.Day", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.HasKey("Id");

                b.HasIndex("Name")
                    .IsUnique();

                b.ToTable("Day");
            });

        modelBuilder.Entity("Interview.Domain.Restaurant.Restaurant", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<byte[]>("RowVersion")
                    .IsConcurrencyToken()
                    .IsRequired()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.HasKey("Id");

                b.HasIndex("Name")
                    .IsUnique();

                b.ToTable("Restaurant");
            });

        modelBuilder.Entity("Interview.Domain.Restaurant.Schedule", b =>
            {
                b.Property<int>("DayId")
                    .HasColumnType("int");

                b.Property<int>("RestaurantId")
                    .HasColumnType("int");

                b.Property<TimeSpan>("End")
                    .HasColumnType("time");

                b.Property<byte[]>("RowVersion")
                    .IsConcurrencyToken()
                    .IsRequired()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.Property<TimeSpan>("Start")
                    .HasColumnType("time");

                b.HasKey("DayId", "RestaurantId");

                b.HasIndex("RestaurantId");

                b.ToTable("Schedule");
            });

        modelBuilder.Entity("Interview.Domain.Restaurant.Schedule", b =>
            {
                b.HasOne("Interview.Domain.Restaurant.Day", "Day")
                    .WithMany("Schedules")
                    .HasForeignKey("DayId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Interview.Domain.Restaurant.Restaurant", "Restaurant")
                    .WithMany("Schedules")
                    .HasForeignKey("RestaurantId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Day");

                b.Navigation("Restaurant");
            });

        modelBuilder.Entity("Interview.Domain.Restaurant.Day", b =>
            {
                b.Navigation("Schedules");
            });

        modelBuilder.Entity("Interview.Domain.Restaurant.Restaurant", b =>
            {
                b.Navigation("Schedules");
            });
#pragma warning restore 612, 618
    }
}
