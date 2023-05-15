using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCore.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Client_ClientAddress_ID",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_Address_Address_ID1",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_Address_ID1",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Address_ClientAddress_ID",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Address_ID1",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ClientAddress_ID",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "Address_ID",
                table: "Address",
                newName: "Addr_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Address_Address_ID",
                table: "Client",
                column: "Address_ID",
                principalTable: "Address",
                principalColumn: "Addr_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Address_Address_ID",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "Addr_ID",
                table: "Address",
                newName: "Address_ID");

            migrationBuilder.AddColumn<Guid>(
                name: "Address_ID1",
                table: "Client",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClientAddress_ID",
                table: "Address",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_Address_ID1",
                table: "Client",
                column: "Address_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ClientAddress_ID",
                table: "Address",
                column: "ClientAddress_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Client_ClientAddress_ID",
                table: "Address",
                column: "ClientAddress_ID",
                principalTable: "Client",
                principalColumn: "Address_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Address_Address_ID1",
                table: "Client",
                column: "Address_ID1",
                principalTable: "Address",
                principalColumn: "Address_ID");
        }
    }
}
