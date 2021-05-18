using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TFCastGroup.Infra.Migrations
{
    public partial class InitalCreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    codigo = table.Column<long>(type: "bigint", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CursoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.codigo);
                    table.ForeignKey(
                        name: "FK_Categoria_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "codigo", "CursoId", "descricao" },
                values: new object[,]
                {
                    { 4L, null, "Comportamental" },
                    { 1L, null, "Programacao" },
                    { 2L, null, "Qualidade" },
                    { 3L, null, "Processos" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_CursoId",
                table: "Categoria",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_IdCategoria",
                table: "Curso",
                column: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_Categoria_IdCategoria",
                table: "Curso",
                column: "IdCategoria",
                principalTable: "Categoria",
                principalColumn: "codigo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Curso_CursoId",
                table: "Categoria");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
