using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TFCastGroup.Domain.Model;

namespace TFCastGroup.Infra.Map
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd(); 
            builder.Property(x => x.DataInicio).HasColumnName("data_inicio").IsRequired();
            builder.Property(x => x.DataTermino).HasColumnName("data_termino").IsRequired();
            builder.Property(x => x.QuantidadeAlunosPorTurma).HasColumnName("qtd_alunos_por_turma");
            builder.Property(x => x.Descricao).HasColumnName("descricao").IsRequired();

            builder.HasOne(a => a.Categoria)
    .WithMany()
    .HasForeignKey(b => b.IdCategoria);

        }
    }
}
