﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parser.Data;

namespace Parser.Data.Migrations
{
    [DbContext(typeof(ParserDbContext))]
    partial class ParserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Parser.Data.Core.Entities.LogLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BytesSent");

                    b.Property<string>("Country");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Host");

                    b.Property<string>("Route");

                    b.Property<int>("StatusResult");

                    b.HasKey("Id");

                    b.ToTable("LogLines");
                });

            modelBuilder.Entity("Parser.Data.Core.Entities.QueryParameter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("LogLineId");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("LogLineId");

                    b.ToTable("QueryParameters");
                });

            modelBuilder.Entity("Parser.Data.Core.Entities.QueryParameter", b =>
                {
                    b.HasOne("Parser.Data.Core.Entities.LogLine")
                        .WithMany("Parameters")
                        .HasForeignKey("LogLineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
