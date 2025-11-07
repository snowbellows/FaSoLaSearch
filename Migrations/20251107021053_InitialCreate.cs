using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaSoLaSearch.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    SongId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Part = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_Songs", x => x.SongId);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Songs");
        }
    }
}
