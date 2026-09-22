using Microsoft.EntityFrameworkCore.Migrations;

namespace elections.Migrations.Seeder
{
    public class CandidatesAndPartiesSeeder : ISeeder
    {
        #region Biographies
        private const string CollorBiography = """
            Fernando Collor de Mello é um político brasileiro que ganhou projeção nacional ao se eleger, em 1989, como o primeiro presidente da República escolhido pelo voto direto após a ditadura militar. Nascido em uma influente família de políticos e empresários de Alagoas, iniciou sua carreira no final dos anos 1970 e governou o estado de Alagoas antes de assumir a presidência com um discurso focado na modernização econômica e no combate à corrupção, adotando uma postura autointitulada de "caçador de marajás". Seu governo foi marcado pela abertura do mercado nacional às importações e pelo polêmico Plano Collor, que confiscou temporariamente as cadernetas de poupança dos brasileiros na tentativa de frear a hiperinflação.
            
            A sua gestão, no entanto, foi abreviada por graves denúncias de corrupção envolvendo o esquema de PC Farias, seu ex-tesoureiro de campanha. Diante do descontentamento popular e das intensas manifestações conhecidas como o movimento dos "Caras-Pintadas", a Câmara dos Deputados instaurou um processo de impeachment que levou Collor a renunciar à presidência em 29 de dezembro de 1992, poucas horas antes de ter seus direitos políticos cassados pelo Senado. Após o período de inelegibilidade, ele retornou à cena política em 2006, elegendo-se senador por Alagoas, cargo que ocupou por mais de uma década até deixar o Parlamento e, posteriormente, enfrentar novas condenações judiciais relacionadas aos seus mandatos recentes.
            """;

        private const string ItamarBiography = """
            Itamar Augusto Cautiero Franco (1929–2011) foi um engenheiro e político brasileiro que exerceu a presidência do Brasil entre 1992 e 1995, assumindo o cargo após o impeachment de Fernando Collor de Mello. Nascido a bordo de um navio no Oceano Atlântico e registrado em Salvador, ele construiu sua carreira política em Minas Gerais, onde foi prefeito de Juiz de Fora por dois mandatos e governador do estado. Conhecido por seu estilo centralizador, temperamento firme e nacionalista, Itamar teve um papel crucial na transição democrática e na pacificação política do país em um período de forte instabilidade.
            
            O maior marco de sua gestão na presidência foi o combate à hiperinflação com o lançamento do Plano Real em 1994, idealizado por sua equipe econômica liderada por Fernando Henrique Cardoso. A nova moeda estabilizou a economia nacional, recuperou o poder de compra dos brasileiros e permitiu a retomada do crescimento econômico. Além do sucesso econômico, seu governo promoveu a retomada da produção do Fusca e a realização do plebiscito sobre a forma e o sistema de governo do Brasil em 1993, consolidando Itamar Franco como um dos nomes mais marcantes da história política recente do país.
            """;

        private const string LulaBiography = """
            Luiz Inácio Lula da Silva nasceu em Garanhuns, Pernambuco, em 27 de outubro de 1945. Ele mudou-se criança com a família pobre para o estado de São Paulo em um caminhão pau de arara. Na juventude, trabalhou como metalúrgico e destacou-se como líder sindical no ABC Paulista durante as greves contra a ditadura militar. Ele também ajudou a fundar o Partido dos Trabalhadores (PT) na mesma época.
            
            Lula tentou a presidência várias vezes até vencer as eleições de 2002. Ele governou o país de 2003 a 2011, focando em programas de combate à fome e inclusão social. Após passar por um período de prisão entre 2018 e 2019, teve suas condenações anuladas pelo Supremo Tribunal Federal. Em 2022, venceu novamente a eleição presidencial e assumiu o terceiro mandato no comando do Brasil em 2023.
            """;

