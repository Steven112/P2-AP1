using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2_AP1.Entidades;
using P2_AP1.BLL;
using P2_AP1.DAL;
using System.Data.Entity;
using System.Linq.Expressions;

namespace P2_AP1.BLL
{
    public class FacturaBLL
    {
        public static bool Guardar(Factura factura)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Factura.Add(factura) != null)
                {
                    paso = db.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Factura factura)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var Anterior = FacturaBLL.Buscar(factura.FacturaId);
                foreach(var item in Anterior.Detalle)
                {
                    if(!!factura.Detalle.Exists(d=> d.ServicioId== item.ServicioId)){
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }
                db.Entry(factura).State = EntityState.Modified;
                paso = db.SaveChanges()>0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            Contexto db = new Contexto();
            Factura factura = new Factura();
            bool paso;
            try
            {
                var eliminar = db.Factura.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Factura Buscar(int id)
        {
            Factura factura = new Factura();
            Contexto db = new Contexto();

            try
            {
                factura = db.Factura.Find(id);
                if (factura != null)
                {
                    factura.Detalle.Count();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return factura;
        }
        public static List<Factura> GetList(Expression<Func<Factura, bool>> expression)
        {
            List<Factura> Lista = new List<Factura>();
            Contexto db = new Contexto();
            try
            {
                Lista =db.Factura.Where(expression).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return Lista;

        }
    }
   


}
