using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable(t => t
                .HasComment("Tabela com os candidatos e os respectivos dados pessoais de cada um."));

            builder.Property(x => x.Id).HasComment("ID interno do candidato.");
            builder.Property(x => x.Uid).HasComment("ID externo do candidato.");
            builder.Property(x => x.Name).HasComment("Nome completo do candidato.");
            builder.Property(x => x.DisplayName).HasComment("Nome de exibição do candidato — o que é exibido nos resultados eleitorais.");
            builder.Property(x => x.TseName).HasComment("Nome do candidato que aparece nos arquivos de resultados eleitorais do TSE que serve como referência de ingestão dos dados de votação. Às vezes pode ser igual o nome completo, ou um apelido, ou o nome completo sem algum sobrenome específico. Por isso a necessidade de haver esse campo.");
            builder.Property(x => x.IsDeceased).HasComment("Campo booleano que determina se o candidato é falecido ou não");
            builder.Property(x => x.BirthDate).HasComment("Data de nascimento do candidato. Pode ser um dado difícil de encontrar em eleições mais remotas, por isso é anulável.");
            builder.Property(x => x.DeathDate).HasComment("Data de falecimento do candidato. É nula quando o candidato está vivo ou pelo mesmo motivo de birth_date.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_candidate_uid");
            builder.HasIndex(x => x.Name).IsUnique().HasDatabaseName("IX_candidate_name");
            builder.HasIndex(x => x.DisplayName).IsUnique().HasDatabaseName("IX_candidate_display_name");
            builder.HasIndex(x => x.TseName).IsUnique().HasDatabaseName("IX_candidate_tse_name");
        }
    }
}