        private const string BisolBiography = """
            José Paulo Bisol (1928–2021) foi um destacado jurista, magistrado, escritor e político brasileiro. Formado em Direito e Comunicação Social, atuou como advogado, jornalista e professor antes de ingressar na magistratura do Rio Grande do Sul, onde atingiu o cargo de desembargador no Tribunal de Justiça do estado em 1978. Aposentou-se pouco tempo depois da magistratura para construir uma trajetória expressiva na vida pública gaúcha e nacional, sendo eleito deputado estadual em 1982.
            
            Em 1986, foi eleito senador pelo Rio Grande do Sul, exercendo papel protagonista como parlamentar na Assembleia Nacional Constituinte de 1988 e no combate à corrupção, inclusive participando ativamente da CPI dos "Anões do Orçamento". Filiado ao PSB, ganhou projeção nacional ao ser o candidato a vice-presidente na chapa de Luiz Inácio Lula da Silva na histórica eleição presidencial de 1989. Anos mais tarde, ocupou a Secretaria de Justiça e Segurança Pública do Rio Grande do Sul (1999–2002) durante o governo de Olívio Dutra, consolidando-se ao longo da vida como uma referência humanista e defensor dos direitos humanos até o seu falecimento, aos 92 anos.
            """;

        private const string JoseAlencarBiography = """
            José Alencar foi um empresário de sucesso e político brasileiro que atuou como o 23º vice-presidente do Brasil de 2003 a 2011, ao lado do presidente Luiz Inácio Lula da Silva. Nascido em Muriaé (MG) em 1931, ele construiu uma grande trajetória no setor têxtil como dono do grupo Coteminas e foi eleito senador por Minas Gerais em 1998. Como vice, sua figura foi fundamental para fazer a ponte entre Lula e o mercado financeiro e empresarial na eleição de 2002.
            
            Apesar da forte aliança, Alencar manteve uma postura independente e criticou publicamente a política de juros altos do governo. Ele também acumulou o cargo de ministro da Defesa entre 2004 e 2006 e foi reeleito vice-presidente em 2006. Conhecido por seu otimismo e bom humor, enfrentou bravamente um câncer por muitos anos até falecer em março de 2011, em São Paulo.
            """;

        private const string SerraBiography = """
            José Serra é um economista e político brasileiro com uma longa trajetória pública, tendo ocupado cargos de destaque no cenário nacional. Nascido em São Paulo em 1942, iniciou sua militância na juventude como presidente da União Nacional dos Estudantes (UNE), o que o levou ao exílio durante a ditadura militar. Ao retornar ao Brasil, formou-se em economia e ajudou a fundar o Partido da Social Democracia Brasileira (PSDB), legenda pela qual exerceu mandatos de deputado federal, senador, prefeito e governador de São Paulo, além de ter sido ministro do Planejamento e da Saúde no governo de Fernando Henrique Cardoso, e ministro das Relações Exteriores no governo de Michel Temer.
            
            Ao longo de sua carreira, Serra destacou-se pela implementação de políticas públicas de grande impacto social e econômico. Como ministro da Saúde, ganhou projeção internacional ao implementar o programa de medicamentos genéricos no Brasil e ao liderar o programa nacional de combate ao HIV/AIDS, enfrentando grandes indústrias farmacêuticas para quebrar patentes. Disputou a Presidência da República em duas ocasiões, em 2002 e 2010, chegando ao segundo turno em ambas, consolidando-se como uma das lideranças políticas mais influentes do país nas últimas décadas.
            """;

        private const string CamataBiography = """
            Rita de Cássia Paste Camata (Venda Nova do Imigrante, 1961) é uma jornalista e ex-política brasileira de forte projeção nacional entre os anos 1980 e 2000. Formada em Comunicação Social pela Universidade Federal do Espírito Santo (UFES), iniciou sua trajetória na vida pública ao lado do marido, Gerson Camata — ex-governador capixaba e senador —, atuando expressivamente na área social do estado. Eleita deputada federal constituinte em 1986 pelo PMDB, destacou-se por ter sido a mulher mais jovem e a candidata mais votada do Espírito Santo naquele pleito.

            Durante seus cinco mandatos consecutivos na Câmara dos Deputados (1987-2007), Rita notabilizou-se como relatora do Estatuto da Criança e do Adolescente (ECA) em 1990, marco que consolidou os direitos da infância no Brasil. Em 2002, viveu o ápice de sua visibilidade nacional ao ser candidata a vice-presidente da República na chapa encabeçada por José Serra (PSDB). Após desfiliar-se do PMDB e ter breves passagens pelo PSDB e pelo PSD, afastou-se dos cargos eletivos para se dedicar a consultorias em gestão pública, ações de responsabilidade social e iniciativas no setor privado.
            """;

