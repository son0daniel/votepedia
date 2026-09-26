using elections.Constants;
using Microsoft.EntityFrameworkCore.Migrations;

namespace elections.Migrations.Seeder
{
    public class TicketTablesSeeder : ISeeder
    {
        public void Seed(MigrationBuilder migrationBuilder)
        {              
            #region Parties
            // Partidos que fizeram parte de coligações dos segundos turnos das eleições presidenciais de 1989 a 2022
            migrationBuilder.InsertData(
                table: "party",
                columns: ["id", "uid", "name", "abbr", "nr_voteable", "is_active", "founded_at", "registered_at", "dissolved_at"],
                values: new object?[,]
                {
                    { 14, Guid.NewGuid().ToString(), "Partido Social Cristão", "PSC", 20, false, new DateOnly(1985, 5, 15), new DateOnly(1990, 3, 29), new DateOnly(2023, 6, 15) },
                    { 15, Guid.NewGuid().ToString(), "Partido Trabalhista Renovador", "PTR", 35, false, null, new DateOnly(1990, 3, 22), new DateOnly(1993, 2, 2) },
                    { 16, Guid.NewGuid().ToString(), "Partido Social Trabalhista", "PST", 52, false, null, new DateOnly(1990, 6, 12), new DateOnly(2003, 4, 1) },
                    { 17, Guid.NewGuid().ToString(), "Partido da Mobilização Nacional", "PMN", 33, false, new DateOnly(1984, 4, 21), new DateOnly(1990, 10, 25), new DateOnly(2023, 12, 5) },
                    { 18, Guid.NewGuid().ToString(), "Partido Comunista Brasileiro", "PCB", 21, true, new DateOnly(1922, 3, 25), new DateOnly(1996, 5, 9), null },
                    { 19, Guid.NewGuid().ToString(), "Partido Democrático Trabalhista", "PDT", 12, true, new DateOnly(1979, 6, 17), new DateOnly(1981, 11, 10), null },
                    { 20, Guid.NewGuid().ToString(), "Partido da República", "PR", 22, false, null, new DateOnly(2006, 12, 19), new DateOnly(2019, 2, 9) },
                    { 21, Guid.NewGuid().ToString(), "Partido Trabalhista Cristão", "PTC", 36, false, null, new DateOnly(2001, 4, 24), new DateOnly(2022, 3, 31) },
                    { 22, Guid.NewGuid().ToString(), "Partido Trabalhista Nacional", "PTN", 19, false, null, new DateOnly(1997, 10, 2), new DateOnly(2017, 5, 16) },
                    { 23, Guid.NewGuid().ToString(), "Partido Popular Socialista", "PPS", 23, false, new DateOnly(1992, 1, 26), new DateOnly(1992, 3, 19), new DateOnly(2019, 9, 19) },
                    { 24, Guid.NewGuid().ToString(), "Partido Trabalhista do Brasil", "PTdoB", 70, false, new DateOnly(1989, 5, 15), new DateOnly(1994, 10, 11), new DateOnly(2017, 9, 12) },
                    { 25, Guid.NewGuid().ToString(), "Partido Trabalhista Brasileiro", "PTB", 14, false, new DateOnly(1979, 11, 21), new DateOnly(1981, 11, 3), new DateOnly(2023, 11, 9) },
                    { 26, Guid.NewGuid().ToString(), "Partido Social Democrático", "PSD", 55, true, new DateOnly(2011, 2, 25), new DateOnly(2011, 9, 27), null },
                    { 27, Guid.NewGuid().ToString(), "Partido Progressista", "PP", 11, false, new DateOnly(1995, 4, 14), new DateOnly(2003, 5, 29), new DateOnly(2018, 9, 11) },
                    { 28, Guid.NewGuid().ToString(), "Partido Republicano da Ordem Social", "PROS", 90, false, new DateOnly(2010, 1, 4), new DateOnly(2013, 9, 24), new DateOnly(2023, 2, 14) },
                    { 29, Guid.NewGuid().ToString(), "Solidariedade", "SOLIDARIEDADE", 77, true, new DateOnly(2012, 10, 25), new DateOnly(2013, 9, 24), null },
                    { 30, Guid.NewGuid().ToString(), "Partido Ecológico Nacional", "PEN", 51, false, new DateOnly(2011, 8, 9), new DateOnly(2012, 7, 19), new DateOnly(2018, 4, 26) },
                    { 31, Guid.NewGuid().ToString(), "Partido Verde", "PV", 43, true, new DateOnly(1986, 1, 17), new DateOnly(1993, 9, 30), null },
                    { 32, Guid.NewGuid().ToString(), "Partido Socialismo e Liberdade", "PSOL", 50, true, new DateOnly(2004, 6, 6), new DateOnly(2005, 9, 15), null },
                    { 33, Guid.NewGuid().ToString(), "Rede Sustentabilidade", "REDE", 18, true, new DateOnly(2013, 2, 16), new DateOnly(2015, 9, 22), null },
                    { 34, Guid.NewGuid().ToString(), "Avante", "AVANTE", 70, true, new DateOnly(1989, 5, 15), new DateOnly(2017, 9, 12), null },
                    { 35, Guid.NewGuid().ToString(), "Agir", "AGIR", 36, true, null, new DateOnly(2022, 3, 31), null },
                    { 36, Guid.NewGuid().ToString(), "Republicanos", "REPUBLICANOS", 10, true, new DateOnly(2003, 10, 6), new DateOnly(2019, 8, 15), null },
                    { 37, Guid.NewGuid().ToString(), "Progressistas", "PROGRESSISTAS", 11, true, new DateOnly(1995, 4, 14), new DateOnly(2018, 9, 11), null }
                });
            #endregion

            #region Federations
            migrationBuilder.InsertData(
                table: "federation",
                columns: ["id", "uid", "name", "display_name", "registered_at"],
                values: new object?[,]
                {
                    { 1, Guid.NewGuid().ToString(), "Brasil da Esperança", "FE Brasil", new DateOnly(2022, 5, 24) },
                    { 2, Guid.NewGuid().ToString(), "PSOL REDE", null, new DateOnly(2022, 5, 26) }
                });
            #endregion

            #region FederationParties
            migrationBuilder.InsertData(
                table: "federation_party",
                columns: ["id", "federation_id", "party_id"],
                values: new object?[,]
                {
                    { 1, 1, 2 }, // PT (FE Brasil)
                    { 2, 1, 12 }, // PCdoB (FE Brasil)
                    { 3, 1, 31 }, // PV (FE Brasil)
                    { 4, 2, 32 }, // PSOL (PSOL REDE)
                    { 5, 2, 33 } // REDE (PSOL REDE)
                });
            #endregion

            #region Tickets
            migrationBuilder.InsertData(
                table: "ticket",
                columns: ["id", "uid", "color", "description"],
                values: new object[,]
                {
                    { 1, Guid.NewGuid().ToString(), Constant.Color.Green, "Chapa presidencial — Collor (PRN) 1989" },
                    { 2, Guid.NewGuid().ToString(), Constant.Color.Red, "Chapa presidencial — Lula (PT) 1989" },
                    { 3, Guid.NewGuid().ToString(), Constant.Color.Red, "Chapa presidencial — Lula (PT) 2002" },
                    { 4, Guid.NewGuid().ToString(), Constant.Color.Blue, "Chapa presidencial — José Serra (PSDB) 2002" },
                    { 5, Guid.NewGuid().ToString(), Constant.Color.Red, "Chapa presidencial — Lula (PT) 2006" },
                    { 6, Guid.NewGuid().ToString(), Constant.Color.Blue, "Chapa presidencial — Alckmin (PSDB) 2006" },
                    { 7, Guid.NewGuid().ToString(), Constant.Color.Red, "Chapa presidencial — Dilma (PT) 2010" },
                    { 8, Guid.NewGuid().ToString(), Constant.Color.Blue, "Chapa presidencial — José Serra (PSDB) 2010" },
                    { 9, Guid.NewGuid().ToString(), Constant.Color.Red, "Chapa presidencial — Dilma (PT) 2014" },
                    { 10, Guid.NewGuid().ToString(), Constant.Color.Blue, "Chapa presidencial — Aécio Neves (PSDB) 2014" },
                    { 11, Guid.NewGuid().ToString(), Constant.Color.DarkBlue, "Chapa presidencial — Jair Bolsonaro (PSL) 2018" },
                    { 12, Guid.NewGuid().ToString(), Constant.Color.Red, "Chapa presidencial — Fernando Haddad (PT) 2018" },
                    { 13, Guid.NewGuid().ToString(), Constant.Color.Red, "Chapa presidencial — Lula (PT) 2022" },
                    { 14, Guid.NewGuid().ToString(), Constant.Color.DarkBlue, "Chapa presidencial — Jair Bolsonaro (PL) 2022" }
                });
            #endregion

            #region Coalitions
            migrationBuilder.InsertData(
                table: "coalition",
                columns: ["id", "uid", "name", "ticket_id"],
                values: new object?[,]
                {
                    { 1, Guid.NewGuid().ToString(), "Brasil Novo", 1 },
                    { 2, Guid.NewGuid().ToString(), "Frente Brasil Popular", 2 },
                    { 3, Guid.NewGuid().ToString(), "Lula Presidente", 3 },
                    { 4, Guid.NewGuid().ToString(), "Grande Aliança", 4 },
                    { 5, Guid.NewGuid().ToString(), "A Força do Povo", 5 },
                    { 6, Guid.NewGuid().ToString(), "Por um Brasil Decente", 6 },
                    { 7, Guid.NewGuid().ToString(), "Para o Brasil Seguir Mudando", 7 },
                    { 8, Guid.NewGuid().ToString(), "O Brasil Pode Mais", 8 },
                    { 9, Guid.NewGuid().ToString(), "Com a Força do Povo", 9 },
                    { 10, Guid.NewGuid().ToString(), "Muda Brasil", 10 },
                    { 11, Guid.NewGuid().ToString(), "Brasil acima de Tudo, Deus acima de Todos", 11 },
                    { 12, Guid.NewGuid().ToString(), "O Povo Feliz de Novo", 12 },
                    { 13, Guid.NewGuid().ToString(), "Brasil da Esperança", 13 },
                    { 14, Guid.NewGuid().ToString(), "Pelo Bem do Brasil", 14 },
                });
            #endregion

            #region CoalitionMembers
            migrationBuilder.InsertData(
                table: "coalition_member",
                columns: ["id", "coalition_id", "party_id", "federation_id"],
                values: new object?[,]
                {
                    { 1, 1, 1, null }, // Coligação Brasil Novo — partido PRN
                    { 2, 1, 14, null }, // Coligação Brasil Novo — partido PSC
                    { 3, 1, 15, null }, // Coligação Brasil Novo — partido PTR
                    { 4, 1, 16, null }, // Coligação Brasil Novo — partido PST
                    { 5, 2, 2, null }, // Coligação Frente Brasil Popular — partido PT
                    { 6, 2, 3, null }, // Coligação Frente Brasil Popular — partido PSB
                    { 7, 2, 12, null }, // Coligação Frente Brasil Popular — partido PCdoB
                    { 8, 3, 2, null }, // Coligação Lula Presidente — partido PT
                    { 9, 3, 4, null }, // Coligação Lula Presidente — partido PL
                    { 10, 3, 12, null }, // Coligação Lula Presidente — partido PCdoB
                    { 11, 3, 17, null }, // Coligação Lula Presidente — partido PMN
                    { 12, 3, 18, null }, // Coligação Lula Presidente — partido PCB
                    { 13, 4, 5, null }, // Coligação Grande Aliança — partido PSDB
                    { 14, 4, 6, null }, // Coligação Grande Aliança — partido PMDB
                    { 15, 5, 2, null }, // Coligação A Força do Povo — partido PT
                    { 16, 5, 7, null }, // Coligação A Força do Povo — partido PRB
                    { 17, 5, 12, null }, // Coligação A Força do Povo — partido PCdoB
                    { 18, 6, 5, null }, // Coligação Por um Brasil Decente — partido PSDB
                    { 19, 6, 8, null }, // Coligação Por um Brasil Decente — partido PFL
                    { 20, 7, 2, null }, // Coligação Para o Brasil Seguir Mudando — partido PT
                    { 21, 7, 6, null }, // Coligação Para o Brasil Seguir Mudando — partido PMDB
                    { 22, 7, 19, null }, // Coligação Para o Brasil Seguir Mudando — partido PDT
                    { 23, 7, 12, null }, // Coligação Para o Brasil Seguir Mudando — partido PCdoB
                    { 24, 7, 3, null }, // Coligação Para o Brasil Seguir Mudando — partido PSB
                    { 25, 7, 20, null }, // Coligação Para o Brasil Seguir Mudando — partido PR
                    { 26, 7, 7, null }, // Coligação Para o Brasil Seguir Mudando — partido PRB
                    { 27, 7, 14, null }, // Coligação Para o Brasil Seguir Mudando — partido PSC
                    { 28, 7, 21, null }, // Coligação Para o Brasil Seguir Mudando — partido PTC
                    { 29, 7, 22, null }, // Coligação Para o Brasil Seguir Mudando — partido PTN
                    { 30, 8, 5, null }, // Coligação O Brasil Pode Mais — partido PSDB
                    { 31, 8, 9, null }, // Coligação O Brasil Pode Mais — partido DEM
                    { 32, 8, 23, null }, // Coligação O Brasil Pode Mais — partido PPS
                    { 33, 8, 17, null }, // Coligação O Brasil Pode Mais — partido PMN
                    { 34, 8, 24, null }, // Coligação O Brasil Pode Mais — partido PTdoB
                    { 35, 8, 25, null }, // Coligação O Brasil Pode Mais — partido PTB
                    { 36, 9, 2, null }, // Coligação Com a Força do Povo — partido PT
                    { 37, 9, 6, null }, // Coligação Com a Força do Povo — partido PMDB
                    { 38, 9, 26, null }, // Coligação Com a Força do Povo — partido PSD
                    { 39, 9, 27, null }, // Coligação Com a Força do Povo — partido PP
                    { 40, 9, 20, null }, // Coligação Com a Força do Povo — partido PR
                    { 41, 9, 19, null }, // Coligação Com a Força do Povo — partido PDT
                    { 42, 9, 7, null }, // Coligação Com a Força do Povo — partido PRB
                    { 43, 9, 28, null }, // Coligação Com a Força do Povo — partido PROS
                    { 44, 9, 12, null }, // Coligação Com a Força do Povo — partido PCdoB
                    { 45, 10, 5, null }, // Coligação Muda Brasil — partido PSDB
                    { 46, 10, 29, null }, // Coligação Muda Brasil — partido SOLIDARIEDADE
                    { 47, 10, 17, null }, // Coligação Muda Brasil — partido PMN
                    { 48, 10, 30, null }, // Coligação Muda Brasil — partido PEN
                    { 49, 10, 22, null }, // Coligação Muda Brasil — partido PTN
                    { 50, 10, 21, null }, // Coligação Muda Brasil — partido PTC
                    { 51, 10, 9, null }, // Coligação Muda Brasil — partido DEM
                    { 52, 10, 24, null }, // Coligação Muda Brasil — partido PTdoB
                    { 53, 10, 25, null }, // Coligação Muda Brasil — partido PTB
                    { 54, 11, 10, null }, // Coligação Brasil acima de Tudo, Deus acima de Todos — partido PSL
                    { 55, 11, 11, null }, // Coligação Brasil acima de Tudo, Deus acima de Todos — partido PRTB
                    { 56, 12, 2, null }, // Coligação O Povo Feliz de Novo — partido PT
                    { 57, 12, 12, null }, // Coligação O Povo Feliz de Novo — partido PCdoB
                    { 58, 12, 28, null }, // Coligação O Povo Feliz de Novo — partido PROS
                    { 59, 13, null, 1 }, // Coligação Brasil da Esperança — federação Brasil da Esperança
                    { 60, 13, null, 2 }, // Coligação Brasil da Esperança — federação PSOL REDE
                    { 61, 13, 3, null }, // Coligação Brasil da Esperança — partido PSB
                    { 62, 13, 29, null }, // Coligação Brasil da Esperança — partido SOLIDARIEDADE
                    { 63, 13, 34, null }, // Coligação Brasil da Esperança — partido AVANTE
                    { 64, 13, 35, null }, // Coligação Brasil da Esperança — partido AGIR
                    { 65, 13, 28, null }, // Coligação Brasil da Esperança — partido PROS
                    { 66, 14, 13, null }, // Coligação Pelo Bem do Brasil — partido PL
                    { 67, 14, 36, null }, // Coligação Pelo Bem do Brasil — partido REPUBLICANOS
                    { 68, 14, 37, null }, // Coligação Pelo Bem do Brasil — partido PROGRESSISTAS
                });
            #endregion

            #region TicketCandidateParties
            migrationBuilder.InsertData(
                table: "ticket_candidate_party",
                columns: ["id", "ticket_id", "candidate_party_id", "nr_level"],
                values: new object[,]
                {
                    { 1, 1, 1, 1 }, // Chapa presidencial com Collor (PRN) titular
                    { 2, 1, 2, 2 }, // Chapa presidencial com Itamar Franco (PRN) vice
                    { 3, 2, 3, 1 }, // Chapa presidencial com Lula (PT) titular
                    { 4, 2, 4, 2 }, // Chapa presidencial com Bisol (PSB) vice
                    { 5, 3, 3, 1 }, // Chapa presidencial com Lula (PT) titular
                    { 6, 3, 5, 2 }, // Chapa presidencial com José Alencar (PL) vice
                    { 7, 4, 6, 1 }, // Chapa presidencial com José Serra (PSDB) titular
                    { 8, 4, 7, 2 }, // Chapa presidencial com Rita Camata (PMDB) vice
                    { 9, 5, 3, 1 }, // Chapa presidencial com Lula (PT) titular
                    { 10, 5, 8, 2 }, // Chapa presidencial com José Alencar (PRB) vice
                    { 11, 6, 9, 1 }, // Chapa presidencial com Alckmin (PSDB) titular
                    { 12, 6, 10, 2 }, // Chapa presidencial com José Jorge (PFL) vice
                    { 13, 7, 11, 1 }, // Chapa presidencial com Dilma (PT) titular
                    { 14, 7, 12, 2 }, // Chapa presidencial com Temer (PMDB) vice
                    { 15, 8, 6, 1 }, // Chapa presidencial com José Serra (PSDB) titular
                    { 16, 8, 13, 2 }, // Chapa presidencial com Indio da Costa (DEM) vice
                    { 17, 9, 11, 1 }, // Chapa presidencial com Dilma (PT) titular
                    { 18, 9, 12, 2 }, // Chapa presidencial com Temer (PMDB) vice
                    { 19, 10, 14, 1 }, // Chapa presidencial com Aécio (PSDB) titular
                    { 20, 10, 15, 2 }, // Chapa presidencial com Aloysio Nunes (PSDB) vice
                    { 21, 11, 16, 1 }, // Chapa presidencial com Jair Bolsonaro (PSL) titular
                    { 22, 11, 17, 2 }, // Chapa presidencial com Hamilton Mourão (PRTB) vice
                    { 23, 12, 18, 1 }, // Chapa presidencial com Haddad (PT) titular
                    { 24, 12, 19, 2 }, // Chapa presidencial com Manuela d'Ávila (PCdoB) vice
                    { 25, 13, 3, 1 }, // Chapa presidencial com Lula (PT) titular
                    { 26, 13, 20, 2 }, // Chapa presidencial com Alckmin (PSB) vice
                    { 27, 14, 21, 1 }, // Chapa presidencial com Jair Bolsonaro (PL) titular
                    { 28, 14, 22, 2 }, // Chapa presidencial com Braga Netto (PL) vice
                });
            #endregion

            #region TicketElectionRounds
            migrationBuilder.InsertData(
                table: "ticket_election_round",
                columns: ["id", "election_round_id", "ticket_id"],
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 1 },
                    { 4, 2, 2 },
                    { 5, 5, 3 },
                    { 6, 5, 4 },
                    { 7, 6, 3 },
                    { 8, 6, 4 },
                    { 9, 7, 5 },
                    { 10, 7, 6 },
                    { 11, 8, 5 },
                    { 12, 8, 6 },
                    { 13, 9, 7 },
                    { 14, 9, 8 },
                    { 15, 10, 7 },
                    { 16, 10, 8 },
                    { 17, 11, 9 },
                    { 18, 11, 10 },
                    { 19, 12, 9 },
                    { 20, 12, 10 },
                    { 21, 13, 11 },
                    { 22, 13, 12 },
                    { 23, 14, 11 },
                    { 24, 14, 12 },
                    { 25, 15, 13 },
                    { 26, 15, 14 },
                    { 27, 16, 13 },
                    { 28, 16, 14 }
                });
            #endregion
        }
    }
}
