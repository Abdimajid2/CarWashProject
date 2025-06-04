using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class updatemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationTime",
                table: "Servitypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationTime",
                table: "Servitypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
