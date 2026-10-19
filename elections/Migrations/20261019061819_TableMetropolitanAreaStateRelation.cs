using elections.Migrations.Seeder;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class TableMetropolitanAreaStateRelation : Migration
    {
        /// <inheritdoc />
        private readonly ISeeder seeder = new TableMetropolitanAreaStateRelationSeeder();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "metropolitan_area_state",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da relação.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    metropolitan_area_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da Região Metropolitana."),
                    state_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno do Estado.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metropolitan_area_state", x => x.id);
                    table.ForeignKey(
                        name: "FK_metropolitan_area_state_metropolitan_area_id",
                        column: x => x.metropolitan_area_id,
                        principalTable: "metropolitan_area",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_metropolitan_area_state_state_id",
                        column: x => x.state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabela de relação entre Regiões Metropolitanas e Estados.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_metropolitan_area_state_metropolitan_area_id",
                table: "metropolitan_area_state",
                column: "metropolitan_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_metropolitan_area_state_state_id",
                table: "metropolitan_area_state",
                column: "state_id");

            seeder.Seed(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "metropolitan_area_state");
        }
    }
}
