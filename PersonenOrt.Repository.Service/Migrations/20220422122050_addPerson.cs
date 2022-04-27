using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonenOrt.Repository.Service.Migrations
{
    public partial class addPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ort",
                columns: table => new
                {
                    PLZ = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ort", x => x.PLZ);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Vorname = table.Column<string>(type: "TEXT", nullable: false),
                    OrtPLZ = table.Column<string>(type: "TEXT", nullable: false),
                    Geburtsdatum = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Ort_OrtPLZ",
                        column: x => x.OrtPLZ,
                        principalTable: "Ort",
                        principalColumn: "PLZ",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_OrtPLZ",
                table: "Person",
                column: "OrtPLZ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Ort");
        }
    }
}
