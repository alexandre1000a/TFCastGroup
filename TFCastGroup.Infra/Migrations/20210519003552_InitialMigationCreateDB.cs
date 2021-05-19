using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TFCastGroup.Infra.Migrations
{
    public partial class InitialMigationCreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    codigo = table.Column<long>(type: "bigint", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_termino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    qtd_alunos_por_turma = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curso_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "codigo", "descricao" },
                values: new object[,]
                {
                    { 4L, "Comportamental" },
                    { 1L, "Programacao" },
                    { 2L, "Qualidade" },
                    { 3L, "Processos" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curso_IdCategoria",
                table: "Curso",
                column: "IdCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
