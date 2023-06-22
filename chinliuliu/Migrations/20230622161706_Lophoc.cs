using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chinliuliu.Migrations
{
    /// <inheritdoc />
    public partial class Lophoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lophoc",
                columns: table => new
                {
                    Malop = table.Column<string>(type: "TEXT", nullable: false),
                    Tenlop = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lophoc", x => x.Malop);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lophoc");
        }
    }
}
