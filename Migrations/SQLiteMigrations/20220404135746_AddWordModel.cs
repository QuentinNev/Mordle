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
                values: new object[] { 1, "PAPILLON" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 2, "TROUPEAU" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 3, "ADORABLE" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 4, "ENTREPOT" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 5, "GOURMAND" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 6, "IRONIQUE" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 7, "LAPEREAU" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 8, "LOCUTEUR" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 9, "MARECAGE" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 10, "NETTOYER" });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "word" },
                values: new object[] { 11, "ORNEMENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
