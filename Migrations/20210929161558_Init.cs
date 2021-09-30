using Microsoft.EntityFrameworkCore.Migrations;

namespace AAF.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageFront = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    Stock = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crafts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ringers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageFront = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    Stock = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ringers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Texteis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageFront = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    Stock = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Texteis", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Crafts");

            migrationBuilder.DropTable(
                name: "Ringers");

            migrationBuilder.DropTable(
                name: "Texteis");
        }
    }
}
