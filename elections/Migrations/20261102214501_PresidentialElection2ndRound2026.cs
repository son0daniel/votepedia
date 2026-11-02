using elections.Migrations.Seeder;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class PresidentialElection2ndRound2026 : Migration
    {
        /// <inheritdoc />
        private readonly ISeeder seeder = new PresidentialElection2ndRound2026Seeder();
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
