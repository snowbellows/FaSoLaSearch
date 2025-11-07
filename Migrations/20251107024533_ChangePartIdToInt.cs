using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FaSoLaSearch.Migrations
{
    /// <inheritdoc />
    public partial class ChangePartIdToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Parts");

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table
                        .Column<Guid>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                        ),
                    SongNumber = table.Column<int>(type: "integer", nullable: false),
                    SongName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    First = table.Column<char>(type: "character(1)", nullable: false),
                    Second = table.Column<char>(type: "character(1)", nullable: false),
                    Third = table.Column<char>(type: "character(1)", nullable: false),
                    Fourth = table.Column<char>(type: "character(1)", nullable: false),
                    Fifth = table.Column<char>(type: "character(1)", nullable: false),
                    Sixth = table.Column<char>(type: "character(1)", nullable: false),
                    Seventh = table.Column<char>(type: "character(1)", nullable: false),
                    Eighth = table.Column<char>(type: "character(1)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Parts");

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<Guid>(type: "uuid", nullable: false),
                    SongNumber = table.Column<int>(type: "integer", nullable: false),
                    SongName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    First = table.Column<char>(type: "character(1)", nullable: false),
                    Second = table.Column<char>(type: "character(1)", nullable: false),
                    Third = table.Column<char>(type: "character(1)", nullable: false),
                    Fourth = table.Column<char>(type: "character(1)", nullable: false),
                    Fifth = table.Column<char>(type: "character(1)", nullable: false),
                    Sixth = table.Column<char>(type: "character(1)", nullable: false),
                    Seventh = table.Column<char>(type: "character(1)", nullable: false),
                    Eighth = table.Column<char>(type: "character(1)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                }
            );
        }
    }
}
