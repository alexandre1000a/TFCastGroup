using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TFCastGroup.Domain.Model;
using TFCastGroup.Infra.Map;

namespace TFCastGroup.Infra.Configuration
{
    public class ContextCastGroup : DbContext
    {
        public ContextCastGroup(DbContextOptions<ContextCastGroup> options) : base(options)
        {
            Database.EnsureCreated();
         
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Curso> Cursos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new CursoMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

            
        //    // Se não estiver configurado no projeto IU pega deginição de chame do json configurado
        //    if (!optionsBuilder.IsConfigured)
        //        optionsBuilder.UseSqlServer(GetStringConectionConfig());
        //    base.OnConfiguring(optionsBuilder);
        //}

        //private string GetStringConectionConfig()
        //{
        //    string strCon = "Data Source=DESKTOP-T7CR44R\\Alexandre;Initial Catalog=CAST_GROUP;Integrated Security=True;";
        //    return strCon;
        //}
    }
}
