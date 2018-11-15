using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackerLib.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsages",
                columns: table => new
                {
                    ParticipantIdentifier = table.Column<string>(nullable: true),
                    DeviceModelName = table.Column<string>(nullable: true),
                    UserCount = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Package = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceUsages",
                columns: table => new
                {
                    ParticipantIdentifier = table.Column<string>(nullable: true),
                    DeviceModelName = table.Column<string>(nullable: true),
                    UserCount = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    EventType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceUsages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsages");

            migrationBuilder.DropTable(
                name: "DeviceUsages");
        }
    }
}
