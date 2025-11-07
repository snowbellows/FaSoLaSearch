using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FaSoLaSearch.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSongNumberToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SongNumber",
                table: "Parts",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SongNumber",
                table: "Parts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
