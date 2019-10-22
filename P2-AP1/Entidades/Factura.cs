using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2_AP1.Entidades;

namespace P2_AP1.Entidades
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Estudiante { get; set; }
        public virtual List<Detalle> Detalle { get; set; }
        public int ServicioId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }

        public Factura()
        {
        }

        public Factura(int facturaId, DateTime fecha, string estudiante, List<Detalle> detalle, int servicioId, int cantidad, decimal precio, decimal importe)
        {
            FacturaId = facturaId;
            Fecha = fecha;
            Estudiante = estudiante;
            Detalle = detalle;
            ServicioId = servicioId;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
        }
    }
}
