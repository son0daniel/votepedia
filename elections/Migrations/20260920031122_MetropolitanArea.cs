using elections.Migrations.Seeder;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class MetropolitanArea : Migration
    {
        /// <inheritdoc />
        private readonly ISeeder seeder = new MetropolitanAreaSeeder();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "metropolitan_area_id",
                table: "municipality",
                type: "bigint",
                nullable: true,
                comment: "ID interno da região metropolitana na qual o município pode estar localizado.");

            migrationBuilder.CreateTable(
                name: "metropolitan_area",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da região metropolitana.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Nome da região metropolitana.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Tipo da região metropolitana.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo da região metropolitana.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metropolitan_area", x => x.id);
                    table.CheckConstraint("CK_metropolitan_area_type", "type IN ('REGIÃO METROPOLITANA', 'REGIÃO INTEGRADA DE DESENVOLVIMENTO')");
                },
                comment: "Tabela com regiões metropolitanas e regiões integradas de desenvolvimento")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_municipality_metropolitan_area_id",
                table: "municipality",
                column: "metropolitan_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_metropolitan_area_name",
                table: "metropolitan_area",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_metropolitan_area_uid",
                table: "metropolitan_area",
                column: "uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_municipality_metropolitan_area",
                table: "municipality",
                column: "metropolitan_area_id",
                principalTable: "metropolitan_area",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            seeder.Seed(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_municipality_metropolitan_area",
                table: "municipality");

            migrationBuilder.DropTable(
                name: "metropolitan_area");

            migrationBuilder.DropIndex(
                name: "IX_municipality_metropolitan_area_id",
                table: "municipality");

            migrationBuilder.DropColumn(
                name: "metropolitan_area_id",
                table: "municipality");
        }
    }
}
