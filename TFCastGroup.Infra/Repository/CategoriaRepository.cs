using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TFCastGroup.Domain.Interface;
using TFCastGroup.Domain.Model;
using TFCastGroup.Infra.Configuration;

namespace TFCastGroup.Infra.Repository
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(DbContextOptions<ContextCastGroup> contextCastGroup) : base(contextCastGroup)
        {
            
        }
    }
}
