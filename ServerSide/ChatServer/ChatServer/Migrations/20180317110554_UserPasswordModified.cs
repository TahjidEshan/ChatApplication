using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChatServer.Migrations
{
    public partial class UserPasswordModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "PasswordHash",
            //    table: "Users",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldNullable: true);
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                nullable: true);
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                nullable: true);
            //migrationBuilder.AlterColumn<string>(
            //    name: "PasswordHash",
            //    table: "Users",
            //    nullable: true,
            //    oldClrType: typeof(byte[]),
            //    oldNullable: true);
        }
    }
}
