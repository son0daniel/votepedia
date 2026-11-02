using elections.Commons.Constants;
using Microsoft.EntityFrameworkCore.Migrations;

namespace elections.Migrations.Seeder
{
    public class PresidentialElection2ndRound2026Seeder : ISeeder
    {
        #region Biographies
        private const string FlavioBolsonaroBiography = """
            Flávio Nantes Bolsonaro é um advogado, empresário e político brasileiro, nascido em Resende (RJ) em 30 de abril de 1981, sendo o filho primogênito do ex-presidente Jair Bolsonaro. Formado em Direito pela Universidade Cândido Mendes, possui especializações em Políticas Públicas e Empreendedorismo. Ingressou na vida pública precoce aos 21 anos, elegendo-se como o deputado estatual mais jovem da Assembleia Legislativa do Rio de Janeiro (Alerj) em 2002. No parlamento fluminense, exerceu quatro mandatos consecutivos entre 2003 e 2019, período no qual se alinhou à pauta conservadora e de endurecimento da segurança pública.
            Em 2018, foi eleito senador da República pelo Rio de Janeiro com mais de 4 milhões de votos, integrando as 56ª e 57ª legislaturas no Senado Federal pelo Partido Liberal (PL). Durante seu mandato, enfrentou questionamentos jurídicos decorrentes de relatórios financeiros sobre movimentações de seu ex-assessor Fabrício Queiroz na Alerj. No entanto, após o Superior Tribunal de Justiça (STJ) e o Supremo Tribunal Federal (STF) invalidarem os relatórios e as quebras de sigilo por falhas processuais e falta de fundamentação legal, o Tribunal de Justiça do Rio de Janeiro (TJ-RJ) rejeitou a denúncia e arquivou formalmente o caso em maio de 2022 a pedido do próprio Ministério Público. Mais tarde, em 2026, com o apoio de seu pai, concorreu à Presidência da República pelo PL, sendo derrotado em segundo turno pelo atual mandatário Luiz Inácio Lula da Silva.
            """;

        public const string AlfredoGasparBiography = """
            Alfredo Gaspar de Mendonça Neto nasceu em Maceió, Alagoas, em 13 de agosto de 1970. Bacharel em Direito com pós-graduação em Direito Público, exerceu a função de promotor de Justiça no Ministério Público de Alagoas (MP-AL) por 24 anos. No âmbito institucional, atuou como coordenador do Grupo Estadual de Combate às Organizações Criminosas (Gecoc) e ocupou, em duas ocasiões, o cargo de Procurador-Geral de Justiça de Alagoas. Paralelamente à carreira jurídica, assumiu a chefia da Secretaria de Segurança Pública do estado de Alagoas.
            Sua entrada na política partidária ocorreu em 2020 ao concorrer à prefeitura de Maceió pelo MDB, disputa na qual foi derrotado no segundo turno. Em 2022, filiou-se ao União Brasil e elegeu-se deputado federal. No Congresso Nacional, exerceu a função de relator da CPMI do INSS, cujo parecer final acabou rejeitado pela maioria dos integrantes da comissão por 19 votos a 12. Em março de 2026, transferiu-se para o Partido Liberal (PL). Meses depois, foi indicado como candidato a vice-presidente na chapa presidencial liderada pelo senador Flávio Bolsonaro, terminando o pleito de outubro de 2026 derrotado em segundo turno pela chapa de Luiz Inácio Lula da Silva.
            """;

