using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackYourPresenceAPI.Migrations
{
    public partial class create_work_day_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Users",
                newName: "DeviceId");

            migrationBuilder.AddColumn<int>(
                name: "WorkHours",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkDay",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PauseTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StopTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDay", x => x.id);
                    table.ForeignKey(
                        name: "FK_WorkDay_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkDay_UserId",
                table: "WorkDay",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkDay");

            migrationBuilder.DropColumn(
                name: "WorkHours",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "Users",
                newName: "Token");
        }
    }
}
