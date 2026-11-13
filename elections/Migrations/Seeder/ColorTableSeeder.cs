using Microsoft.EntityFrameworkCore.Migrations;

namespace elections.Migrations.Seeder
{
    public class ColorTableSeeder : ISeeder
    {
        public void Seed(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ticket_color",
                columns: ["id", "uid", "name", "lightest_hex", "standard_hex", "darkest_hex"],
                values: new object[,]
                {
                    { 1, Guid.NewGuid().ToString(), "Verde", "7FCCB2", "009966", "001E14" },
                    { 2, Guid.NewGuid().ToString(), "Vermelho", "FFD8D8", "FF132F", "2B0000" },
                    { 3, Guid.NewGuid().ToString(), "Azul PSDB", "CCEAFF", "0066CC", "001429" },
                    { 4, Guid.NewGuid().ToString(), "Azul REPUBLICANOS", "B9DAE8", "1884B4", "051A24" },
                    { 5, Guid.NewGuid().ToString(), "Amarelo", "FBFF81", "F7FF03", "313200" },
                    { 6, Guid.NewGuid().ToString(), "Laranja PSB/MDB", "FFD27F", "FFA500", "322000" },
                    { 7, Guid.NewGuid().ToString(), "Laranja NOVO", "F9B890", "F37221", "301606" },
                    { 8, Guid.NewGuid().ToString(), "Roxo PL", "9FA0CA", "3F4196", "06060E" },
                    { 9, Guid.NewGuid().ToString(), "Roxo PDT", "BF7FBF", "800080", "190019" },
                    { 10, Guid.NewGuid().ToString(), "Ciano", "A5EDFF", "4CDBFF", "0F2B32" },
                    { 11, Guid.NewGuid().ToString(), "Cinza", "BFBFBF", "808080", "191919" },
                    { 12, Guid.NewGuid().ToString(), "Azul Progressistas", "A9DBF4", "54B8EA", "10242E" }
                });

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 1,
                column: "ticket_color_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 2,
                column: "ticket_color_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 3,
                column: "ticket_color_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 4,
                column: "ticket_color_id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 5,
                column: "ticket_color_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 6,
                column: "ticket_color_id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 7,
                column: "ticket_color_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 8,
                column: "ticket_color_id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 9,
                column: "ticket_color_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 10,
                column: "ticket_color_id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 11,
                column: "ticket_color_id",
                value: 8);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 12,
                column: "ticket_color_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 13,
                column: "ticket_color_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 14,
                column: "ticket_color_id",
                value: 8);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 15,
                column: "ticket_color_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ticket",
                keyColumn: "id",
                keyValue: 16,
                column: "ticket_color_id",
                value: 8);
        }
    }
}
