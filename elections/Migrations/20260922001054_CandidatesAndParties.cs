using System;
using elections.Migrations.Seeder;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class CandidatesAndParties : Migration
    {
        /// <inheritdoc />
        private readonly ISeeder seeder = new CandidatesAndPartiesSeeder();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "candidate",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno do candidato.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false, comment: "Nome completo do candidato.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    display_name = table.Column<string>(type: "varchar(255)", nullable: false, comment: "Nome de exibição do candidato — o que é exibido nos resultados eleitorais.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tse_name = table.Column<string>(type: "varchar(255)", nullable: false, comment: "Nome do candidato que aparece nos arquivos de resultados eleitorais do TSE que serve como referência de ingestão dos dados de votação. Às vezes pode ser igual o nome completo, ou um apelido, ou o nome completo sem algum sobrenome específico. Por isso a necessidade de haver esse campo.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_deceased = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "Campo booleano que determina se o candidato é falecido ou não"),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true, comment: "Data de nascimento do candidato. Pode ser um dado difícil de encontrar em eleições mais remotas, por isso é anulável."),
                    death_date = table.Column<DateOnly>(type: "date", nullable: true, comment: "Data de falecimento do candidato. É nula quando o candidato está vivo ou pelo mesmo motivo de birth_date."),
                    biography = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo do candidato.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidate", x => x.id);
                },
                comment: "Tabela com os candidatos e os respectivos dados pessoais de cada um.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "party",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno do partido.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false, comment: "Nome completo do partido.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    abbr = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Sigla do partido.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nr_voteable = table.Column<short>(type: "smallint", nullable: false, comment: "Número do partido."),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "Campo booleano que determina se o partido está ativo ou não."),
                    founded_at = table.Column<DateOnly>(type: "date", nullable: true, comment: "Data de fundação do partido."),
                    registered_at = table.Column<DateOnly>(type: "date", nullable: false, comment: "Data de registro do partido junto ao TSE."),
                    dissolved_at = table.Column<DateOnly>(type: "date", nullable: true, comment: "Data de dissolução/mudança de nome/fusão/incorporação do partido."),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo do partido.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_party", x => x.id);
                },
                comment: "Tabela com os partidos políticos. Pode ter registros do mesmo partido se ele mudou de nome ou se fundiu-se ou incorporou-se a outro partido.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "candidate_party",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da relação.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    candidate_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno do candidato."),
                    party_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno do partido.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidate_party", x => x.id);
                    table.ForeignKey(
                        name: "FK_candidate_party_candidate",
                        column: x => x.candidate_id,
                        principalTable: "candidate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_candidate_party_party",
                        column: x => x.party_id,
                        principalTable: "party",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Tabela de relação entre um candidato e um partido político — a filiação de um político a determinado partido.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_candidate_display_name",
                table: "candidate",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_candidate_name",
                table: "candidate",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_candidate_tse_name",
                table: "candidate",
                column: "tse_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_candidate_uid",
                table: "candidate",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_candidate_party",
                table: "candidate_party",
                columns: new[] { "candidate_id", "party_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_candidate_party_party_id",
                table: "candidate_party",
                column: "party_id");

            migrationBuilder.CreateIndex(
                name: "IX_party_uid",
                table: "party",
                column: "uid",
                unique: true);

            seeder.Seed(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidate_party");

            migrationBuilder.DropTable(
                name: "candidate");

            migrationBuilder.DropTable(
                name: "party");
        }
    }
}
