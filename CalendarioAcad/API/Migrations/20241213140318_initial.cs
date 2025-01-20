using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendarios",
                columns: table => new
                {
                    IdCalendario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    NumeroResolucao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendarios", x => x.IdCalendario);
                });

            migrationBuilder.CreateTable(
                name: "Portarias",
                columns: table => new
                {
                    Id_Portaria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumPortaria = table.Column<int>(type: "int", nullable: false),
                    AnoPortaria = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portarias", x => x.Id_Portaria);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    IdEvento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFinal = table.Column<DateOnly>(type: "date", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UescFunciona = table.Column<bool>(type: "bit", nullable: false),
                    Importante = table.Column<bool>(type: "bit", nullable: false),
                    TipoFeriado = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCalendario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.IdEvento);
                    table.ForeignKey(
                        name: "FK_Eventos_Calendarios_IdCalendario",
                        column: x => x.IdCalendario,
                        principalTable: "Calendarios",
                        principalColumn: "IdCalendario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evento_Portarias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEvento = table.Column<int>(type: "int", nullable: false),
                    IdPortaria = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFinal = table.Column<DateOnly>(type: "date", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Portarias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evento_Portarias_Eventos_IdEvento",
                        column: x => x.IdEvento,
                        principalTable: "Eventos",
                        principalColumn: "IdEvento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evento_Portarias_Portarias_IdPortaria",
                        column: x => x.IdPortaria,
                        principalTable: "Portarias",
                        principalColumn: "Id_Portaria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historicos",
                columns: table => new
                {
                    IdHistorico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    DataMudanca = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCalendario = table.Column<int>(type: "int", nullable: false),
                    IdEvento = table.Column<int>(type: "int", nullable: false),
                    IdPortaria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historicos", x => x.IdHistorico);
                    table.ForeignKey(
                        name: "FK_Historicos_Calendarios_IdCalendario",
                        column: x => x.IdCalendario,
                        principalTable: "Calendarios",
                        principalColumn: "IdCalendario");
                    table.ForeignKey(
                        name: "FK_Historicos_Eventos_IdEvento",
                        column: x => x.IdEvento,
                        principalTable: "Eventos",
                        principalColumn: "IdEvento");
                    table.ForeignKey(
                        name: "FK_Historicos_Portarias_IdPortaria",
                        column: x => x.IdPortaria,
                        principalTable: "Portarias",
                        principalColumn: "Id_Portaria");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendarios_Ano",
                table: "Calendarios",
                column: "Ano",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Portarias_IdEvento",
                table: "Evento_Portarias",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Portarias_IdPortaria",
                table: "Evento_Portarias",
                column: "IdPortaria");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_IdCalendario",
                table: "Eventos",
                column: "IdCalendario");

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_IdCalendario",
                table: "Historicos",
                column: "IdCalendario");

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_IdEvento",
                table: "Historicos",
                column: "IdEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Historicos_IdPortaria",
                table: "Historicos",
                column: "IdPortaria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evento_Portarias");

            migrationBuilder.DropTable(
                name: "Historicos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Portarias");

            migrationBuilder.DropTable(
                name: "Calendarios");
        }
    }
}
