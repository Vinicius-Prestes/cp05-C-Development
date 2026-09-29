using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CampusEventos.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CATEGORIAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CATEGORIAS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_EVENTOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DataHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Local = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CapacidadeMaxima = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EVENTOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_EVENTOS_TB_CATEGORIAS_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "TB_CATEGORIAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TB_CATEGORIAS",
                columns: new[] { "Id", "Ativo", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, true, "Oficinas práticas e capacitações técnicas", "Workshop" },
                    { 2, true, "Apresentações com especialistas do mercado", "Palestra" },
                    { 3, true, "Maratonas de desenvolvimento e inovação", "Hackathon" }
                });

            migrationBuilder.InsertData(
                table: "TB_EVENTOS",
                columns: new[] { "Id", "CapacidadeMaxima", "CategoriaId", "DataHora", "Descricao", "Local", "Preco", "Titulo" },
                values: new object[,]
                {
                    { 1, 40, 1, new DateTime(2026, 10, 15, 19, 0, 0, 0, DateTimeKind.Unspecified), "Workshop focado em novidades do C# e boas práticas com EF Core", "Laboratório 504 - Campus Paulista", 0.00m, "Imersão em C# e .NET 10" },
                    { 2, 150, 2, new DateTime(2026, 10, 20, 20, 0, 0, 0, DateTimeKind.Unspecified), "Palestra sobre o impacto da IA no desenvolvimento corporativo", "Auditório FIAP", 0.00m, "Inteligência Artificial Generativa no Mercado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_EVENTOS_CategoriaId",
                table: "TB_EVENTOS",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_EVENTOS");

            migrationBuilder.DropTable(
                name: "TB_CATEGORIAS");
        }
    }
}
