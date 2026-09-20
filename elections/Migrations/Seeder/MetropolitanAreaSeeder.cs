using elections.Constants;
using Microsoft.EntityFrameworkCore.Migrations;

namespace elections.Migrations.Seeder
{
    public class MetropolitanAreaSeeder : ISeeder
    {
        public void Seed(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "metropolitan_area",
                columns: ["id", "uid", "name", "type"],
                values: new object[,]
                {
                    { 1, Guid.NewGuid().ToString(), "GRANDE SÃO PAULO", Constant.MetropolitanAreaType.Standard },
                    { 2, Guid.NewGuid().ToString(), "VALE DO PARAÍBA E LITORAL NORTE", Constant.MetropolitanAreaType.Standard },
                    { 3, Guid.NewGuid().ToString(), "SERRA GAÚCHA", Constant.MetropolitanAreaType.Standard },
                    { 4, Guid.NewGuid().ToString(), "SÃO JOSÉ DO RIO PRETO", Constant.MetropolitanAreaType.Standard },
                    { 5, Guid.NewGuid().ToString(), "GRANDE GOIÂNIA", Constant.MetropolitanAreaType.Standard },
                    { 6, Guid.NewGuid().ToString(), "JUNDIAÍ", Constant.MetropolitanAreaType.Standard },
                    { 7, Guid.NewGuid().ToString(), "LONDRINA", Constant.MetropolitanAreaType.Standard },
                    { 8, Guid.NewGuid().ToString(), "DISTRITO FEDERAL E ENTORNO", Constant.MetropolitanAreaType.IntegratedAreaDevelopment },
                    { 9, Guid.NewGuid().ToString(), "GRANDE PORTO ALEGRE", Constant.MetropolitanAreaType.Standard },
                    { 10, Guid.NewGuid().ToString(), "GRANDE BELO HORIZONTE", Constant.MetropolitanAreaType.Standard },
                    { 11, Guid.NewGuid().ToString(), "SOROCABA", Constant.MetropolitanAreaType.Standard },
                    { 12, Guid.NewGuid().ToString(), "GRANDE FORTALEZA", Constant.MetropolitanAreaType.Standard },
                    { 13, Guid.NewGuid().ToString(), "RIBEIRÃO PRETO", Constant.MetropolitanAreaType.Standard },
                    { 14, Guid.NewGuid().ToString(), "GRANDE RIO", Constant.MetropolitanAreaType.Standard },
                    { 15, Guid.NewGuid().ToString(), "GRANDE VITÓRIA", Constant.MetropolitanAreaType.Standard },
                    { 16, Guid.NewGuid().ToString(), "GRANDE NATAL", Constant.MetropolitanAreaType.Standard },
                    { 17, Guid.NewGuid().ToString(), "PIRACICABA", Constant.MetropolitanAreaType.Standard },
                    { 18, Guid.NewGuid().ToString(), "GRANDE TERESINA", Constant.MetropolitanAreaType.Standard },
                    { 19, Guid.NewGuid().ToString(), "BAIXADA SANTISTA", Constant.MetropolitanAreaType.Standard },
                    { 20, Guid.NewGuid().ToString(), "GRANDE SALVADOR", Constant.MetropolitanAreaType.Standard },
                    { 21, Guid.NewGuid().ToString(), "GRANDE CURITIBA", Constant.MetropolitanAreaType.Standard },
                    { 22, Guid.NewGuid().ToString(), "GRANDE ARACAJU", Constant.MetropolitanAreaType.Standard },
                    { 23, Guid.NewGuid().ToString(), "POLO PETROLINA E JUAZEIRO", Constant.MetropolitanAreaType.IntegratedAreaDevelopment },
                    { 24, Guid.NewGuid().ToString(), "CAMPINAS", Constant.MetropolitanAreaType.Standard },
                    { 25, Guid.NewGuid().ToString(), "GRANDE FLORIANÓPOLIS", Constant.MetropolitanAreaType.Standard },
                    { 26, Guid.NewGuid().ToString(), "GRANDE JOÃO PESSOA", Constant.MetropolitanAreaType.Standard },
                    { 27, Guid.NewGuid().ToString(), "GRANDE MACEIÓ", Constant.MetropolitanAreaType.Standard },
                    { 28, Guid.NewGuid().ToString(), "GRANDE SÃO LUÍS", Constant.MetropolitanAreaType.Standard },
                    { 29, Guid.NewGuid().ToString(), "GRANDE BELÉM", Constant.MetropolitanAreaType.Standard },
                    { 30, Guid.NewGuid().ToString(), "GRANDE RECIFE", Constant.MetropolitanAreaType.Standard },
                    { 31, Guid.NewGuid().ToString(), "VALE DO RIO CUIABÁ", Constant.MetropolitanAreaType.Standard }
                }
            );
        }
    }
}
