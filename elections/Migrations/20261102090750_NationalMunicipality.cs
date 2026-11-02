using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class NationalMunicipality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_national",
                table: "municipality",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "Determina se o munícipio é brasileiro ou não.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_national",
                table: "municipality");
        }
    }
}
