using Microsoft.EntityFrameworkCore.Migrations;

namespace elections.Migrations.Seeder
{
    public interface ISeeder
    {
        public void Seed(MigrationBuilder migrationBuilder);
    }
}
