using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCore.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoNV = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TenNV = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TenLot = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Phai = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Luong = table.Column<double>(type: "float", nullable: true),
                    Ma_NQL = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false),
                    PHG = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MaNV);
                });

            migrationBuilder.CreateTable(
                name: "ThanNhan",
                columns: table => new
                {
                    TenNV = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Ma_NVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phai = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuanHe = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanNhan", x => x.TenNV);
                    table.ForeignKey(
                        name: "FK_ThanNhan_NhanVien_Ma_NVien",
                        column: x => x.Ma_NVien,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThanNhan_Ma_NVien",
                table: "ThanNhan",
                column: "Ma_NVien");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThanNhan");

            migrationBuilder.DropTable(
                name: "NhanVien");
        }
    }
}
