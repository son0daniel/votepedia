using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class ElectionResultsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ticket_election_round_statistic",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno das estatísticas.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ticket_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno do turno da chapa eleitoral."),
                    election_round_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno do turno da eleição."),
                    municipality_id = table.Column<long>(type: "bigint", nullable: true, comment: "ID interno do Município."),
                    metropolitan_area_id = table.Column<long>(type: "bigint", nullable: true, comment: "ID interno da Região Metropolitana."),
                    state_id = table.Column<long>(type: "bigint", nullable: true, comment: "ID interno da UF."),
                    macroregion_id = table.Column<long>(type: "bigint", nullable: true, comment: "ID interno da Macrorregião."),
                    ticket_votes_count = table.Column<int>(type: "int", nullable: false, comment: "Contagem de votos da chapa eleitoral."),
                    ticket_position_nr = table.Column<int>(type: "int", nullable: false, comment: "Posição da chapa eleitoral."),
                    ticket_valid_votes_pp = table.Column<float>(type: "float", nullable: true, comment: "Porcentagem de votos válidos da chapa eleitoral."),
                    ticket_total_votes_pp = table.Column<float>(type: "float", nullable: true, comment: "Porcentagem de votos da chapa em relação à contagem total de votos."),
                    ticket_registered_votes_pp = table.Column<float>(type: "float", nullable: false, comment: "Porcentagem de votos da chapa em relação à contagem total de inscritos."),
                    registered_voters_count = table.Column<int>(type: "int", nullable: false, comment: "Contagem de inscritos."),
                    turnout_count = table.Column<int>(type: "int", nullable: false, comment: "Contagem de comparecimento."),
                    abstention_count = table.Column<int>(type: "int", nullable: false, comment: "Contagem de abstenção."),
                    valid_votes_count = table.Column<int>(type: "int", nullable: false, comment: "Contagem de todos os votos válidos."),
                    invalid_votes_count = table.Column<int>(type: "int", nullable: false, comment: "Contagem de votos inválidos."),
                    null_votes_count = table.Column<int>(type: "int", nullable: false, comment: "Contagem de votos nulos."),
                    blank_votes_count = table.Column<int>(type: "int", nullable: false, comment: "Contagem de votos em branco."),
                    anulled_votes_count = table.Column<int>(type: "int", nullable: false, comment: "Contagem de votos que foram anulados por decisão da Justiça Eleitoral."),
                    electoral_alienation_count = table.Column<int>(type: "int", nullable: false, comment: "Contagem da alienação eleitoral (abstenção + votos inválidos)."),
                    candidates_count = table.Column<int>(type: "int", nullable: false, comment: "Contagem de candidatos. Invariável independente do nível de agregação."),
                    turnout_pp = table.Column<float>(type: "float", nullable: false, comment: "Porcentagem de comparecimento. Relativa ao total de inscritos."),
                    abstention_pp = table.Column<float>(type: "float", nullable: false, comment: "Porcentagem de abstenção. Relativa ao total de inscritos."),
                    valid_votes_pp = table.Column<float>(type: "float", nullable: true, comment: "Porcentagem de votos válidos relativa ao total de votos."),
                    valid_votes_registered_pp = table.Column<float>(type: "float", nullable: false, comment: "Porcentagem de votos válidos relativa aos inscritos."),
                    invalid_votes_pp = table.Column<float>(type: "float", nullable: true, comment: "Porcentagem de votos inválidos relativa ao total de votos."),
                    invalid_votes_registered_pp = table.Column<float>(type: "float", nullable: false, comment: "Porcentagem de votos inválidos relativa aos inscritos."),
                    null_votes_pp = table.Column<float>(type: "float", nullable: true, comment: "Porcentagem de votos nulos relativa ao total de votos."),
                    null_votes_registered_pp = table.Column<float>(type: "float", nullable: false, comment: "Porcentagem de votos nulos relativa aos inscritos."),
                    blank_votes_pp = table.Column<float>(type: "float", nullable: true, comment: "Porcentagem de votos em branco relativa ao total de votos."),
                    blank_votes_registered_pp = table.Column<float>(type: "float", nullable: false, comment: "Porcentagem de votos em branco relativa aos inscritos."),
                    anulled_votes_pp = table.Column<float>(type: "float", nullable: true, comment: "Porcentagem de votos anulados relativa ao total de votos."),
                    anulled_votes_registered_pp = table.Column<float>(type: "float", nullable: false, comment: "Porcentagem de votos anulados relativa aos inscritos."),
                    electoral_alienation_pp = table.Column<float>(type: "float", nullable: false, comment: "Porcentagem da alienação eleitoral relativa ao total de inscritos."),
                    ticket_status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Status da chapa eleitoral no turno da eleição.Tem sempre o mesmo valor independente do nível de agregação (localidade).")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_election_round_statistic", x => x.id);
                    table.CheckConstraint("CK_ticket_election_round_statistic_ticket_status", "ticket_status IN \r\n(\r\n    'ELEITO', \r\n    'NÃO ELEITO', \r\n    '2º TURNO'\r\n)");
                    table.ForeignKey(
                        name: "FK_ticket_election_round_statistic_election_round_id",
                        column: x => x.election_round_id,
                        principalTable: "election_round",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticket_election_round_statistic_macroregion_id",
                        column: x => x.macroregion_id,
                        principalTable: "macroregion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticket_election_round_statistic_metropolitan_area_id",
                        column: x => x.metropolitan_area_id,
                        principalTable: "metropolitan_area",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticket_election_round_statistic_municipality_id",
                        column: x => x.municipality_id,
                        principalTable: "municipality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticket_election_round_statistic_state_id",
                        column: x => x.state_id,
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticket_election_round_statistic_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "ticket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabela com as estatísticas da chapa eleitoral e as gerais do turno da eleição. As estatísticas do mesmo turno eleitoral variam conforme o nível de agregação (localidade) com exceção ao status da chapa e à quantidade de candidatos.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_election_round_statistic_election_round_id",
                table: "ticket_election_round_statistic",
                column: "election_round_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_election_round_statistic_macroregion",
                table: "ticket_election_round_statistic",
                columns: new[] { "ticket_id", "election_round_id", "macroregion_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_election_round_statistic_macroregion_id",
                table: "ticket_election_round_statistic",
                column: "macroregion_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_election_round_statistic_metropolitan_area",
                table: "ticket_election_round_statistic",
                columns: new[] { "ticket_id", "election_round_id", "metropolitan_area_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_election_round_statistic_metropolitan_area_id",
                table: "ticket_election_round_statistic",
                column: "metropolitan_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_election_round_statistic_municipality",
                table: "ticket_election_round_statistic",
                columns: new[] { "ticket_id", "election_round_id", "municipality_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_election_round_statistic_municipality_id",
                table: "ticket_election_round_statistic",
                column: "municipality_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_election_round_statistic_state",
                table: "ticket_election_round_statistic",
                columns: new[] { "ticket_id", "election_round_id", "state_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_election_round_statistic_state_id",
                table: "ticket_election_round_statistic",
                column: "state_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket_election_round_statistic");
        }
    }
}
