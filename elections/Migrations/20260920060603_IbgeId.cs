using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class IbgeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ibge_id",
                table: "municipality",
                type: "bigint",
                nullable: true,
                comment: "ID do município no IBGE.");

            migrationBuilder.CreateIndex(
                name: "IX_municipality_ibge_id",
                table: "municipality",
                column: "ibge_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_municipality_ibge_id",
                table: "municipality");

            migrationBuilder.DropColumn(
                name: "ibge_id",
                table: "municipality");
        }
    }
}
