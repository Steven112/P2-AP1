using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2_AP1.BLL;
using P2_AP1.DAL;
using P2_AP1.Entidades;
using P2_AP1.UI.Registros;

namespace P2_AP1.UI.Registros
{
    public partial class rServicios : Form
    {
        RepositorioBase<Servicios> GenericCategorias;
        rFactura factura = new rFactura();
        public rServicios()
        {
            InitializeComponent();
            GenericCategorias = new RepositorioBase<Servicios>();
        }
        private Servicios LlenaClase()
        {
            Servicios servicios = new Servicios();
            servicios.ServiciosId = Convert.ToInt32(IdnumericUpDown1.Value);
           servicios.Nombres = NombretextBox.Text;

            return servicios;
        }
        private bool Validar()
        {
            MyerrorProvider.Clear();
            bool paso = true;


            if (string.IsNullOrWhiteSpace(NombretextBox.Text))
            {
                MyerrorProvider.SetError(NombretextBox, "El campo no puede estar vacio");
                NombretextBox.Focus();
                paso = false;
            }

            return paso;
        }
        private void LlenaCampos(Servicios servicios)
        {
            IdnumericUpDown1.Value = servicios.ServiciosId;
            NombretextBox.Text = servicios.Nombres;
        }



        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void RAnonimo_Load(object sender, EventArgs e)
        {

        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Servicios categorias = new Servicios();
            bool paso = false;

            if (!Validar())
                return;

            categorias = LlenaClase();


            if (IdnumericUpDown1.Value == 0)
                paso = GenericCategorias.Guardar(categorias);
            else
            {
                if (!ExisteEnBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar un registro que no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = GenericCategorias.Modificar(categorias);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No Guardado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            factura.LlenaCombobox();
        }
        private void Limpiar()
        {
            IdnumericUpDown1.Value = 0;
            NombretextBox.Text = string.Empty;
        }
        private bool ExisteEnBaseDeDatos()
        {
            Servicios categorias = GenericCategorias.Buscar((int)IdnumericUpDown1.Value);
            return (categorias != null);
        }

        private void Buscrabutton_Click(object sender, EventArgs e)
        {
            int id;
            id = Convert.ToInt32(IdnumericUpDown1.Value);
            Servicios categorias= new Servicios();
            GenericCategorias = new RepositorioBase<Servicios>();

            Limpiar();

            categorias = GenericCategorias.Buscar(id);

            if (categorias != null)
            {
                LlenaCampos(categorias);
            }
            else
            {
                MessageBox.Show("Asignatura no encontrada");
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {

            MyerrorProvider.Clear();

            int id;
            id = Convert.ToInt32(IdnumericUpDown1.Value); 
            Contexto db = new Contexto();

            Servicios categorias = new Servicios();

            Limpiar();

            if (GenericCategorias.Eliminar(id))
            {
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MyerrorProvider.SetError(IdnumericUpDown1, "No se puede eliminar la asignatura");
            }
            factura.LlenaCombobox();
        }
    }
}
