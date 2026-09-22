using elections.Constants;
using elections.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace elections.Data.Configurations
{
    public class ElectionConfiguration : IEntityTypeConfiguration<Election>
    {
        public void Configure(EntityTypeBuilder<Election> builder)
        {
            builder.ToTable(t => t.HasComment("Tabela com as eleições."));

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_election_role", 
                $"""
                role IN 
                (
                    '{Constant.Election.Role.President}', 
                    '{Constant.Election.Role.Governor}', 
                    '{Constant.Election.Role.Mayor}', 
                    '{Constant.Election.Role.Senator}',
                    '{Constant.Election.Role.HouseRepresentative}',
                    '{Constant.Election.Role.StateRepresentative}',
                    '{Constant.Election.Role.CityCouncilor}'
                )
                """));

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_election_type", 
                $"""
                type IN 
                (
                    '{Constant.Election.Type.Ordinary}', 
                    '{Constant.Election.Type.Supplementary}'
                )
                """));

            builder.ToTable(t => t.HasCheckConstraint(
                "CK_election_role_scope",
                $"""
                (
                    role = '{Constant.Election.Role.President}' 
                    AND municipality_id IS NULL
                    AND state_id IS NULL
                )
                OR
                (   role IN
                    (
                        '{Constant.Election.Role.Governor}',
                        '{Constant.Election.Role.Senator}',
                        '{Constant.Election.Role.HouseRepresentative}',
                        '{Constant.Election.Role.StateRepresentative}'
                    )
                    AND municipality_id IS NULL 
                    AND state_id IS NOT NULL
                )
                OR
                (   role IN
                    (
                        '{Constant.Election.Role.Mayor}',
                        '{Constant.Election.Role.CityCouncilor}'
                    )
                    AND municipality_id IS NOT NULL 
                    AND state_id IS NULL
                )
                """));

            builder.Property(x => x.Id).HasComment("ID interno da eleição.");
            builder.Property(x => x.Uid).HasComment("ID externo da eleição.");
            builder.Property(x => x.Role).HasMaxLength(50).HasComment("Cargo em disputa da eleição.");
            builder.Property(x => x.Type).HasMaxLength(50).HasComment("O tipo da eleição.");
            builder.Property(x => x.MunicipalityId).HasComment("ID interno do munícipio caso o cargo em disputa for Prefeito, Vereador ou algum outro cargo correlato a nível municipal.");
            builder.Property(x => x.StateId).HasComment("ID interno da UF caso o cargo em disputa for Governador do Estado, Deputado Estadual ou Federal, Senador, ou algum outro cargo com relação direta à UF.");

            builder.HasIndex(x => x.Uid).IsUnique().HasDatabaseName("IX_election_uid");

            builder.HasOne(x => x.Municipality)
                .WithMany(x => x.Elections)
                .HasForeignKey(x => x.MunicipalityId)
                .HasForeignKey("FK_election_municipality_id");

            builder.HasOne(x => x.State)
                .WithMany(x => x.Elections)
                .HasForeignKey(x => x.StateId)
                .HasForeignKey("FK_election_state_id");
        }
    }
}
