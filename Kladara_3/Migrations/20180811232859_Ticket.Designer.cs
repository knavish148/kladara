﻿// <auto-generated />
using Kladara3.Data;
using Kladara3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Kladara3.Migrations
{
    [DbContext(typeof(Kladara3Context))]
    [Migration("20180811232859_Ticket")]
    partial class Ticket
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kladara3.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AwayTeam");

                    b.Property<double>("AwayWins");

                    b.Property<string>("HomeTeam");

                    b.Property<double>("HomeWins");

                    b.Property<string>("Sport");

                    b.Property<double>("Tied");

                    b.HasKey("Id");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("Kladara3.Models.Pair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Bet");

                    b.Property<int>("MatchId");

                    b.HasKey("Id");

                    b.ToTable("Pair");
                });

            modelBuilder.Entity("Kladara3.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Bonus");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Pairs");

                    b.Property<double>("PossibleGain");

                    b.Property<int>("Wager");

                    b.HasKey("Id");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("Kladara3.Models.WalletTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("TransactionDate");

                    b.Property<double>("WalletAfter");

                    b.Property<double>("WalletBefore");

                    b.HasKey("Id");

                    b.ToTable("WalletTransaction");
                });
#pragma warning restore 612, 618
        }
    }
}
