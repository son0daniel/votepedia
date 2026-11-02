using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class NationalStatistic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_national_statistic",
                table: "ticket_election_round_statistic",
                type: "tinyint(1)",
                nullable: true,
                comment: "Determina se a estatística vem de uma agregação nacional. Resultados gerais de eleições presidenciais têm esse campo nulo.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_national_statistic",
                table: "ticket_election_round_statistic");
        }
    }
}