        private const string AlckminBiography = """
            Geraldo José Rodrigues Alckmin Filho é um médico e político brasileiro, atual vice-presidente do Brasil e ministro do Desenvolvimento, Indústria, Comércio e Serviços. Nascido em Pindamonhangaba (SP) em 1952, formou-se em Medicina e iniciou sua carreira política na década de 1970 como vereador e prefeito de sua cidade natal. Ao longo de sua trajetória, consolidou-se como uma das principais lideranças do Partido da Social Democracia Brasileira (PSDB), legenda pela qual exerceu mandatos de deputado federal, vice-governador e, posteriormente, governou o estado de São Paulo por quatro mandatos, tornando-se o político que por mais tempo comandou o governo paulista desde a redemocratização.
            
            Em 2022, após décadas de atuação no campo da centro-direita e de rivalizar historicamente com o Partido dos Trabalhadores (PT), Alckmin desfiliou-se do PSDB e ingressou no Partido Socialista Brasileiro (PSB) para compor a chapa presidencial de Luiz Inácio Lula da Silva. A aliança, vista como um movimento estratégico para atrair o eleitorado de centro e o setor empresarial, saiu vitoriosa na eleição presidencial daquele ano. Além de disputar a presidência da República de forma independente em 2006 e 2018, sua biografia ficou marcada pela conciliação política, pela gestão de forte perfil tecnocrata no Executivo paulista e por sua marcante transição para o cenário governamental federal contemporâneo.
            """;

        private const string JoseJorgeBiography = """
            José Jorge de Vasconcelos Lima (Recife, 1944) é um engenheiro civil, professor e político brasileiro de longa trajetória pública. Formado pela Universidade Federal de Pernambuco (UFPE), iniciou sua carreira técnica no setor público e na área de infraestrutura do estado. Ingressou na política eleitoral nos anos 1970 pelo MDB, migrando posteriormente para o PDS e o PFL (atual União Brasil). Exerceu três mandatos consecutivos como deputado federal constituinte por Pernambuco (1983-1999) e foi eleito senador em 1998, consolidando-se como uma das principais lideranças pefelistas do Nordeste e ocupando o cargo de Ministro das Minas e Energia durante o segundo governo de Fernando Henrique Cardoso (2001-2002).

            Em 2006, José Jorge ganhou projeção nacional ao compor como candidato a vice-presidente a chapa encabeçada por Geraldo Alckmin (PSDB), aliança que acabou derrotada no segundo turno por Luiz Inácio Lula da Silva. Três anos após a disputa presidencial, em 2009, foi indicado pelo Senado e empossado como ministro do Tribunal de Contas da União (TCU), órgão onde atuou como julgador até sua aposentadoria compulsória no fim de 2014, quando encerrou formalmente sua vida pública.
            """;

        private const string DilmaBiography = """
            Dilma Vana Rousseff é uma economista e política brasileira que atuou como a 36ª presidente do Brasil, de 2011 até seu impeachment em 2016, tornando-se a primeira mulher a chefiar o Executivo do país. Nascida em Belo Horizonte em 1947, ela iniciou sua militância política na juventude durante a ditadura militar, integrando organizações de esquerda clandestinas, o que resultou em sua prisão e tortura entre 1970 e 1972. Após ser libertada, reconstruiu sua vida no Rio Grande do Sul, onde participou da fundação do PDT antes de se filiar ao PT em 2001, consolidando-se como uma gestora técnica e de perfil centralizador.
            
            Durante o governo de Luiz Inácio Lula da Silva, Dilma ganhou projeção nacional ao assumir o Ministério de Minas e Energia e, posteriormente, a chefia da Casa Civil, sendo apontada como a "mãe" do Programa de Aceleração do Crescimento (PAC). Eleita presidente em 2010 e reeleita em 2014, seu mandato foi marcado por fortes investimentos sociais, mas também enfrentou severa recessão econômica e intensos protestos populares. Em agosto de 2016, foi destituída do cargo após um processo de impeachment por crime de responsabilidade fiscal (as chamadas "pedaladas fiscais"). Anos mais tarde, em 2023, foi eleita presidente do Novo Banco de Desenvolvimento (o Banco dos BRICS), cargo que ocupa atualmente em Xangai, na China.
            """;

