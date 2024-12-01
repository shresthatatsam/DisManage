using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class donationinvictim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DonationId",
                table: "Victims",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VictimId",
                table: "donations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_donations_VictimId",
                table: "donations",
                column: "VictimId",
                unique: true,
                filter: "[VictimId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_donations_Victims_VictimId",
                table: "donations",
                column: "VictimId",
                principalTable: "Victims",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_donations_Victims_VictimId",
                table: "donations");

            migrationBuilder.DropIndex(
                name: "IX_donations_VictimId",
                table: "donations");

            migrationBuilder.DropColumn(
                name: "DonationId",
                table: "Victims");

            migrationBuilder.DropColumn(
                name: "VictimId",
                table: "donations");
        }
    }
}
