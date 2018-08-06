﻿// <auto-generated />
using Kladara_3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Kladara_3.Migrations
{
    [DbContext(typeof(Kladara_3Context))]
    partial class Kladara_3ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kladara_3.Models.Match", b =>
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
#pragma warning restore 612, 618
        }
    }
}
