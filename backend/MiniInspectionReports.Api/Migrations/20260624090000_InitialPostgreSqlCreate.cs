using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiniInspectionReports.Api.Migrations;

/// <inheritdoc />
public partial class InitialPostgreSqlCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "InspectionReports",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                ProductName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                InspectorName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                InspectionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                ResultStatus = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                Comment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_InspectionReports", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "InspectionReports",
            columns: new[] { "Id", "Comment", "InspectionDate", "InspectorName", "ProductName", "ResultStatus" },
            values: new object[,]
            {
                { 1, "Keine Auffälligkeiten festgestellt.", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anna Müller", "Hydraulikpumpe HP-200", "Bestanden" },
                { 2, "Gehäuse leicht beschädigt, Funktion gegeben.", new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max Schneider", "Steuergerät SG-42", "Mängel" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "InspectionReports");
    }
}
