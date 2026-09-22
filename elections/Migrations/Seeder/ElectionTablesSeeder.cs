using elections.Constants;
using Microsoft.EntityFrameworkCore.Migrations;

namespace elections.Migrations.Seeder
{
    public class ElectionTablesSeeder : ISeeder
    {
        public void Seed(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "election",
                columns: ["id", "uid", "year", "role", "type"],
                values: new object[,]
                {
                    { 1, Guid.NewGuid().ToString(), 1989, Constant.Election.Role.President, Constant.Election.Type.Ordinary },
                    { 2, Guid.NewGuid().ToString(), 1994, Constant.Election.Role.President, Constant.Election.Type.Ordinary },
                    { 3, Guid.NewGuid().ToString(), 1998, Constant.Election.Role.President, Constant.Election.Type.Ordinary },
                    { 4, Guid.NewGuid().ToString(), 2002, Constant.Election.Role.President, Constant.Election.Type.Ordinary },
                    { 5, Guid.NewGuid().ToString(), 2006, Constant.Election.Role.President, Constant.Election.Type.Ordinary },
                    { 6, Guid.NewGuid().ToString(), 2010, Constant.Election.Role.President, Constant.Election.Type.Ordinary },
                    { 7, Guid.NewGuid().ToString(), 2014, Constant.Election.Role.President, Constant.Election.Type.Ordinary },
                    { 8, Guid.NewGuid().ToString(), 2018, Constant.Election.Role.President, Constant.Election.Type.Ordinary },
                    { 9, Guid.NewGuid().ToString(), 2022, Constant.Election.Role.President, Constant.Election.Type.Ordinary }
                });

            migrationBuilder.InsertData(
                table: "election_round",
                columns: ["id", "uid", "election_id", "nr_round", "held_at"],
                values: new object[,]
                {
                    { 1, Guid.NewGuid().ToString(), 1, 1, new DateOnly(1989, 11, 15) }, // 1º turno da Eleição Presidencial de 1989
                    { 2, Guid.NewGuid().ToString(), 1, 2, new DateOnly(1989, 12, 17) }, // 2º turno da Eleição Presidencial de 1989
                    { 3, Guid.NewGuid().ToString(), 2, 1, new DateOnly(1994, 10, 3) }, // 1º turno da Eleição Presidencial de 1994
                    { 4, Guid.NewGuid().ToString(), 3, 1, new DateOnly(1998, 10, 4) }, // 1º turno da Eleição Presidencial de 1998
                    { 5, Guid.NewGuid().ToString(), 4, 1, new DateOnly(2002, 10, 6) }, // 1º turno da Eleição Presidencial de 2002
                    { 6, Guid.NewGuid().ToString(), 4, 2, new DateOnly(2002, 10, 27) }, // 2º turno da Eleição Presidencial de 2002
                    { 7, Guid.NewGuid().ToString(), 5, 1, new DateOnly(2006, 10, 1) }, // 1º turno da Eleição Presidencial de 2006
                    { 8, Guid.NewGuid().ToString(), 5, 2, new DateOnly(2006, 10, 29) }, // 2º turno da Eleição Presidencial de 2006
                    { 9, Guid.NewGuid().ToString(), 6, 1, new DateOnly(2010, 10, 3) }, // 1º turno da Eleição Presidencial de 2010
                    { 10, Guid.NewGuid().ToString(), 6, 2, new DateOnly(2010, 10, 31) }, // 2º turno da Eleição Presidencial de 2010
                    { 11, Guid.NewGuid().ToString(), 7, 1, new DateOnly(2014, 10, 5) }, // 1º turno da Eleição Presidencial de 2014
                    { 12, Guid.NewGuid().ToString(), 7, 2, new DateOnly(2014, 10, 26) }, // 2º turno da Eleição Presidencial de 2014
                    { 13, Guid.NewGuid().ToString(), 8, 1, new DateOnly(2018, 10, 7) }, // 1º turno da Eleição Presidencial de 2018
                    { 14, Guid.NewGuid().ToString(), 8, 2, new DateOnly(2018, 10, 28) }, // 2º turno da Eleição Presidencial de 2018
                    { 15, Guid.NewGuid().ToString(), 9, 1, new DateOnly(2022, 10, 2) }, // 1º turno da Eleição Presidencial de 2022
                    { 16, Guid.NewGuid().ToString(), 9, 2, new DateOnly(2022, 10, 30) }, // 2º turno da Eleição Presidencial de 2022
                });
        }
    }
}
