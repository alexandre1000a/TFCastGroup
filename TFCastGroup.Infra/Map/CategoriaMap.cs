using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TFCastGroup.Domain.Model;

namespace TFCastGroup.Infra.Map
{
    class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(p => p.Codigo);
            builder.Property(p => p.Codigo).ValueGeneratedNever();
            builder.Property(x => x.Codigo).HasColumnName("codigo");
            builder.Property(x => x.Descricao).HasColumnName("descricao");
            builder.Ignore(c => c.Curso);

            List<Categoria> categorias = new List<Categoria> { new Categoria
            {
                Codigo = 4,Descricao = "Comportamental"
            }, new Categoria{Codigo = 1, Descricao = "Programacao" }, new Categoria{ Codigo = 2,Descricao = "Qualidade"}, new Categoria{Codigo = 3,Descricao = "Processos" } };

            builder.HasData(categorias);


        }
    }
}

