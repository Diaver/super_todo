﻿// <auto-generated />
using System;
using Logging.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Logging.Database.Migrations
{
    [DbContext(typeof(LoggingDbContext))]
    partial class TodoTasksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Logging.Database.Models.RabbitMqEvent", b =>
                {
                    b.Property<Guid>("RabbitMqEventLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RabbitMqEventLogId");

                    b.ToTable("RabbitMqEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
