using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class rescueteam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RescueTeamId",
                table: "volunteers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "rescueTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rescueTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rescueTeams_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_volunteers_RescueTeamId",
                table: "volunteers",
                column: "RescueTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_rescueTeams_LocationId",
                table: "rescueTeams",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_volunteers_rescueTeams_RescueTeamId",
                table: "volunteers",
                column: "RescueTeamId",
                principalTable: "rescueTeams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_volunteers_rescueTeams_RescueTeamId",
                table: "volunteers");

            migrationBuilder.DropTable(
                name: "rescueTeams");

            migrationBuilder.DropIndex(
                name: "IX_volunteers_RescueTeamId",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "RescueTeamId",
                table: "volunteers");
        }
    }
}
