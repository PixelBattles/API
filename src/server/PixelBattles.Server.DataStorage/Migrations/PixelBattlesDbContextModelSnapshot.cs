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
    partial class PixelBattlesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("PixelBattles.Server.DataStorage.Models.GameEntity", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BattleId");

                    b.Property<int>("ChangeIndex");

                    b.Property<int>("Cooldown");

                    b.Property<DateTime>("EndDateUTC");

                    b.Property<int>("Height");

                    b.Property<DateTime>("StartDateUTC");

                    b.Property<byte[]>("State");

                    b.Property<int>("Width");

                    b.HasKey("GameId");

                    b.HasIndex("BattleId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("PixelBattles.Server.DataStorage.Models.HubEntity", b =>
                {
                    b.Property<Guid>("HubId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("HubId");

                    b.ToTable("Hub");
                });

            modelBuilder.Entity("PixelBattles.Server.DataStorage.Models.UserActionEntity", b =>
                {
                    b.Property<Guid>("GameId");

                    b.Property<int>("ChangeIndex");

                    b.Property<int>("Color");

                    b.Property<Guid>("UserId");

                    b.Property<int>("XIndex");

                    b.Property<int>("YIndex");

                    b.HasKey("GameId", "ChangeIndex");

                    b.ToTable("UserAction");
                });

            modelBuilder.Entity("PixelBattles.Server.DataStorage.Models.UserBattleEntity", b =>
                {
                    b.Property<Guid>("BattleId");

                    b.Property<Guid>("UserId");

                    b.HasKey("BattleId", "UserId");

                    b.ToTable("UserBattle");
                });

            modelBuilder.Entity("PixelBattles.Server.DataStorage.Stores.EnumLookup<PixelBattles.Server.DataStorage.Models.BattleStatusEntity>", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("BattleStatus");
                });

            modelBuilder.Entity("PixelBattles.Server.DataStorage.Models.GameEntity", b =>
                {
                    b.HasOne("PixelBattles.Server.DataStorage.Models.BattleEntity")
                        .WithMany("Games")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PixelBattles.Server.DataStorage.Models.UserBattleEntity", b =>
                {
                    b.HasOne("PixelBattles.Server.DataStorage.Models.BattleEntity")
                        .WithMany("UserBattles")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
