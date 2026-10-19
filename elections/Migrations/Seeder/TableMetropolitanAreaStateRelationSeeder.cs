using Microsoft.EntityFrameworkCore.Migrations;

namespace elections.Migrations.Seeder
{
    public class TableMetropolitanAreaStateRelationSeeder : ISeeder
    {
        public void Seed(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "metropolitan_area_state",
                columns: ["id", "metropolitan_area_id", "state_id"],
                values: new object[,]
                {
                    { 1, 1, 25 },
                    { 2, 2, 25 },
                    { 3, 3, 21 },
                    { 4, 4, 25 },
                    { 5, 5, 9 },
                    { 6, 6, 25 },
                    { 7, 7, 16 },
                    { 8, 8, 7 },
                    { 9, 8, 9 },
                    { 10, 8, 13 },
                    { 11, 9, 21 },
                    { 12, 10, 13 },
                    { 13, 11, 25 },
                    { 14, 12, 6 },
                    { 15, 13, 25 },
                    { 16, 14, 19 },
                    { 17, 15, 8 },
                    { 18, 16, 20 },
                    { 19, 17, 25 },
                    { 20, 18, 18 },
                    { 21, 18, 10 },
                    { 22, 19, 25 },
                    { 23, 20, 5 },
                    { 24, 21, 16 },
                    { 25, 22, 26 },
                    { 26, 23, 5 },
                    { 27, 23, 17 },
                    { 28, 24, 25 },
                    { 29, 25, 24 },
                    { 30, 26, 15 },
                    { 31, 27, 2 },
                    { 32, 28, 10 },
                    { 33, 29, 14 },
                    { 34, 30, 17 },
                    { 35, 31, 11 }
                });
        }
    }
}
