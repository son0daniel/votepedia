using System;
using elections.Migrations.Seeder;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class TicketTables : Migration
    {
        /// <inheritdoc />
        private readonly ISeeder seeder = new TicketTablesSeeder();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "federation",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da federação partidária.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nome formal da federação partidária.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    display_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "Nome de exibição da federação partidária.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    registered_at = table.Column<DateOnly>(type: "date", nullable: false, comment: "Data de registro da federação partidária."),
                    dissolved_at = table.Column<DateOnly>(type: "date", nullable: true, comment: "Data de dissolução da federação partidária."),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo da federação partidária.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_federation", x => x.id);
                    table.CheckConstraint("CK_federation_date", "dissolved_at IS NULL OR registered_at < dissolved_at");
                },
                comment: "Tabela com as federações partidárias, instituto criado pela Lei nº 14.208/2021")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da chapa.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    color = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "Cor usada para representar a chapa em mapas eleitorais.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "Campo opcional usado para contextualizar o registro da chapa eleitoral.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo da chapa.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.id);
                    table.CheckConstraint("CK_ticket_color", "color IS NULL \r\nOR color IN ('Blue', 'Red', 'Green', 'DarkBlue', 'Yellow', 'Orange', 'Purple', 'Grey')");
                },
                comment: "Tabela com as chapas eleitorais.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "federation_party",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da relação.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    federation_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da federação partidária."),
                    party_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno do partido político.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_federation_party", x => x.id);
                    table.ForeignKey(
                        name: "FK_federation_party_federation_id",
                        column: x => x.federation_id,
                        principalTable: "federation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_federation_party_party_id",
                        column: x => x.party_id,
                        principalTable: "party",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabela de relação entre a federação partidária e o partido político.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "coalition",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da coligação.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: true, comment: "Nome da coligação.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ticket_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da chapa eleitoral à qual a coligação está vinculada."),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo da coligação.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coalition", x => x.id);
                    table.ForeignKey(
                        name: "FK_coalition_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "ticket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabela com as coligações.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_candidate_party",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da relação.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nr_level = table.Column<int>(type: "int", nullable: false, comment: "A hierarquia do candidato em relação à chapa. 1 é titular; 2 é vice/primeiro suplente; e 3 para segundo suplente."),
                    ticket_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da chapa eleitoral."),
                    candidate_party_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da filiação partidária do candidato.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_candidate_party", x => x.id);
                    table.CheckConstraint("CK_ticket_candidate_party_nr_level", "nr_level IN (1, 2, 3)");
                    table.ForeignKey(
                        name: "FK_ticket_candidate_party_candidate_party_id",
                        column: x => x.ticket_id,
                        principalTable: "candidate_party",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticket_candidate_party_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "ticket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabela de relação entre a chapa eleitoral e o candidato com filiação partidária.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_election_round",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da relação.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ticket_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da chapa eleitoral."),
                    election_round_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno do turno da eleição.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_election_round", x => x.id);
                    table.ForeignKey(
                        name: "FK_ticket_election_round_election_round_id",
                        column: x => x.election_round_id,
                        principalTable: "election_round",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ticket_election_round_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "ticket",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabela de relação que expressa a participação de determinada chapa eleitoral em determinado turno de uma eleição. Útil para ingestão de votos usando arquivos de resultados csv do TSE.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "coalition_member",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da relação.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    coalition_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da coligação."),
                    party_id = table.Column<long>(type: "bigint", nullable: true, comment: "ID interno do partido político coligado."),
                    federation_id = table.Column<long>(type: "bigint", nullable: true, comment: "ID interno da federação partidária coligada.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coalition_member", x => x.id);
                    table.CheckConstraint("CK_coalition_member_party_or_federation", "(party_id IS NOT NULL AND federation_id IS NULL)\r\nOR\r\n(party_id IS NULL AND federation_id IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_coalition_member_coalition_coalition_id",
                        column: x => x.coalition_id,
                        principalTable: "coalition",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_coalition_member_federation_id",
                        column: x => x.federation_id,
                        principalTable: "federation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_coalition_member_party_id",
                        column: x => x.party_id,
                        principalTable: "party",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabela de relação que expressa o vínculo de um partido ou federação partidária a uma coligação")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_coalition_ticket_id",
                table: "coalition",
                column: "ticket_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_coalition_uid",
                table: "coalition",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_coalition_member_federation",
                table: "coalition_member",
                columns: new[] { "coalition_id", "federation_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_coalition_member_federation_id",
                table: "coalition_member",
                column: "federation_id");

            migrationBuilder.CreateIndex(
                name: "IX_coalition_member_party",
                table: "coalition_member",
                columns: new[] { "coalition_id", "party_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_coalition_member_party_id",
                table: "coalition_member",
                column: "party_id");

            migrationBuilder.CreateIndex(
                name: "IX_federation_display_name",
                table: "federation",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_federation_name",
                table: "federation",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_federation_uid",
                table: "federation",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_federation_party",
                table: "federation_party",
                columns: new[] { "federation_id", "party_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_federation_party_party_id",
                table: "federation_party",
                column: "party_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_uid",
                table: "ticket",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_candidate_party",
                table: "ticket_candidate_party",
                columns: new[] { "ticket_id", "candidate_party_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_candidate_party_nr_level",
                table: "ticket_candidate_party",
                columns: new[] { "ticket_id", "nr_level" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_election_round",
                table: "ticket_election_round",
                columns: new[] { "ticket_id", "election_round_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_election_round_election_round_id",
                table: "ticket_election_round",
                column: "election_round_id");

            seeder.Seed(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coalition_member");

            migrationBuilder.DropTable(
                name: "federation_party");

            migrationBuilder.DropTable(
                name: "ticket_candidate_party");

            migrationBuilder.DropTable(
                name: "ticket_election_round");

            migrationBuilder.DropTable(
                name: "coalition");

            migrationBuilder.DropTable(
                name: "federation");

            migrationBuilder.DropTable(
                name: "ticket");
        }
    }
}
