using Microsoft.EntityFrameworkCore.Migrations;

namespace elections.Migrations.Seeder
{
    public class GeographicTablesSeeder : ISeeder
    {
        public void Seed(MigrationBuilder migrationBuilder)
        {
            #region Macroregions
            migrationBuilder.InsertData(
                table: "macroregion",
                columns: ["id", "uid", "name", "abbr"],
                values: new object[,]
                {
                    { 1, Guid.NewGuid().ToString(), "Centro-Oeste", "CO" },
                    { 2, Guid.NewGuid().ToString(), "Nordeste", "NE" },
                    { 3, Guid.NewGuid().ToString(), "Norte", "N" },
                    { 4, Guid.NewGuid().ToString(), "Sudeste", "SE" },
                    { 5, Guid.NewGuid().ToString(), "Sul", "S" },
                    { 6, Guid.NewGuid().ToString(), "Exterior", "ZZ" }
                }
            );
            #endregion

            #region States
            migrationBuilder.InsertData(
                table: "state",
                columns: ["id", "uid", "name", "abbr", "macroregion_id"],
                values: new object[,]
                {
                    { 1, Guid.NewGuid().ToString(), "Acre", "AC", 3 },
                    { 2, Guid.NewGuid().ToString(), "Alagoas", "AL", 2 },
                    { 3, Guid.NewGuid().ToString(), "Amapá", "AP", 3 },
                    { 4, Guid.NewGuid().ToString(), "Amazonas", "AM", 3 },
                    { 5, Guid.NewGuid().ToString(), "Bahia", "BA", 2 },
                    { 6, Guid.NewGuid().ToString(), "Ceará", "CE", 2 },
                    { 7, Guid.NewGuid().ToString(), "Distrito Federal", "DF", 1 },
                    { 8, Guid.NewGuid().ToString(), "Espírito Santo", "ES", 4 },
                    { 9, Guid.NewGuid().ToString(), "Goiás", "GO", 1 },
                    { 10, Guid.NewGuid().ToString(), "Maranhão", "MA", 2 },
                    { 11, Guid.NewGuid().ToString(), "Mato Grosso", "MT", 1 },
                    { 12, Guid.NewGuid().ToString(), "Mato Grosso do Sul", "MS", 1 },
                    { 13, Guid.NewGuid().ToString(), "Minas Gerais", "MG", 4 },
                    { 14, Guid.NewGuid().ToString(), "Pará", "PA", 3 },
                    { 15, Guid.NewGuid().ToString(), "Paraíba", "PB", 2 },
                    { 16, Guid.NewGuid().ToString(), "Paraná", "PR", 5 },
                    { 17, Guid.NewGuid().ToString(), "Pernambuco", "PE", 2 },
                    { 18, Guid.NewGuid().ToString(), "Piauí", "PI", 2 },
                    { 19, Guid.NewGuid().ToString(), "Rio de Janeiro", "RJ", 4 },
                    { 20, Guid.NewGuid().ToString(), "Rio Grande do Norte", "RN", 2 },
                    { 21, Guid.NewGuid().ToString(), "Rio Grande do Sul", "RS", 5 },
                    { 22, Guid.NewGuid().ToString(), "Rondônia", "RO", 3 },
                    { 23, Guid.NewGuid().ToString(), "Roraima", "RR", 3 },
                    { 24, Guid.NewGuid().ToString(), "Santa Catarina", "SC", 5 },
                    { 25, Guid.NewGuid().ToString(), "São Paulo", "SP", 4 },
                    { 26, Guid.NewGuid().ToString(), "Sergipe", "SE", 2 },
                    { 27, Guid.NewGuid().ToString(), "Tocantins", "TO", 3 },
                    { 28, Guid.NewGuid().ToString(), "Exterior", "ZZ", 6 }
                }
            );
            #endregion
        }
    }
}