        public const string LulaNewBiography = """
            Luiz Inácio Lula da Silva é um ex-metalúrgico, líder sindical e político brasileiro, nascido em Garanhuns (PE) em 27 de outubro de 1945. Migrou ainda na infância com sua família para o estado de São Paulo, onde se formou como torneiro mecânico pelo SENAI e iniciou sua trajetória profissional nas indústrias automotivas do ABC paulista. Na década de 1970, despontou nacionalmente como presidente do Sindicato dos Metalúrgicos de São Bernardo do Campo e Diadema, liderando históricas greves operárias operadas durante o regime militar. Em 1980, participou ativamente da fundação do Partido dos Trabalhadores (PT), legenda pela qual exerceu o mandato de deputado federal constituinte em 1986 e disputou suas primeiras eleições presidenciais.
            Após concorrer à Presidência da República em pleitos anteriores, foi eleito chefe do Executivo Federal pela primeira vez em 2002 e reeleito em 2006, períodos caracterizados pela expansão de programas sociais de transferência de renda e crescimento econômico. Anos após deixar o cargo com elevados índices de aprovação, enfrentou processos judiciais no âmbito da Operação Lava Jato, cumprindo 580 dias de prisão entre 2018 e 2019 em razão de condenação confirmada em segunda instância. Contudo, após o Supremo Tribunal Federal (STF) mudar o entendimento sobre a prisão antes do trânsito em julgado e, posteriormente, invalidar as ações por incompetência de foro e suspeição do juízo, ele recuperou seus plenos direitos políticos. Retornou ao Palácio do Planalto ao vencer o pleito de 2022 e, em outubro de 2026, consolidou-se como o primeiro político brasileiro eleito para quatro mandatos presidenciais diretos ao derrotar o senador Flávio Bolsonaro no segundo turno.
            """;

        private const string AlckminNewBiography = """
            Geraldo José Rodrigues Alckmin Filho é um médico, professor universitário e político brasileiro, nascido em Pindamonhangaba (SP) em 7 de novembro de 1952. Formado em Medicina pela Universidade de Taubaté com especialização em anestesiologia, iniciou sua trajetória pública precocemente pelo MDB, elegendo-se vereador aos 19 anos e, posteriormente, tornando-se prefeito de sua cidade natal. Ao longo de sua carreira parlamentar, foi deputado estadual e deputado federal, participando ativamente da Assembleia Constituinte de 1988. Naquele mesmo ano, consolidou-se como um dos fundadores do Partido da Social Democracia Brasileira (PSDB), legenda pela qual governou o estado de São Paulo por quatro mandatos — tornando-se o político a passar mais tempo no comando do Executivo paulista — e disputou a Presidência da República em 2006 e 2018.
            Após desfiliar-se do PSDB em 2021, Alckmin ingressou no Partido Socialista Brasileiro (PSB) para compor uma coalizão ampla de centro-esquerda como candidato a vice-presidente na chapa de Luiz Inácio Lula da Silva. Eleito em 2022, assumiu cumulativamente a Vice-Presidência e o Ministério do Desenvolvimento, Indústria, Comércio e Serviços, atuando como um importante interlocutor junto aos setores empresariais e ao mercado financeiro. No âmbito jurídico, enfrentou questionamentos decorrentes de delações premiadas sobre recursos de campanha; contudo, as ações penais e de improbidade administrativa foram formalmente trancadas pelo Supremo Tribunal Federal em 2022 e 2024, uma vez que as Cortes Superiores invalidaram os elementos de prova coletados. Em outubro de 2026, foi reeleito vice-presidente da República após a vitória da chapa no segundo turno contra o senador Flávio Bolsonaro.
            """;
        #endregion

