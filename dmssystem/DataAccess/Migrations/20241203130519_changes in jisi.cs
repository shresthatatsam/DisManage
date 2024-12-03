using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changesinjisi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_jinsiDonations_DonationId",
                table: "jinsiDonations");

            migrationBuilder.DropColumn(
                name: "JinsiId",
                table: "donations");

            migrationBuilder.AlterColumn<string>(
                name: "jQuantity",
                table: "jinsiDonations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateIndex(
                name: "IX_jinsiDonations_DonationId",
                table: "jinsiDonations",
                column: "DonationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_jinsiDonations_DonationId",
                table: "jinsiDonations");

            migrationBuilder.AlterColumn<double>(
                name: "jQuantity",
                table: "jinsiDonations",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "JinsiId",
                table: "donations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_jinsiDonations_DonationId",
                table: "jinsiDonations",
                column: "DonationId",
                unique: true,
                filter: "[DonationId] IS NOT NULL");
        }
    }
}
