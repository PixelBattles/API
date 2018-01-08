﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using PixelBattles.Server.DataStorage;
using PixelBattles.Server.DataStorage.Models;
using System;

namespace PixelBattles.Server.DataStorage.Migrations
{
    [DbContext(typeof(PixelBattlesDbContext))]
    [Migration("20180108092700_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PixelBattles.Server.DataStorage.Models.BattleEntity", b =>
                {
                    b.Property<Guid>("BattleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.HasKey("BattleId");

                    b.ToTable("Battle");
                });

            modelBuilder.Entity("PixelBattles.Server.DataStorage.Stores.EnumLookup<PixelBattles.Server.DataStorage.Models.BattleStatusEntity>", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("BattleStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
