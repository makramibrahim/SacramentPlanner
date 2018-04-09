using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SacramentMeetingPlanner.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sacrament",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Conducting = table.Column<string>(maxLength: 50, nullable: false),
                    MeetingDate = table.Column<DateTime>(nullable: false),
                    Subjects = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sacrament", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Hymns",
                columns: table => new
                {
                    HymnsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClosingHymn = table.Column<string>(maxLength: 50, nullable: false),
                    ClosingNumber = table.Column<int>(nullable: false),
                    MeetingDate = table.Column<DateTime>(nullable: false),
                    OpeningHyNumber = table.Column<int>(nullable: false),
                    OpeningHymn = table.Column<string>(maxLength: 50, nullable: false),
                    SacrHyNumber = table.Column<int>(nullable: false),
                    SacramentHymn = table.Column<string>(maxLength: 50, nullable: false),
                    SacramentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hymns", x => x.HymnsID);
                    table.ForeignKey(
                        name: "FK_Hymns_Sacrament_SacramentID",
                        column: x => x.SacramentID,
                        principalTable: "Sacrament",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prayers",
                columns: table => new
                {
                    PrayersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClosingPrayers = table.Column<string>(maxLength: 50, nullable: false),
                    MeetingDate = table.Column<DateTime>(nullable: false),
                    OpeningPrayers = table.Column<string>(maxLength: 50, nullable: false),
                    SacramentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prayers", x => x.PrayersID);
                    table.ForeignKey(
                        name: "FK_Prayers_Sacrament_SacramentID",
                        column: x => x.SacramentID,
                        principalTable: "Sacrament",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    SpeakersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeetingDate = table.Column<DateTime>(nullable: false),
                    SacramentID = table.Column<int>(nullable: true),
                    Speaker1 = table.Column<string>(maxLength: 50, nullable: false),
                    Speaker2 = table.Column<string>(maxLength: 50, nullable: false),
                    Speaker3 = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.SpeakersID);
                    table.ForeignKey(
                        name: "FK_Speakers_Sacrament_SacramentID",
                        column: x => x.SacramentID,
                        principalTable: "Sacrament",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hymns_SacramentID",
                table: "Hymns",
                column: "SacramentID");

            migrationBuilder.CreateIndex(
                name: "IX_Prayers_SacramentID",
                table: "Prayers",
                column: "SacramentID");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_SacramentID",
                table: "Speakers",
                column: "SacramentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hymns");

            migrationBuilder.DropTable(
                name: "Prayers");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropTable(
                name: "Sacrament");
        }
    }
}
