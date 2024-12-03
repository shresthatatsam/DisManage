using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class jinsiid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DonationId",
                table: "jinsiDonations",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_jinsiDonations_donations_DonationId",
                table: "jinsiDonations",
                column: "DonationId",
                principalTable: "donations",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jinsiDonations_donations_DonationId",
                table: "jinsiDonations");

            migrationBuilder.DropIndex(
                name: "IX_jinsiDonations_DonationId",
                table: "jinsiDonations");

            migrationBuilder.DropColumn(
                name: "DonationId",
                table: "jinsiDonations");

            migrationBuilder.DropColumn(
                name: "JinsiId",
                table: "donations");
        }
    }
}
