﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class ExemploDeBatidas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var userId = new Guid("dd1edfcd-1425-4cd8-a8a4-421967940eb5");
            var entrada = new DateTime(2024, 3, 21, 9, 0, 0, DateTimeKind.Utc);
            var almoco = new DateTime(2024, 3, 21, 12, 0, 0, DateTimeKind.Utc);
            var retorno = new DateTime(2024, 3, 21, 13, 0, 0, DateTimeKind.Utc);
            var saida = new DateTime(2024, 3, 21, 18, 0, 0, DateTimeKind.Utc);
            var baseDate = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc); // Iniciando em 1 de fevereiro de 2024, marcado como UTC
            var random = new Random();

            migrationBuilder.InsertData(
                table: "Pontos",
                columns: new[] { "Id", "DataHora", "TipoPonto", "Observacao", "UserId" },
                values: new object[] { Guid.NewGuid(), entrada, 0, null, userId }
            );

            migrationBuilder.InsertData(
                table: "Pontos",
                columns: new[] { "Id", "DataHora", "TipoPonto", "Observacao", "UserId" },
                values: new object[] { Guid.NewGuid(), almoco, 1, null, userId }
            );

            migrationBuilder.InsertData(
                table: "Pontos",
                columns: new[] { "Id", "DataHora", "TipoPonto", "Observacao", "UserId" },
                values: new object[] { Guid.NewGuid(), retorno, 2, null, userId }
            );

            migrationBuilder.InsertData(
                table: "Pontos",
                columns: new[] { "Id", "DataHora", "TipoPonto", "Observacao", "UserId" },
                values: new object[] { Guid.NewGuid(), saida, 3, null, userId }
            );

            //Batidas aleatórias para o mês de fevereiro de 2024
            for (int day = 0; day < 22; day++)
            {
                var currentDate = baseDate.AddDays(day);

                // Certifique-se de que cada DateTime esteja especificado como UTC
                migrationBuilder.InsertData(
                    table: "Pontos",
                    columns: new[] { "Id", "DataHora", "TipoPonto", "Observacao", "UserId" },
                    values: new object[] { Guid.NewGuid(), DateTime.SpecifyKind(currentDate.AddHours(9).AddMinutes(random.Next(60)), DateTimeKind.Utc), 0, null, userId }
                );

                migrationBuilder.InsertData(
                    table: "Pontos",
                    columns: new[] { "Id", "DataHora", "TipoPonto", "Observacao", "UserId" },
                    values: new object[] { Guid.NewGuid(), DateTime.SpecifyKind(currentDate.AddHours(12).AddMinutes(random.Next(60)), DateTimeKind.Utc), 1, null, userId }
                );

                migrationBuilder.InsertData(
                    table: "Pontos",
                    columns: new[] { "Id", "DataHora", "TipoPonto", "Observacao", "UserId" },
                    values: new object[] { Guid.NewGuid(), DateTime.SpecifyKind(currentDate.AddHours(13).AddMinutes(random.Next(60)), DateTimeKind.Utc), 2, null, userId }
                );

                migrationBuilder.InsertData(
                    table: "Pontos",
                    columns: new[] { "Id", "DataHora", "TipoPonto", "Observacao", "UserId" },
                    values: new object[] { Guid.NewGuid(), DateTime.SpecifyKind(currentDate.AddHours(18).AddMinutes(random.Next(60)), DateTimeKind.Utc), 3, null, userId }
                );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Pontos WHERE UserId = 'dd1edfcd-1425-4cd8-a8a4-421967940eb5'");
        }
    }
}
