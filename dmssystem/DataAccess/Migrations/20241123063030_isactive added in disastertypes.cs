using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class isactiveaddedindisastertypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_disasterTypes",
                table: "disasterTypes");

            migrationBuilder.RenameTable(
                name: "disasterTypes",
                newName: "DisasterTypes");

            migrationBuilder.AlterColumn<bool>(
                name: "Isactive",
                table: "DisasterTypes",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisasterTypes",
                table: "DisasterTypes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DisasterTypes",
                table: "DisasterTypes");

            migrationBuilder.RenameTable(
                name: "DisasterTypes",
                newName: "disasterTypes");

            migrationBuilder.AlterColumn<bool>(
                name: "Isactive",
                table: "disasterTypes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_disasterTypes",
                table: "disasterTypes",
                column: "Id");
        }
    }
}
