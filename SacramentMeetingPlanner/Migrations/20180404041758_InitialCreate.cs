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
                    HymnName = table.Column<string>(maxLength: 50, nullable: false),
                    HymnNumber = table.Column<int>(nullable: false),
                    MeetingDate = table.Column<DateTime>(nullable: false),
                    Prayers = table.Column<string>(maxLength: 50, nullable: false),
                    Speakers = table.Column<string>(maxLength: 50, nullable: false),
                    Subjects = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sacrament", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sacrament");
        }
    }
}
