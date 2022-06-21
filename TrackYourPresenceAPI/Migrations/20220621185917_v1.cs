using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackYourPresenceAPI.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkDay_Users_UserId",
                table: "WorkDay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkDay",
                table: "WorkDay");

            migrationBuilder.RenameTable(
                name: "WorkDay",
                newName: "WorkDays");

            migrationBuilder.RenameIndex(
                name: "IX_WorkDay_UserId",
                table: "WorkDays",
                newName: "IX_WorkDays_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "Uuid",
                table: "WorkDays",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WorkDays",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkDays",
                table: "WorkDays",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkDays_Users_UserId",
                table: "WorkDays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkDays_Users_UserId",
                table: "WorkDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkDays",
                table: "WorkDays");

            migrationBuilder.RenameTable(
                name: "WorkDays",
                newName: "WorkDay");

            migrationBuilder.RenameIndex(
                name: "IX_WorkDays_UserId",
                table: "WorkDay",
                newName: "IX_WorkDay_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "WorkDay",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WorkDay",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkDay",
                table: "WorkDay",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkDay_Users_UserId",
                table: "WorkDay",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
