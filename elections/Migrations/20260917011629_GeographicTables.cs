using elections.Migrations.Seeder;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class GeographicTables : Migration
    {
        /// <inheritdoc />
        ISeeder seeder = new GeographicTablesSeeder();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "macroregion",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da macrorregião.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Nome da macrorregião.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    abbr = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false, comment: "Sigla da macrorregião.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo da macrorregião.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_macroregion", x => x.id);
                    table.CheckConstraint("CK_macroregion_abbr", "abbr REGEXP '^[A-Z]{1,2}$'");
                },
                comment: "Tabela com as 5 macrorregiões brasileiras + 1 registro do Exterior.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "state",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da UF.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false, comment: "Nome da UF.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    abbr = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false, comment: "Sigla da UF.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    macroregion_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da macroregião na qual a UF está localizada."),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo da UF.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state", x => x.id);
                    table.CheckConstraint("CK_state_abbr", "abbr REGEXP '^[A-Z]{2}$'");
                    table.ForeignKey(
                        name: "FK_state_macroregion",
                        column: x => x.macroregion_id,
                        principalTable: "macroregion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabela com 27 UFs do Brasil + 1 registro indicando Exterior.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "municipality",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno do município.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false, comment: "Nome do município.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tse_id = table.Column<long>(type: "bigint", nullable: false, comment: "Código do TSE ligado ao município que pode ser visto nos arquivos de resultados."),
                    is_capital = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "Determina se o município é a capital do Estado. Válido apenas para cidades brasileiras."),
                    state_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da UF na qual o município está localizado."),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo do município.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipality", x => x.id);
                    table.ForeignKey(
                        name: "FK_municipality_state",
                        column: x => x.state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabela com os municípios brasileiros ou cidades estrangeiras com votações de eleições brasilieiras. Pode aceitar duplicatas de cidades do exterior porque, nesses casos, o TSE dá um código diferente a cada seção eleitoral, ainda que da mesma localidade.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_macroregion_abbr",
                table: "macroregion",
                column: "abbr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_macroregion_name",
                table: "macroregion",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_macroregion_uid",
                table: "macroregion",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_municipality_state_id",
                table: "municipality",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_municipality_tse_id",
                table: "municipality",
                column: "tse_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_municipality_uid",
                table: "municipality",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_state_abbr",
                table: "state",
                column: "abbr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_state_macroregion_id",
                table: "state",
                column: "macroregion_id");

            migrationBuilder.CreateIndex(
                name: "IX_state_name",
                table: "state",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_state_uid",
                table: "state",
                column: "uid",
                unique: true);

            seeder.Seed(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "municipality");

            migrationBuilder.DropTable(
                name: "state");

            migrationBuilder.DropTable(
                name: "macroregion");
        }
    }
}
