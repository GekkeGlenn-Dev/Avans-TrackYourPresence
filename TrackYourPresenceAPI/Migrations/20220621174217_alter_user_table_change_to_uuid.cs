using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackYourPresenceAPI.Migrations
{
    public partial class alter_user_table_change_to_uuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "String",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "Token",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "String",
                table: "Users",
                type: "int",
                nullable: true);
        }
    }
}
