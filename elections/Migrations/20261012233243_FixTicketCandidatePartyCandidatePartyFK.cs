using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class FixTicketCandidatePartyCandidatePartyFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticket_candidate_party_candidate_party_id",
                table: "ticket_candidate_party");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_candidate_party_candidate_party_id",
                table: "ticket_candidate_party",
                column: "candidate_party_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_candidate_party_candidate_party_id",
                table: "ticket_candidate_party",
                column: "candidate_party_id",
                principalTable: "candidate_party",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticket_candidate_party_candidate_party_id",
                table: "ticket_candidate_party");

            migrationBuilder.DropIndex(
                name: "IX_ticket_candidate_party_candidate_party_id",
                table: "ticket_candidate_party");

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_candidate_party_candidate_party_id",
                table: "ticket_candidate_party",
                column: "ticket_id",
                principalTable: "candidate_party",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
