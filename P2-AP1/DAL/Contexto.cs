using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2_AP1.Entidades;

namespace P2_AP1.DAL
{
    public class Contexto: DbContext
    {
        public DbSet<Categorias> Categorias { get; set; }
        public Contexto() : base("ConStr")
        {

        }
    }
}
