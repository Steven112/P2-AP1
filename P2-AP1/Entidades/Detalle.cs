using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_AP1.Entidades
{
    public class Detalle
    {
        [Key]
        public int ServicioId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }

        public Detalle()
        {
        }

        public Detalle(int servicioId, string nombre, int cantidad, decimal precio, decimal importe)
        {
            ServicioId = servicioId;
            Nombre = nombre;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
        }
    }
}