        private const string TemerBiography = """
            Michel Miguel Elias Temer Lulia é um jurista, professor e político brasileiro que atuou como o 37º presidente do Brasil, de 31 de agosto de 2016 a 31 de dezembro de 2018. Nascido em Tietê, São Paulo, em 1940, formou-se em Direito pela USP e doutorou-se pela PUC-SP, tornando-se uma referência em Direito Constitucional. Iniciou sua carreira pública como procurador e secretário de Segurança Pública de São Paulo, ingressando posteriormente no PMDB (atual MDB), partido que presidiu por anos. Exerceu seis mandatos como deputado federal, chegando à presidência da Câmara dos Deputados por três vezes, consolidando-se como uma das lideranças políticas mais influentes do país.

            Sua ascensão ao Poder Executivo ocorreu ao ser eleito vice-presidente da República nas chapas de Dilma Rousseff em 2010 e 2014. Em maio de 2016, assumiu interinamente a presidência devido ao afastamento de Rousseff, sendo efetivado em agosto do mesmo ano após a conclusão do processo de impeachment. Seu governo foi marcado pela implementação de reformas estruturais de perfil liberal, como o Teto de Gastos Públicos e a Reforma Trabalhista, além da modernização do Ensino Médio. Apesar de enfrentar forte oposição e denúncias de corrupção que impactaram sua popularidade, Temer concluiu o mandato entregando o cargo ao sucessor eleito em 2018 e, desde então, atua na iniciativa privada, na advocacia e como conselheiro político.
            """;

        private const string IndioBiography = """
            Antônio Pedro Índio da Costa, conhecido publicamente como Índio da Costa, é um advogado, empresário e ex-político brasileiro nascido no Rio de Janeiro em 1970. Graduado em Direito pela Universidade Candido Mendes, iniciou sua trajetória na administração pública na década de 1990. Ele ocupou o cargo de vereador na capital fluminense por três mandatos e atuou como Secretário Municipal de Administração. No âmbito legislativo federal, exerceu o mandato de deputado federal e atuou como relator do projeto da Lei da Ficha Limpa na Câmara dos Deputados.
            
            Em decorrência de sua atuação parlamentar, foi indicado para compor a chapa de José Serra como candidato a vice-presidente da República nas eleições de 2010. Posteriormente, exerceu outras funções públicas no estado do Rio de Janeiro, incluindo o cargo de Secretário Estadual do Ambiente e novos mandatos como deputado federal. Disputou a prefeitura da capital fluminense em 2016 e o governo do estado em 2018. Atualmente, encontra-se afastado da atividade político-partidária, direcionando sua atuação profissional para o setor privado nas áreas de advocacia, gestão e finanças.
            """;

        private const string AecioBiography = """
            Aécio Neves da Cunha é um economista e político brasileiro de destaque nacional, filiado ao Partido da Social Democracia Brasileira (PSDB). Neto do ex-presidente Tancredo Neves, iniciou sua trajetória precoce na política, consolidando-se como governador de Minas Gerais por dois mandatos consecutivos (2003–2010) e, posteriormente, como senador. Seu auge político ocorreu em 2014, quando disputou a Presidência da República, sendo derrotado em segundo turno por uma margem estreita de votos pela candidata Dilma Rousseff.
            
            Nos anos seguintes, a carreira de Aécio Neves foi profundamente impactada por denúncias de corrupção e desvios, que vieram à tona principalmente no âmbito da Operação Lava Jato e em delações premiadas da empresa JBS. Embora as investigações tenham abalado sua força política e imagem pública em âmbito nacional, ele conseguiu manter capital político em seu estado natal. Atualmente, atua como deputado federal por Minas Gerais, concentrando suas articulações nos bastidores do Congresso Nacional e na reestruturação partidária.
            """;

