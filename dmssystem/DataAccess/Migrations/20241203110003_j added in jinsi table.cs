using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class jaddedinjinsitable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "jinsiDonations",
                newName: "jname");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "jinsiDonations",
                newName: "jSource");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "jinsiDonations",
                newName: "jQuantity");

            migrationBuilder.RenameColumn(
                name: "Kaifayat",
                table: "jinsiDonations",
                newName: "jKaifayat");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "jinsiDonations",
                newName: "jCost");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "jinsiDonations",
                newName: "jBrand");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "jname",
                table: "jinsiDonations",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "jSource",
                table: "jinsiDonations",
                newName: "Source");

            migrationBuilder.RenameColumn(
                name: "jQuantity",
                table: "jinsiDonations",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "jKaifayat",
                table: "jinsiDonations",
                newName: "Kaifayat");

            migrationBuilder.RenameColumn(
                name: "jCost",
                table: "jinsiDonations",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "jBrand",
                table: "jinsiDonations",
                newName: "Brand");
        }
    }
}
