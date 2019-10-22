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
            ServicioscomboBox.ValueMember = "ServiciosId";


        }
        public void Limpiar()
        {
            IdnumericUpDown.Value = 0;
            EstudiantetextBox.Text = string.Empty;
            ServicioscomboBox.SelectedValue = -1;
            FechadateTimePicker.Value = DateTime.Now;
            CantidadtextBox.Text = string.Empty;
            PreciotextBox.Text = string.Empty;
            ImportetextBox.Text = string.Empty;
            this.DetalleFac =new List<Detalle>();
            CargarGrid();
        }

        public Factura LlenaClase()
        {
            Factura factura = new Factura();
            factura.FacturaId = Convert.ToInt32(IdnumericUpDown.Value);
            factura.Fecha = FechadateTimePicker.Value;
            factura.Estudiante = EstudiantetextBox.Text;
            factura.Detalle = this.DetalleFac;
            factura.ServicioId= Convert.ToInt32(ServicioscomboBox.SelectedValue);

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

            if (string.IsNullOrWhiteSpace(EstudiantetextBox.Text))
            {
                MyerrorProvider.SetError(EstudiantetextBox, "Debe agregar algun estudiante");
                EstudiantetextBox.Focus();
                paso = false;
            }

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
            ServicioscomboBox.SelectedValue = factura.ServicioId;
            this.DetalleFac = factura.Detalle;
            CargarGrid();
        }



        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (FacturadataGridView.DataSource != null)
                this.DetalleFac = (List<Detalle>)FacturadataGridView.DataSource;

            string nombres = ServiciosGen.Buscar((int)ServicioscomboBox.SelectedValue).Nombres;
           
            this.DetalleFac.Add(
                new Detalle(
                   
                    servicioId: (int)ServicioscomboBox.SelectedValue,
                    nombre:nombres,
                    cantidad: Convert.ToInt32(CantidadtextBox.Text),
                    precio: Convert.ToDecimal(PreciotextBox.Text),
                    importe: Convert.ToDecimal(ImportetextBox.Text)
                    
                    ));
            CargarGrid();
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

        private void RFactura_Load(object sender, EventArgs e)
        {
            LlenaCombobox();
        }

        private void ImportetextBox_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            Factura factura = new Factura();
            int id =Convert.ToInt32( IdnumericUpDown.Value);

            factura = FacturaBLL.Buscar(id);

            if (factura != null)
            {
                LlenaCampo(factura);
            }
            else
            {
                MessageBox.Show("Registro no encontrado","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (FacturadataGridView.Rows.Count > 0 && FacturadataGridView.CurrentRow != null)
            {
                DetalleFac.RemoveAt(FacturadataGridView.CurrentRow.Index);
                CargarGrid();
            }
        }

        private void CantidadtextBox_TextChanged(object sender, EventArgs e)
        {
           

            
        }

        private void PreciotextBox_TextChanged(object sender, EventArgs e)
        {
            decimal cantidad = 0;
            decimal precio = 0;

            if (!string.IsNullOrWhiteSpace(PreciotextBox.Text))
            {
                precio = decimal.Parse(PreciotextBox.Text);
            }
            if (!string.IsNullOrWhiteSpace(CantidadtextBox.Text))
            {
                cantidad = decimal.Parse(CantidadtextBox.Text);
            }


            decimal total = precio * cantidad;
            ImportetextBox.Text = Convert.ToString(total);
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            MyerrorProvider.Clear();
            Contexto db = new Contexto();

            int id = Convert.ToInt32(IdnumericUpDown.Value);

            Limpiar();

            if (FacturaBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MyerrorProvider.SetError(IdnumericUpDown, "No se puede eliminar un registro que no existe");
            }
        }
    }
}
