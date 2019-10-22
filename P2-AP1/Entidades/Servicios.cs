using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_AP1.Entidades
{
    public class Servicios
    {
        [Key]
        public int ServiciosId { get; set; }
        public string Nombres { get; set; }

        public Servicios(int serviciosId, string nombres)
        {
            ServiciosId = serviciosId;
            Nombres = nombres;
        }
        public Servicios()
        {
            
        }

    }
}
