﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Showcase.Data;

#nullable disable

namespace Showcase.Migrations.GameDb
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20240904122840_tessttt")]
    partial class tessttt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("Showcase.Models.GameResultRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DatePlayed")
                        .HasColumnType("TEXT");

                    b.Property<string>("GameId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Player1Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Player2Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Result")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("GameResults");
                });
#pragma warning restore 612, 618
        }
    }
}
