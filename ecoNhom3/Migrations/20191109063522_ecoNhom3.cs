using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ecoNhom3.Migrations
{
    public partial class ecoNhom3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loais",
                columns: table => new
                {
                    MaLoai = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenLoai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loais", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCaps",
                columns: table => new
                {
                    MaNcc = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenNcc = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DienThoai = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    MoTa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCaps", x => x.MaNcc);
                });

            migrationBuilder.CreateTable(
                name: "PhanLoaiTVs",
                columns: table => new
                {
                    LoaiTV = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoaiThanhVien = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanLoaiTVs", x => x.LoaiTV);
                });

            migrationBuilder.CreateTable(
                name: "TrangThais",
                columns: table => new
                {
                    MaTt = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenTrangThai = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThais", x => x.MaTt);
                });

            migrationBuilder.CreateTable(
                name: "HangHoas",
                columns: table => new
                {
                    MaHh = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenHh = table.Column<string>(nullable: true),
                    MaLoai = table.Column<int>(nullable: false),
                    Hinh = table.Column<string>(nullable: true),
                    DonGia = table.Column<double>(nullable: false),
                    GiamGia = table.Column<double>(nullable: false),
                    MoTa = table.Column<string>(nullable: true),
                    MaNcc = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangHoas", x => x.MaHh);
                    table.ForeignKey(
                        name: "FK_HangHoas_Loais_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "Loais",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HangHoas_NhaCungCaps_MaNcc",
                        column: x => x.MaNcc,
                        principalTable: "NhaCungCaps",
                        principalColumn: "MaNcc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhVien",
                columns: table => new
                {
                    MaTv = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenTv = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    DienThoai = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    LoaiTv = table.Column<int>(nullable: false),
                    TaiKhoan = table.Column<string>(nullable: true),
                    MatKhau = table.Column<string>(nullable: true),
                    GioiTinh = table.Column<string>(nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhVien", x => x.MaTv);
                    table.ForeignKey(
                        name: "FK_ThanhVien_PhanLoaiTVs_LoaiTv",
                        column: x => x.LoaiTv,
                        principalTable: "PhanLoaiTVs",
                        principalColumn: "LoaiTV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuanSps",
                columns: table => new
                {
                    MaBl = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoiDung = table.Column<string>(nullable: true),
                    NgayBl = table.Column<DateTime>(nullable: true),
                    MaTv = table.Column<int>(nullable: false),
                    MaHh = table.Column<int>(nullable: false),
                    Mahh = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuanSps", x => x.MaBl);
                    table.ForeignKey(
                        name: "FK_BinhLuanSps_ThanhVien_MaTv",
                        column: x => x.MaTv,
                        principalTable: "ThanhVien",
                        principalColumn: "MaTv",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BinhLuanSps_HangHoas_Mahh",
                        column: x => x.Mahh,
                        principalTable: "HangHoas",
                        principalColumn: "MaHh",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    MaHd = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaTv = table.Column<int>(nullable: false),
                    NgayDat = table.Column<DateTime>(nullable: false),
                    NgayGiao = table.Column<DateTime>(nullable: true),
                    HoTen = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    SdtNguoiNhan = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(nullable: true),
                    PhiShip = table.Column<double>(nullable: false),
                    MaTrangThai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.MaHd);
                    table.ForeignKey(
                        name: "FK_HoaDons_TrangThais_MaTrangThai",
                        column: x => x.MaTrangThai,
                        principalTable: "TrangThais",
                        principalColumn: "MaTt",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDons_ThanhVien_MaTv",
                        column: x => x.MaTv,
                        principalTable: "ThanhVien",
                        principalColumn: "MaTv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHds",
                columns: table => new
                {
                    MaCTHD = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaHd = table.Column<int>(nullable: false),
                    Mahd = table.Column<int>(nullable: true),
                    MaHh = table.Column<int>(nullable: false),
                    DonGia = table.Column<double>(nullable: false),
                    GiamGia = table.Column<double>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHds", x => x.MaCTHD);
                    table.ForeignKey(
                        name: "FK_ChiTietHds_HangHoas_MaHh",
                        column: x => x.MaHh,
                        principalTable: "HangHoas",
                        principalColumn: "MaHh",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietHds_HoaDons_Mahd",
                        column: x => x.Mahd,
                        principalTable: "HoaDons",
                        principalColumn: "MaHd",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanSps_MaTv",
                table: "BinhLuanSps",
                column: "MaTv");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanSps_Mahh",
                table: "BinhLuanSps",
                column: "Mahh");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHds_MaHh",
                table: "ChiTietHds",
                column: "MaHh");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHds_Mahd",
                table: "ChiTietHds",
                column: "Mahd",
                unique: true,
                filter: "[Mahd] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HangHoas_MaLoai",
                table: "HangHoas",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_HangHoas_MaNcc",
                table: "HangHoas",
                column: "MaNcc");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaTrangThai",
                table: "HoaDons",
                column: "MaTrangThai");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaTv",
                table: "HoaDons",
                column: "MaTv");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhVien_LoaiTv",
                table: "ThanhVien",
                column: "LoaiTv");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinhLuanSps");

            migrationBuilder.DropTable(
                name: "ChiTietHds");

            migrationBuilder.DropTable(
                name: "HangHoas");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "Loais");

            migrationBuilder.DropTable(
                name: "NhaCungCaps");

            migrationBuilder.DropTable(
                name: "TrangThais");

            migrationBuilder.DropTable(
                name: "ThanhVien");

            migrationBuilder.DropTable(
                name: "PhanLoaiTVs");
        }
    }
}
