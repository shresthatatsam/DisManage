using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class rescueteamasssigned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssignVolunteerViewModelId",
                table: "volunteers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "assignVolunteers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedVolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignVolunteers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_volunteers_AssignVolunteerViewModelId",
                table: "volunteers",
                column: "AssignVolunteerViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_volunteers_assignVolunteers_AssignVolunteerViewModelId",
                table: "volunteers",
                column: "AssignVolunteerViewModelId",
                principalTable: "assignVolunteers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_volunteers_assignVolunteers_AssignVolunteerViewModelId",
                table: "volunteers");

            migrationBuilder.DropTable(
                name: "assignVolunteers");

            migrationBuilder.DropIndex(
                name: "IX_volunteers_AssignVolunteerViewModelId",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "AssignVolunteerViewModelId",
                table: "volunteers");
        }
    }
}
