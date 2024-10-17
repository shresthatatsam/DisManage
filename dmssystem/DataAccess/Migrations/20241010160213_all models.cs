using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class allmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Victims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PPSizePhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenshipNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrandFatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DisasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Victims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disasters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisasterType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateReported = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VictimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disasters_Victims_VictimId",
                        column: x => x.VictimId,
                        principalTable: "Victims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermanentProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermanentDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermanentMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermanentTole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemporaryProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemporaryDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemporaryMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemporaryTole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VictimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Victims_VictimId",
                        column: x => x.VictimId,
                        principalTable: "Victims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisasterImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisasterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VictimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisasterImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisasterImages_Disasters_DisasterId",
                        column: x => x.DisasterId,
                        principalTable: "Disasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisasterImages_Victims_VictimId",
                        column: x => x.VictimId,
                        principalTable: "Victims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisasterImages_DisasterId",
                table: "DisasterImages",
                column: "DisasterId");

            migrationBuilder.CreateIndex(
                name: "IX_DisasterImages_VictimId",
                table: "DisasterImages",
                column: "VictimId");

            migrationBuilder.CreateIndex(
                name: "IX_Disasters_VictimId",
                table: "Disasters",
                column: "VictimId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_VictimId",
                table: "Locations",
                column: "VictimId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisasterImages");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Disasters");

            migrationBuilder.DropTable(
                name: "Victims");
        }
    }
}
