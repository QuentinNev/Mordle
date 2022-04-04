using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mordle.Migrations.SQLiteMigrations
{
    public partial class AddWordModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    word = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 1, "Bonjour" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 2, "Voiture" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 3, "Raquette" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 4, "Anticonstitutionnellement" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 5, "Repas" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 6, "Escalade" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 7, "Poule" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 8, "Recherche" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 9, "Soigner" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 10, "Trompe" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 11, "Balancer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