        public void Seed(MigrationBuilder migrationBuilder)
        {
            #region Candidates
            migrationBuilder.InsertData(table: "candidate",
                columns: ["id", "uid", "name", "display_name", "tse_name", "is_deceased", "birth_date", "death_date", "biography"],
                values: new object?[,]
                {
                    { 20, Guid.NewGuid().ToString(), "Flávio Nantes Bolsonaro", "Flávio Bolsonaro", "FLÁVIO NANTES BOLSONARO", false, new DateOnly(1981, 4, 30), null, FlavioBolsonaroBiography },
                    { 21, Guid.NewGuid().ToString(), "Alfredo Gaspar de Mendonça Neto", "Alfredo Gaspar", "ALFREDO GASPAR DE MENDONÇA NETO", false, new DateOnly(1970, 8, 13), null, AlfredoGasparBiography }
                });

            migrationBuilder.UpdateData(
                table: "candidate",
                keyColumn: "id",
                keyValue: 3,
                column: "biography",
                value: LulaNewBiography);

            migrationBuilder.UpdateData(
                table: "candidate",
                keyColumn: "id",
                keyValue: 8,
                column: "biography",
                value: AlckminNewBiography);
            #endregion

            #region CandidateParties
            migrationBuilder.InsertData(
                table: "candidate_party",
                columns: ["id", "candidate_id", "party_id"],
                values: new object[,]
                {
                    { 23, 20, 13 }, // Flávio Bolsonaro (PL)
                    { 24, 21, 13 } // Alfredo Gaspar (PL)
                });
            #endregion

            #region Elections
            migrationBuilder.InsertData(
                table: "election",
                columns: ["id", "uid", "year", "role", "type"],
                values: new object[,]
                {
                    { 10, Guid.NewGuid().ToString(), 2026, Constant.Election.Role.President, Constant.Election.Type.Ordinary }
                });

            migrationBuilder.InsertData(
                table: "election_round",
                columns: ["id", "uid", "election_id", "nr_round", "held_at"],
                values: new object[,]
                {
                    { 17, Guid.NewGuid().ToString(), 10, 2, new DateOnly(2026, 10, 25) }, // 2º turno da Eleição Presidencial de 2026
                });
            #endregion

            #region Tickets
            migrationBuilder.InsertData(
                table: "ticket",
                columns: ["id", "uid", "color", "description"],
                values: new object[,]
                {
                    { 15, Guid.NewGuid().ToString(), Constant.Color.Red, "Chapa presidencial — Lula (PT) 2026" },
                    { 16, Guid.NewGuid().ToString(), Constant.Color.DarkBlue, "Chapa presidencial — Flávio Bolsonaro (PL) 2026" }
                });
            #endregion

            #region Coalitions
            migrationBuilder.InsertData(
                table: "coalition",
                columns: ["id", "uid", "name", "ticket_id"],
                values:
                [
                    15, Guid.NewGuid().ToString(), "Brasil Pronto pra Mais", 15
                ]);
            #endregion

            #region CoalitionMembers
            migrationBuilder.InsertData(
                table: "coalition_member",
                columns: ["id", "coalition_id", "party_id", "federation_id"],
                values: new object?[,]
                {
                    { 69, 15, null, 1 }, // Coligação Brasil Pronto pra Mais — federação Brasil da Esperança
                    { 70, 15, null, 2 }, // Coligação Brasil Pronto pra Mais — federação PSOL REDE
                    { 71, 15, 3, null }, // Coligação Brasil da Esperança — partido PSB
                    { 72, 15, 19, null }, // Coligação Brasil da Esperança — partido PDT
                });
            #endregion

            #region TicketCandidateParties
            migrationBuilder.InsertData(
                table: "ticket_candidate_party",
                columns: ["id", "ticket_id", "candidate_party_id", "nr_level"],
                values: new object[,]
                {
                    { 29, 15, 3, 1 }, // Chapa presidencial com Lula (PT) titular
                    { 30, 15, 20, 2 }, // Chapa presidencial com Alckmin (PSB) vice
                    { 31, 16, 23, 1 }, // Chapa presidencial com Flávio Bolsonaro (PL) titular
                    { 32, 16, 24, 2 } // Chapa presidencial com Alfredo Gaspar (PL) vice
                });
            #endregion

            #region TicketElectionRounds
            migrationBuilder.InsertData(
                table: "ticket_election_round",
                columns: ["id", "election_round_id", "ticket_id"],
                values: new object[,]
                {
                    { 29, 17, 15 },
                    { 30, 17, 16 }
                });
            #endregion
        }
    }
}
