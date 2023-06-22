using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chinliuliu.Migrations
{
    /// <inheritdoc />
    public partial class Sinhvien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sinhvien",
                columns: table => new
                {
                    Masinhvien = table.Column<string>(type: "TEXT", nullable: false),
                    Hoten = table.Column<string>(type: "TEXT", nullable: false),
                    Malop = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinhvien", x => x.Masinhvien);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sinhvien");
        }
    }
}
