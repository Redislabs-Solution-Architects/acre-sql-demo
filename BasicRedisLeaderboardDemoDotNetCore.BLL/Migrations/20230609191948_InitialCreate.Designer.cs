﻿// <auto-generated />
using System;
using BasicRedisLeaderboardDemoDotNetCore.BLL.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230609191948_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BasicRedisLeaderboardDemoDotNetCore.BLL.Entities.RankEntity", b =>
                {
                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("MarketCap")
                        .HasColumnType("bigint");

                    b.Property<long>("Rank")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Symbol");

                    b.ToTable("company", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}