        private const string AloysioBiography = """
            Aloysio Nunes Ferreira Filho é um experiente advogado e político brasileiro que construiu grande parte de sua trajetória pública no estado de São Paulo e no cenário federal. Nascido em São José do Rio Preto em 1945, iniciou sua militância na juventude participando da oposição armada à ditadura militar pela Ação Libertadora Nacional (ALN), período em que chegou a viver no exílio. Com a redemocratização, consolidou-se como uma das principais lideranças da social-democracia no país, exercendo mandatos de deputado estadual, deputado federal e vice-governador de São Paulo, além de ter ocupado os cargos de Ministro da Justiça no governo Fernando Henrique Cardoso e de Ministro das Relações Exteriores durante a gestão de Michel Temer.
            
            Após passar 27 anos filiado ao PSDB — partido pelo qual foi eleito senador e concorreu como candidato a vice-presidente na chapa de Aécio Neves em 2014 —, ele deixou formalmente os tucanos em 2024 devido a divergências sobre os rumos da sigla. Em junho de 2026, oficializou sua filiação ao PSB (Partido Socialista Brasileiro), alinhando-se politicamente ao grupo do ex-tucano e atual vice-presidente Geraldo Alckmin. Atualmente, Aloysio Nunes atua na diplomacia corporativa e comercial como chefe do escritório da ApexBrasil (Agência Brasileira de Promoção de Exportações e Investimentos) em Bruxelas, na Europa.
            """;

        private const string JairBolsonaroBiography = """
            Jair Messias Bolsonaro nasceu em Glicério, São Paulo, no dia 21 de março de 1955. Ele seguiu a carreira militar e formou-se na Academia Militar das Agulhas Negras em 1977, alcançando a patente de capitão do Exército. Ficou conhecido na política corporativa militar na década de 1980 e iniciou sua trajetória pública ao ser eleito vereador do Rio de Janeiro em 1988. Em seguida, exerceu sete mandatos consecutivos como deputado federal pelo Rio de Janeiro, de 1991 a 2018, destacando-se pela defesa de pautas conservadoras e de segurança.
            
            Em 2018, Jair Bolsonaro foi eleito o 38º presidente do Brasil pelo PSL, com um discurso focado na moralização política, costumes tradicionais e liberalismo econômico. Seu governo foi marcado por forte polarização e gestão de crises, incluindo o período da pandemia de COVID-19. Em 2022, concorreu à reeleição pelo PL, mas foi derrotado no segundo turno por Luiz Inácio Lula da Silva. Posteriormente, enfrentou investigações e decisões judiciais que tanto o tornaram inelegível e o levaram à prisão.
            """;

        private const string HamiltonMouraoBiography = """
            Antônio Hamilton Martins Mourão, nascido em Porto Alegre em 15 de agosto de 1953, é um general da reserva do Exército Brasileiro e político, exercendo atualmente o cargo de senador pelo Rio Grande do Sul desde 2023. De ascendência indígena, ele ingressou na carreira militar em 1972 na Academia Militar das Agulhas Negras (AMAN), onde se formou na arma da Artilharia. Ao longo de seus 46 anos de serviço ativo, Mourão alcançou o posto de General de Exército, o topo da hierarquia militar em tempos de paz, comandando unidades de prestígio como o Comando Militar do Sul e exercendo a chefia do Comando de Operações Terrestres (COTER), além de atuar em missões de paz da ONU em Angola.
            
            Sua transição definitiva para a política nacional ocorreu em 2018, quando foi eleito Vice-Presidente da República na chapa de Jair Bolsonaro, exercendo o mandato de 2019 a 2022. Durante a vice-presidência, presidiu o Conselho Nacional da Amazônia Legal e liderou comitês de coordenação com parceiros internacionais importantes, como a China. Conhecido por suas declarações firmes e, por vezes, posições de contraponto ao próprio presidente, Mourão filiou-se ao partido Republicanos em 2022, legenda pela qual conquistou uma cadeira no Senado Federal com mais de 2,5 milhões de votos para a legislatura que se estende até 2031.
            """;

