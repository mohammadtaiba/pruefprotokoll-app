using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pruefprotokoll.Api.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pruefprotokoll.Api.Migrations;

[DbContext(typeof(AppDbContext))]
partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.8")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("Pruefprotokoll.Api.Models.InspectionReport", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer");

            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

            b.Property<string>("Comment")
                .HasMaxLength(1000)
                .HasColumnType("character varying(1000)");

            b.Property<DateTime>("InspectionDate")
                .HasColumnType("timestamp without time zone");

            b.Property<string>("InspectorName")
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnType("character varying(120)");

            b.Property<string>("ProductName")
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnType("character varying(120)");

            b.Property<string>("ResultStatus")
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("character varying(30)");

            b.HasKey("Id");

            b.ToTable("InspectionReports");

            b.HasData(
                new
                {
                    Id = 1,
                    Comment = "Keine Auffälligkeiten festgestellt.",
                    InspectionDate = new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    InspectorName = "Anna Müller",
                    ProductName = "Hydraulikpumpe HP-200",
                    ResultStatus = "Bestanden"
                },
                new
                {
                    Id = 2,
                    Comment = "Gehäuse leicht beschädigt, Funktion gegeben.",
                    InspectionDate = new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                    InspectorName = "Max Schneider",
                    ProductName = "Steuergerät SG-42",
                    ResultStatus = "Mängel"
                });
        });
#pragma warning restore 612, 618
    }
}
