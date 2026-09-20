using Microsoft.EntityFrameworkCore.Migrations;

namespace elections.Migrations.Seeder
{
    public class StateMacroregionUpperCaseNormalizationSeeder : ISeeder
    {
        public void Seed(MigrationBuilder migrationBuilder)
        {
            #region Macroregions

            migrationBuilder.Sql(
                """
                    UPDATE macroregion
                    SET name = CASE id
                        WHEN 1 THEN "CENTRO-OESTE"
                        WHEN 2 THEN "NORDESTE"
                        WHEN 3 THEN "NORTE"
                        WHEN 4 THEN "SUDESTE"
                        WHEN 5 THEN "SUL"
                        WHEN 6 THEN "EXTERIOR"
                    END
                    WHERE id BETWEEN 1 AND 6;
                """    
            );

            #endregion

            #region States

            migrationBuilder.Sql(
                """
                    UPDATE state
                    SET name = CASE id
                        WHEN 1 THEN "ACRE"
                        WHEN 2 THEN "ALAGOAS"
                        WHEN 3 THEN "AMAPÁ"
                        WHEN 4 THEN "AMAZONAS"
                        WHEN 5 THEN "BAHIA"
                        WHEN 6 THEN "CEARÁ"
                        WHEN 7 THEN "DISTRITO FEDERAL"
                        WHEN 8 THEN "ESPÍRITO SANTO"
                        WHEN 9 THEN "GOIÁS"
                        WHEN 10 THEN "MARANHÃO"
                        WHEN 11 THEN "MATO GROSSO"
                        WHEN 12 THEN "MATO GROSSO DO SUL"
                        WHEN 13 THEN "MINAS GERAIS"
                        WHEN 14 THEN "PARÁ"
                        WHEN 15 THEN "PARAÍBA"
                        WHEN 16 THEN "PARANÁ"
                        WHEN 17 THEN "PERNAMBUCO"
                        WHEN 18 THEN "PIAUÍ"
                        WHEN 19 THEN "RIO DE JANEIRO"
                        WHEN 20 THEN "RIO GRANDE DO NORTE"
                        WHEN 21 THEN "RIO GRANDE DO SUL"
                        WHEN 22 THEN "RONDÔNIA"
                        WHEN 23 THEN "RORAIMA"
                        WHEN 24 THEN "SANTA CATARINA"
                        WHEN 25 THEN "SÃO PAULO"
                        WHEN 26 THEN "SERGIPE"
                        WHEN 27 THEN "TOCANTINS"
                        WHEN 28 THEN "EXTERIOR"
                    END
                    WHERE id BETWEEN 1 AND 28;
                """
            );

            #endregion
        }
    }
}
