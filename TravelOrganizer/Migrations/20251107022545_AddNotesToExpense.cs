using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelOrganizer.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesToExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Expenses",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Expenses");
        }
    }
}
