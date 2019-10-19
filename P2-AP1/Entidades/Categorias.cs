using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_AP1.Entidades
{
    public class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }
        public string Descripcion { get; set; }

        public Categorias(int categoriaId, string descripcion)
        {
            CategoriaId = categoriaId;
            Descripcion = descripcion;
        }
        public Categorias()
        {
            
        }

    }
}
