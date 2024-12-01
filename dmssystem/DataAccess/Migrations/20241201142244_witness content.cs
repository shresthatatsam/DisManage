using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class witnesscontent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WitnessAddress",
                table: "DisasterImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WitnessGender",
                table: "DisasterImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WitnessName",
                table: "DisasterImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WitnessPhonenumber",
                table: "DisasterImages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WitnessAddress",
                table: "DisasterImages");

            migrationBuilder.DropColumn(
                name: "WitnessGender",
                table: "DisasterImages");

            migrationBuilder.DropColumn(
                name: "WitnessName",
                table: "DisasterImages");

            migrationBuilder.DropColumn(
                name: "WitnessPhonenumber",
                table: "DisasterImages");
        }
    }
}
