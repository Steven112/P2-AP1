using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2_AP1.Entidades;
using P2_AP1.BLL;
using P2_AP1.DAL;

namespace P2_AP1.UI.Registros
{
    public partial class rFactura : Form
    {
        public RepositorioBase<Servicios> ServiciosGen;
        public List<Detalle> DetalleFac;
        public rFactura()
        {
            InitializeComponent();
            ServiciosGen = new RepositorioBase<Servicios>();
            this.DetalleFac = new List<Detalle>();
        }

        public void CargarGrid()
        {
            FacturadataGridView.DataSource = null;
            FacturadataGridView.DataSource = this.DetalleFac;
        }

        public void LlenaCombobox()
        {
            ServicioscomboBox.DataSource = null;
            RepositorioBase<Servicios> Generic = new RepositorioBase<Servicios>();
            List<Servicios> Lista1 = Generic.GetList(p => true);
            ServicioscomboBox.DataSource = Lista1;
            ServicioscomboBox.DisplayMember = "Nombres";
            ServicioscomboBox.SelectedValue = "ServiciosId";


        }

        public Factura LlenaClase()
        {
            Factura factura = new Factura();
            factura.FacturaId = Convert.ToInt32(IdnumericUpDown.Value);
            factura.Fecha = FechadateTimePicker.Value;
            factura.Estudiante = EstudiantetextBox.Text;
            factura.Detalle = this.DetalleFac;
            factura.Cantidad = Convert.ToInt32(CantidadtextBox.Text);
            decimal total = Convert.ToInt32(CantidadtextBox.Text) * Convert.ToDecimal(PreciotextBox.Text);
            factura.Precio = Convert.ToDecimal(PreciotextBox.Text);
            ImportetextBox.Text= Convert.ToString(total);
            factura.Importe= Convert.ToDecimal(ImportetextBox.Text);
            CargarGrid();

            return factura;
        }
        private bool Validar()
        {
            MyerrorProvider.Clear();
            bool paso = true;


            if (this.DetalleFac.Count==0)
            {
                MyerrorProvider.SetError(FacturadataGridView, "El campo no puede estar vacio");
                ServicioscomboBox.Focus();
                paso = false;
            }

            /*if (EstudiantetextBox.Text = string.Empty)
            {
                MyerrorProvider.SetError(FacturadataGridView, "El campo no puede estar vacio");
                ServicioscomboBox.Focus();
                paso = false;
            }*/

            return paso;
        }
        private bool ExisteEnBaseDeDatos()
        {
            Factura factura = FacturaBLL.Buscar((int)IdnumericUpDown.Value);
            return (factura != null);
        }

        public void LlenaCampo(Factura factura)
        {
            IdnumericUpDown.Value = factura.FacturaId;
            FechadateTimePicker.Value = factura.Fecha;
            EstudiantetextBox.Text = factura.Estudiante;
            this.DetalleFac = factura.Detalle;
            CantidadtextBox.Text = Convert.ToString(factura.Cantidad);
            PreciotextBox.Text = Convert.ToString(factura.Precio);
            ImportetextBox.Text = Convert.ToString(factura.Importe);
            CargarGrid();
        }



        private void Guardarbutton_Click(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (FacturadataGridView.DataSource != null)
                this.DetalleFac = (List<Detalle>)FacturadataGridView.DataSource;

            string nombres = ServiciosGen.Buscar((int)ServicioscomboBox.SelectedValue).Nombres;

            this.DetalleFac.Add(
                new Detalle(
                    servicioId: (int)ServicioscomboBox.SelectedValue,
                    nombre: nombres,
                    cantidad: Convert.ToInt32(CantidadtextBox.Text),
                    precio: Convert.ToDecimal(PreciotextBox.Text),
                    importe: Convert.ToDecimal(ImportetextBox.Text)

                    ));
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Guardarbutton_Click_1(object sender, EventArgs e)
        {
            bool paso;
            Factura factura = new Factura();

            if (!Validar())
                return;

            factura = LlenaClase();

            if (IdnumericUpDown.Value == 0)
            {
                paso = FacturaBLL.Guardar(factura);
            }
            else
            {
                if (!ExisteEnBaseDeDatos())
                {
                    MessageBox.Show("Registro no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = FacturaBLL.Modificar(factura);
            }

            if (paso)
            {
                MessageBox.Show("Guardado Correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Registro no Guardado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
    }
}
