using elections.Constants;
using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class TicketElectionRoundStatisticConfiguration : IEntityTypeConfiguration<TicketElectionRoundStatistic>
    {
        public void Configure(EntityTypeBuilder<TicketElectionRoundStatistic> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela com as estatísticas da chapa eleitoral e as gerais do turno da eleição. As estatísticas do mesmo turno eleitoral variam conforme o nível de agregação (localidade) com exceção ao status da chapa e à quantidade de candidatos."));
            builder.ToTable(t => t.HasCheckConstraint(
                "CK_ticket_election_round_statistic_ticket_status",
                $"""
                ticket_status IN 
                (
                    '{Constant.Election.Status.Elected}', 
                    '{Constant.Election.Status.NotElected}', 
                    '{Constant.Election.Status.Runoff}'
                )
                """
                ));

            builder.Property(x => x.Id).HasComment("ID interno das estatísticas.");
            builder.Property(x => x.TicketId).HasComment("ID interno do turno da chapa eleitoral.");
            builder.Property(x => x.ElectionRoundId).HasComment("ID interno do turno da eleição.");
            builder.Property(x => x.MunicipalityId).HasComment("ID interno do Município.");
            builder.Property(x => x.MetropolitanAreaId).HasComment("ID interno da Região Metropolitana.");
            builder.Property(x => x.StateId).HasComment("ID interno da UF.");
            builder.Property(x => x.MacroregionId).HasComment("ID interno da Macrorregião.");
            builder.Property(x => x.TicketVotesCount).HasComment("Contagem de votos da chapa eleitoral.");
            builder.Property(x => x.TicketPositionNr).HasComment("Posição da chapa eleitoral.");
            builder.Property(x => x.TicketValidVotesPp).HasComment("Porcentagem de votos válidos da chapa eleitoral.");
            builder.Property(x => x.TicketTotalVotesPp).HasComment("Porcentagem de votos da chapa em relação à contagem total de votos.");
            builder.Property(x => x.TicketRegisteredVotesPp).HasComment("Porcentagem de votos da chapa em relação à contagem total de inscritos.");
            builder.Property(x => x.TicketStatus).HasMaxLength(50).HasComment("Status da chapa eleitoral no turno da eleição.Tem sempre o mesmo valor independente do nível de agregação (localidade).");
            builder.Property(x => x.RegisteredVotersCount).HasComment("Contagem de inscritos.");
            builder.Property(x => x.TurnoutCount).HasComment("Contagem de comparecimento.");
            builder.Property(x => x.AbstentionCount).HasComment("Contagem de abstenção.");
            builder.Property(x => x.ValidVotesCount).HasComment("Contagem de todos os votos válidos.");
            builder.Property(x => x.InvalidVotesCount).HasComment("Contagem de votos inválidos.");
            builder.Property(x => x.NullVotesCount).HasComment("Contagem de votos nulos.");
            builder.Property(x => x.BlankVotesCount).HasComment("Contagem de votos em branco.");
            builder.Property(x => x.AnulledVotesCount).HasComment("Contagem de votos que foram anulados por decisão da Justiça Eleitoral.");
            builder.Property(x => x.ElectoralAlienationCount).HasComment("Contagem da alienação eleitoral (abstenção + votos inválidos).");
            builder.Property(x => x.CandidatesCount).HasComment("Contagem de candidatos. Invariável independente do nível de agregação.");
            builder.Property(x => x.TurnoutPp).HasComment("Porcentagem de comparecimento. Relativa ao total de inscritos.");
            builder.Property(x => x.AbstentionPp).HasComment("Porcentagem de abstenção. Relativa ao total de inscritos.");
            builder.Property(x => x.ValidVotesPp).HasComment("Porcentagem de votos válidos relativa ao total de votos.");
            builder.Property(x => x.ValidVotesRegisteredPp).HasComment("Porcentagem de votos válidos relativa aos inscritos.");
            builder.Property(x => x.InvalidVotesPp).HasComment("Porcentagem de votos inválidos relativa ao total de votos.");
            builder.Property(x => x.InvalidVotesRegisteredPp).HasComment("Porcentagem de votos inválidos relativa aos inscritos.");
            builder.Property(x => x.NullVotesPp).HasComment("Porcentagem de votos nulos relativa ao total de votos.");
            builder.Property(x => x.NullVotesRegisteredPp).HasComment("Porcentagem de votos nulos relativa aos inscritos.");
            builder.Property(x => x.BlankVotesPp).HasComment("Porcentagem de votos em branco relativa ao total de votos.");
            builder.Property(x => x.BlankVotesRegisteredPp).HasComment("Porcentagem de votos em branco relativa aos inscritos.");
            builder.Property(x => x.AnulledVotesPp).HasComment("Porcentagem de votos anulados relativa ao total de votos.");
            builder.Property(x => x.AnulledVotesRegisteredPp).HasComment("Porcentagem de votos anulados relativa aos inscritos.");
            builder.Property(x => x.ElectoralAlienationPp).HasComment("Porcentagem da alienação eleitoral relativa ao total de inscritos.");

            builder.HasIndex(x => new { x.TicketId, x.ElectionRoundId, x.MunicipalityId }).IsUnique().HasDatabaseName("IX_ticket_election_round_statistic_municipality");
            builder.HasIndex(x => new { x.TicketId, x.ElectionRoundId, x.MetropolitanAreaId }).IsUnique().HasDatabaseName("IX_ticket_election_round_statistic_metropolitan_area");
            builder.HasIndex(x => new { x.TicketId, x.ElectionRoundId, x.StateId }).IsUnique().HasDatabaseName("IX_ticket_election_round_statistic_state");
            builder.HasIndex(x => new { x.TicketId, x.ElectionRoundId, x.MacroregionId }).IsUnique().HasDatabaseName("IX_ticket_election_round_statistic_macroregion");

            builder.HasOne(x => x.Ticket)
                .WithMany(x => x.TicketElectionRoundStatistics)
                .HasForeignKey(x => x.TicketId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ticket_election_round_statistic_ticket_id");

            builder.HasOne(x => x.ElectionRound)
                .WithMany(x => x.TicketElectionRoundStatistics)
                .HasForeignKey(x => x.ElectionRoundId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ticket_election_round_statistic_election_round_id");

            builder.HasOne(x => x.Municipality)
                .WithMany(x => x.TicketElectionRoundStatistics)
                .HasForeignKey(x => x.MunicipalityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ticket_election_round_statistic_municipality_id");

            builder.HasOne(x => x.MetropolitanArea)
                .WithMany(x => x.TicketElectionRoundStatistics)
                .HasForeignKey(x => x.MetropolitanAreaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ticket_election_round_statistic_metropolitan_area_id");

            builder.HasOne(x => x.State)
                .WithMany(x => x.TicketElectionRoundStatistics)
                .HasForeignKey(x => x.StateId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ticket_election_round_statistic_state_id");

            builder.HasOne(x => x.Macroregion)
                .WithMany(x => x.TicketElectionRoundStatistics)
                .HasForeignKey(x => x.MacroregionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ticket_election_round_statistic_macroregion_id");
        }
    }
}
