using System;
using elections.Migrations.Seeder;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class ElectionTables : Migration
    {
        /// <inheritdoc />
        private readonly ISeeder seeder = new ElectionTablesSeeder();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "election",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da eleição.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    year = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Cargo em disputa da eleição.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "O tipo da eleição.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    municipality_id = table.Column<long>(type: "bigint", nullable: true, comment: "ID interno do munícipio caso o cargo em disputa for Prefeito, Vereador ou algum outro cargo correlato a nível municipal."),
                    state_id = table.Column<long>(type: "bigint", nullable: true, comment: "ID interno da UF caso o cargo em disputa for Governador do Estado, Deputado Estadual ou Federal, Senador, ou algum outro cargo com relação direta à UF."),
                    FK_election_municipality_id = table.Column<long>(type: "bigint", nullable: true),
                    FK_election_state_id = table.Column<long>(type: "bigint", nullable: true),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo da eleição.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election", x => x.id);
                    table.CheckConstraint("CK_election_role", "role IN \r\n(\r\n    'PRESIDENTE', \r\n    'GOVERNADOR', \r\n    'PREFEITO', \r\n    'SENADOR',\r\n    'DEPUTADO FEDERAL',\r\n    'DEPUTADO ESTADUAL',\r\n    'VEREADOR'\r\n)");
                    table.CheckConstraint("CK_election_role_scope", "(\r\n    role = 'PRESIDENTE' \r\n    AND municipality_id IS NULL\r\n    AND state_id IS NULL\r\n)\r\nOR\r\n(   role IN\r\n    (\r\n        'GOVERNADOR',\r\n        'SENADOR',\r\n        'DEPUTADO FEDERAL',\r\n        'DEPUTADO ESTADUAL'\r\n    )\r\n    AND municipality_id IS NULL \r\n    AND state_id IS NOT NULL\r\n)\r\nOR\r\n(   role IN\r\n    (\r\n        'PREFEITO',\r\n        'VEREADOR'\r\n    )\r\n    AND municipality_id IS NOT NULL \r\n    AND state_id IS NULL\r\n)");
                    table.CheckConstraint("CK_election_type", "type IN \r\n(\r\n    'Ordinária', \r\n    'Suplementar'\r\n)");
                    table.ForeignKey(
                        name: "FK_election_municipality_FK_election_municipality_id",
                        column: x => x.FK_election_municipality_id,
                        principalTable: "municipality",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_election_state_FK_election_state_id",
                        column: x => x.FK_election_state_id,
                        principalTable: "state",
                        principalColumn: "id");
                },
                comment: "Tabela com as eleições.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "election_round",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno do turno da eleição.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nr_round = table.Column<int>(type: "int", nullable: false, comment: "Número do turno da eleição."),
                    held_at = table.Column<DateOnly>(type: "date", nullable: false, comment: "Data da realização do turno da eleição."),
                    election_id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da eleição."),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo do turno da eleição.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_election_round", x => x.id);
                    table.CheckConstraint("CK_election_round", "nr_round IN (1, 2)");
                    table.ForeignKey(
                        name: "FK_election_round_election_id",
                        column: x => x.election_id,
                        principalTable: "election",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tabela com os turnos de eleições.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_election_FK_election_municipality_id",
                table: "election",
                column: "FK_election_municipality_id");

            migrationBuilder.CreateIndex(
                name: "IX_election_FK_election_state_id",
                table: "election",
                column: "FK_election_state_id");

            migrationBuilder.CreateIndex(
                name: "IX_election_uid",
                table: "election",
                column: "uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_election_round_election_nr_round",
                table: "election_round",
                columns: new[] { "election_id", "nr_round" });

            migrationBuilder.CreateIndex(
                name: "IX_election_round_uid",
                table: "election_round",
                column: "uid",
                unique: true);

            seeder.Seed(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "election_round");

            migrationBuilder.DropTable(
                name: "election");
        }
    }
}