        private const string HaddadBiography = """
            Fernando Haddad nasceu em São Paulo em 1963. Ele é advogado, professor universitário e político filiado ao Partido dos Trabalhadores (PT). Formou-se em Direito, fez mestrado em Economia e doutorado em Filosofia pela Universidade de São Paulo (USP). No início da carreira, atuou na iniciativa privada e no meio acadêmico antes de entrar na gestão pública.
            
            Na política, destacou-se como Ministro da Educação entre 2005 e 2012, período em que ajudou a criar programas como o Sisu e o Prouni. Depois, foi prefeito da cidade de São Paulo de 2013 a 2016, focando em mobilidade urbana. Também foi candidato à presidência em 2018 e assumiu o cargo de ministro da Fazenda a partir de 2023.
            """;

        private const string ManuelaBiography = """
            Manuela Pinto Vieira d'Ávila nasceu em Porto Alegre, no dia 18 de agosto de 1981. Ela é jornalista, escritora e política brasileira. Iniciou sua trajetória no movimento estudantil e foi eleita a vereadora mais jovem de Porto Alegre em 2004. Depois, atuou como deputada federal por dois mandatos e deputada estadual no Rio Grande do Sul. Também concorreu ao cargo de vice-presidente da República na chapa de Fernando Haddad em 2018.
            
            Além de sua atuação parlamentar, Manuela d'Ávila possui mestrado em Políticas Públicas pela Universidade Federal do Rio Grande do Sul (UFRGS) e é autora de livros voltados ao debate político e social, como relatos sobre violência de gênero. Após um período afastada de mandatos eletivos, retornou ao cenário político com foco em pautas democráticas e representatividade feminina.
            """;

        private const string BragaNettoBiography = """
            Walter Souza Braga Netto é um general de exército da reserva e político brasileiro, nascido em Belo Horizonte em 11 de março de 1957. Ele ingressou na carreira militar em 1975 e ganhou projeção nacional em 2018 ao ser nomeado pelo presidente Michel Temer como Interventor Federal na Segurança Pública do Estado do Rio de Janeiro. Com a eleição de Jair Bolsonaro, migrou para a vida pública civil, exercendo os cargos de Ministro-Chefe da Casa Civil em 2020 e de Ministro da Defesa em 2021, consolidando sua atuação no governo federal a ponto de ser escolhido como candidato a vice-presidente na chapa de Bolsonaro nas eleições de 2022.
            
            Após o período eleitoral, sua trajetória passou a ser marcada por desdobramentos jurídicos. O general foi detido preventivamente pela Polícia Federal em dezembro de 2024 sob a acusação de obstrução de justiça. Posteriormente, em setembro de 2025, foi condenado pela Primeira Turma do STF a 26 anos de prisão em regime inicial fechado por crimes como organização criminosa e tentativa de abolição violenta do Estado Democrático de Direito, passando a cumprir a pena em uma unidade militar no Rio de Janeiro.
            """;
        #endregion

