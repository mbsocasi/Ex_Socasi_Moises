using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class RepositoryFactory
    {
        public static IRepository CreateRepository()
        {
            return new EFRepository(new Hospitales_DBEntities()); // Reemplaza con el contexto generado para tu modelo.
        }
    }
}
