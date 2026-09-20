using elections.Migrations.Seeder;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class StateMacroregionUpperCaseNormalization : Migration
    {
        /// <inheritdoc />
        private readonly ISeeder seeder = new StateMacroregionUpperCaseNormalizationSeeder();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            seeder.Seed(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