        public void Seed(MigrationBuilder migrationBuilder)
        {
            #region Candidates
            migrationBuilder.InsertData(
                table: "candidate",
                columns: ["id", "uid", "name", "display_name", "tse_name", "is_deceased", "birth_date", "death_date", "biography"],
                values: new object?[,]
                {
                    { 1, Guid.NewGuid().ToString(), "Fernando Affonso Collor de Mello", "Fernando Collor", "FERNANDO AFFONSO COLLOR DE MELLO", false, new DateOnly(1949, 8, 12), null, CollorBiography },
                    { 2, Guid.NewGuid().ToString(), "Itamar Augusto Cautiero Franco", "Itamar Franco", "ITAMAR AUGUSTO CAUTIERO FRANCO", true, new DateOnly(1930, 6, 28), new DateOnly(2011, 7, 2), ItamarBiography },
                    { 3, Guid.NewGuid().ToString(), "Luiz Inácio Lula da Silva", "Lula", "LUIZ INÁCIO LULA DA SILVA", false, new DateOnly(1945, 10, 27), null, LulaBiography },
                    { 4, Guid.NewGuid().ToString(), "José Paulo Bisol", "Paulo Bisol", "JOSÉ PAULO BISOL", true, new DateOnly(1928, 10, 22), new DateOnly(2021, 6, 26), BisolBiography },
                    { 5, Guid.NewGuid().ToString(), "José Alencar Gomes da Silva", "José Alencar", "JOSÉ ALENCAR GOMES DA SILVA", true, new DateOnly(1931, 10, 17), new DateOnly(2011, 3, 29), JoseAlencarBiography },
                    { 6, Guid.NewGuid().ToString(), "José Serra Chirico", "José Serra", "JOSÉ SERRA", false, new DateOnly(1942, 3, 19), null, SerraBiography },
                    { 7, Guid.NewGuid().ToString(), "Rita de Cássia Paste Camata", "Rita Camata", "RITA DE CÁSSIA PASTE CAMATA", false, new DateOnly(1961, 1, 1), null, CamataBiography },
                    { 8, Guid.NewGuid().ToString(), "Geraldo José Rodrigues Alckmin Filho", "Geraldo Alckmin", "GERALDO JOSÉ RODRIGUES ALCKMIN FILHO", false, new DateOnly(1952, 11, 7), null, AlckminBiography },
                    { 9, Guid.NewGuid().ToString(), "José Jorge de Vasconcelos Lima", "José Jorge", "JOSÉ JORGE DE VASCONCELOS LIMA", false, new DateOnly(1944, 11, 18), null, JoseJorgeBiography },
                    { 10, Guid.NewGuid().ToString(), "Dilma Vana Rousseff", "Dilma Rousseff", "DILMA VANA ROUSSEFF", false, new DateOnly(1947, 12, 14), null, DilmaBiography },
                    { 11, Guid.NewGuid().ToString(), "Michel Miguel Elias Temer Lulia", "Michel Temer", "MICHEL MIGUEL ELIAS TEMER LULIA", false, new DateOnly(1940, 9, 23), null, TemerBiography },
                    { 12, Guid.NewGuid().ToString(), "Antonio Pedro Indio da Costa", "Indio da Costa", "ANTONIO PEDRO INDIO DA COSTA", false, new DateOnly(1970, 10, 20), null, IndioBiography },
                    { 13, Guid.NewGuid().ToString(), "Aécio Neves da Cunha", "Aécio Neves", "AÉCIO NEVES DA CUNHA", false, new DateOnly(1960, 3, 10), null, AecioBiography },
                    { 14, Guid.NewGuid().ToString(), "Aloysio Nunes Ferreira Filho", "Aloysio Nunes", "ALOYSIO NUNES FERREIRA FILHO", false, new DateOnly(1945, 4, 5), null, AloysioBiography },
                    { 15, Guid.NewGuid().ToString(), "Jair Messias Bolsonaro", "Jair Bolsonaro", "JAIR MESSIAS BOLSONARO", false, new DateOnly(1955, 3, 21), null, JairBolsonaroBiography },
                    { 16, Guid.NewGuid().ToString(), "Antônio Hamilton Martins Mourão", "Hamilton Mourão", "ANTÔNIO HAMILTON MARTINS MOURÃO", false, new DateOnly(1953, 8, 15), null, HamiltonMouraoBiography },
                    { 17, Guid.NewGuid().ToString(), "Fernando Haddad", "Fernando Haddad", "FERNANDO HADDAD", false, new DateOnly(1963, 1, 25), null, HaddadBiography },
                    { 18, Guid.NewGuid().ToString(), "Manuela Pinto Vieira d'Ávila", "Manuela d'Ávila", "MANUELA PINTO VIEIRA D'ÁVILA", false, new DateOnly(1981, 8, 18), null, ManuelaBiography },
                    { 19, Guid.NewGuid().ToString(), "Walter Souza Braga Netto", "Braga Netto", "WALTER SOUZA BRAGA NETTO", false, new DateOnly(1957, 3, 11), null, BragaNettoBiography }
                });
            #endregion

            #region Parties
            migrationBuilder.InsertData(
                table: "party",
                columns: ["id", "uid", "name", "abbr", "nr_voteable", "is_active", "founded_at", "registered_at", "dissolved_at"],
                values: new object?[,]
                {
                    { 1, Guid.NewGuid().ToString(), "Partido da Reconstrução Nacional", "PRN", 36, false, null, new DateOnly(1989, 2, 2), new DateOnly(2001, 4, 24) },
                    { 2, Guid.NewGuid().ToString(), "Partido dos Trabalhadores", "PT", 13, true, new DateOnly(1980, 2, 10), new DateOnly(1982, 2, 11), null },
                    { 3, Guid.NewGuid().ToString(), "Partido Socialista Brasileiro", "PSB", 40, true, new DateOnly(1985, 7, 2), new DateOnly(1988, 7, 1), null },
                    { 4, Guid.NewGuid().ToString(), "Partido Liberal", "PL", 22, false, null, new DateOnly(1985, 6, 23), new DateOnly(2006, 10, 26) },
                    { 5, Guid.NewGuid().ToString(), "Partido da Social Democracia Brasileira", "PSDB", 45, true, new DateOnly(1988, 6, 25), new DateOnly(1989, 8, 24), null },
                    { 6, Guid.NewGuid().ToString(), "Partido do Movimento Democrático Brasileiro", "PMDB", 15, false, new DateOnly(1980, 1, 15), new DateOnly(1981, 6, 30), new DateOnly(2018, 5, 15) },
                    { 7, Guid.NewGuid().ToString(), "Partido Republicano Brasileiro", "PRB", 10, false, new DateOnly(2003, 10, 6), new DateOnly(2005, 8, 10), new DateOnly(2019, 8, 15) },
                    { 8, Guid.NewGuid().ToString(), "Partido da Frente Liberal", "PFL", 25, false, new DateOnly(1985, 1, 24), new DateOnly(1986, 9, 11), new DateOnly(2007, 6, 12) },
                    { 9, Guid.NewGuid().ToString(), "Democratas", "DEM", 25, false, null, new DateOnly(2007, 6, 12), new DateOnly(2022, 2, 8) },
                    { 10, Guid.NewGuid().ToString(), "Partido Social Liberal", "PSL", 17, false, new DateOnly(1994, 10, 30), new DateOnly(1998, 6, 2), new DateOnly(2022, 2, 8) },
                    { 11, Guid.NewGuid().ToString(), "Partido Renovador Trabalhista Brasileiro", "PRTB", 28, true, new DateOnly(1994, 11, 27), new DateOnly(1995, 3, 28), null },
                    { 12, Guid.NewGuid().ToString(), "Partido Comunista do Brasil", "PCdoB", 65, true, new DateOnly(1962, 2, 18), new DateOnly(1988, 6, 23), null },
                    { 13, Guid.NewGuid().ToString(), "Partido Liberal", "PL", 22, true, null, new DateOnly(2019, 2, 9), null }
                });
            #endregion

            #region CandidateParties
            migrationBuilder.InsertData(
                table: "candidate_party",
                columns: ["id", "candidate_id", "party_id"],
                values: new object[,]
                {
                    { 1, 1, 1 }, // Collor (PRN)
                    { 2, 2, 1 }, // Itamar (PRN)
                    { 3, 3, 2 }, // Lula (PT)
                    { 4, 4, 3 }, // Bisol (PSB)
                    { 5, 5, 4 }, // José Alencar (PL)
                    { 6, 6, 5 }, // José Serra (PSDB)
                    { 7, 7, 6 }, // Rita Camata (PMDB)
                    { 8, 5, 7 }, // José Alencar (PRB)
                    { 9, 8, 5 }, // Alckmin (PSDB)
                    { 10, 9, 8 }, // José Jorge (PFL)
                    { 11, 10, 2 }, // Dilma (PT)
                    { 12, 11, 6 }, // Temer (PMDB)
                    { 13, 12, 9 }, // Indio da Costa (DEM)
                    { 14, 13, 5 }, // Aécio (PSDB)
                    { 15, 14, 5 }, // Aloysio Nunes (PSDB)
                    { 16, 15, 10 }, // Jair Bolsonaro (PSL)
                    { 17, 16, 11 }, // Hamilton Mourão (PRTB)
                    { 18, 17, 2 }, // Haddad (PT)
                    { 19, 18, 12 }, // Manuela d'Ávila (PCdoB)
                    { 20, 8, 3 }, // Alckmin (PSB)
                    { 21, 15, 13 }, // Jair Bolsonaro (PL)
                    { 22, 19, 13 } // Braga Netto (PL)
                });
            #endregion

        }
    }
}
