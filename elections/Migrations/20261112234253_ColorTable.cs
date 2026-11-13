using elections.Migrations.Seeder;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elections.Migrations
{
    /// <inheritdoc />
    public partial class ColorTable : Migration
    {
        /// <inheritdoc />
        private readonly ISeeder seeder = new ColorTableSeeder();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "color",
                table: "ticket");

            migrationBuilder.AddColumn<long>(
                name: "ticket_color_id",
                table: "ticket",
                type: "bigint",
                maxLength: 50,
                nullable: true,
                comment: "ID interno da cor usada para representar a chapa em mapas eleitorais.");

            migrationBuilder.CreateTable(
                name: "ticket_color",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "ID interno da cor.")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false, comment: "Nome da cor.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    standard_hex = table.Column<string>(type: "char(6)", fixedLength: true, maxLength: 6, nullable: false, comment: "Código hexadecimal padrão da cor.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lightest_hex = table.Column<string>(type: "char(6)", fixedLength: true, maxLength: 6, nullable: false, comment: "Código hexadecimal do tom mais claro da cor exibido em mapas eleitorais.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    darkest_hex = table.Column<string>(type: "char(6)", fixedLength: true, maxLength: 6, nullable: false, comment: "Código hexadecimal do tom mais escuro da cor exibido em mapas eleitorais.")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uid = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ID externo da cor.")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_color", x => x.id);
                },
                comment: "Tabela com as cores em hexadecimal utilizadas nos mapas eleitorais.")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_ticket_color_id",
                table: "ticket",
                column: "ticket_color_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_color_name",
                table: "ticket_color",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_color_uid",
                table: "ticket_color",
                column: "uid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_color",
                table: "ticket",
                column: "ticket_color_id",
                principalTable: "ticket_color",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            seeder.Seed(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticket_color",
                table: "ticket");

            migrationBuilder.DropTable(
                name: "ticket_color");

            migrationBuilder.DropIndex(
                name: "IX_ticket_ticket_color_id",
                table: "ticket");

            migrationBuilder.DropColumn(
                name: "ticket_color_id",
                table: "ticket");

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "ticket",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "Cor usada para representar a chapa em mapas eleitorais.")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
